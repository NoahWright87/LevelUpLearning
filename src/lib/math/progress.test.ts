import { expect, it } from "vitest";
import { activePlayer, addPlayer, emptySave, parseSaveFile, SAVE_VERSION, serializeSave } from "../save/schema";
import { DEFAULT_SETTINGS, startGame, submitAnswer } from "./game";
import { MAX_MATH_SESSIONS, buildMathSession, mathTotals, recordMathSession } from "./progress";

function finished() {
  let game = startGame(DEFAULT_SETTINGS, 0, () => 0);
  for (let i = 1; i <= 3; i++) {
    // Simulate three misses, acknowledging each correction.
    game = submitAnswer({ ...game, awaitingNext: false }, "99", i * 1000);
  }
  return game;
}

it("migrates version 1 while preserving players, spelling and lists", () => {
  const old = addPlayer(emptySave(), "Ava");
  const raw = { ...old, version: 1, players: old.players.map((p) => ({ id: p.id, name: p.name, createdAt: p.createdAt, spelling: p.spelling })) };
  const migrated = parseSaveFile(JSON.stringify(raw));
  expect(migrated).toEqual(old);
  expect(migrated.version).toBe(SAVE_VERSION);
});

it("round-trips math history, attributes it to the original player and deduplicates", () => {
  let save = addPlayer(emptySave(), "Ava");
  const ava = save.activePlayerId!;
  save = addPlayer(save, "Ben");
  const session = buildMathSession(finished());
  save = recordMathSession(recordMathSession(save, ava, session), ava, session);
  expect(parseSaveFile(serializeSave(save))).toEqual(save);
  expect(activePlayer(save)!.math.sessions).toHaveLength(0);
  expect(save.players[0].math.sessions).toHaveLength(1);
  expect(mathTotals(save.players[0])).toEqual({ rounds: 1, correct: 0, points: 0 });
  expect(() => buildMathSession(startGame(DEFAULT_SETTINGS, 0))).toThrow(/finished/);
});

it("bounds history and discards invalid math records without losing spelling data", () => {
  let save = addPlayer(emptySave(), "Ava");
  const session = buildMathSession(finished());
  for (let i = 0; i < MAX_MATH_SESSIONS + 2; i++) save = recordMathSession(save, save.activePlayerId!, { ...session, id: String(i) });
  expect(save.players[0].math.sessions).toHaveLength(MAX_MATH_SESSIONS);
  const raw = JSON.parse(serializeSave(save));
  raw.players[0].math.sessions.push(null, { settings: { operation: "division", digits: 99 } });
  expect(parseSaveFile(JSON.stringify(raw))).toEqual(save);
});
