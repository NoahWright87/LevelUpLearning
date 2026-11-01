import { describe, expect, it } from "vitest";
import { addPlayer, emptySave, parseSaveFile, serializeSave } from "../save/schema";
import { DEFAULT_SETTINGS, mathPairs, startGame } from "./game";
import { buildMatchingSession, matchingTotals, recordMatchingSession } from "./progress";

describe("matching history", () => {
  const game = startGame(mathPairs("addition"), DEFAULT_SETTINGS, 1000);
  const session = buildMatchingSession({ ...game, finishedAt: 3500, correct: 3, mistakes: 1 }, "Addition", "2026-11-01T12:00:00.000Z");
  it("requires completion and captures duration and settings", () => {
    expect(() => buildMatchingSession(game, "Addition")).toThrow(/finished/);
    expect(session).toMatchObject({ correct: 3, mistakes: 1, seconds: 2.5, source: "Addition", settings: DEFAULT_SETTINGS });
    expect(session.settings).not.toBe(game.settings);
  });
  it("records only the owner, deduplicates, and survives backup", () => {
    let save = addPlayer(emptySave(), "A");
    const owner = save.activePlayerId!;
    save = addPlayer(save, "B");
    save = recordMatchingSession(recordMatchingSession(save, owner, session), owner, session);
    expect(matchingTotals(save.players[0])).toEqual({ rounds: 1, correct: 3, mistakes: 1 });
    expect(save.players[1].matching.sessions).toEqual([]);
    expect(parseSaveFile(serializeSave(save))).toEqual(save);
  });
  it("migrates v3 without losing decks or other progress", () => {
    const save = addPlayer(emptySave(), "A");
    const old = { ...save, version: 3, players: save.players.map(p => ({ ...p, matching: undefined })) };
    expect(parseSaveFile(JSON.stringify(old))).toEqual(save);
  });
  it("bounds history and ignores removed players", () => {
    let save = addPlayer(emptySave(), "A");
    for (let i = 0; i < 502; i++) save = recordMatchingSession(save, save.activePlayerId!, { ...session, id: String(i) });
    expect(save.players[0].matching.sessions).toHaveLength(500);
    expect(save.players[0].matching.sessions[0].id).toBe("2");
    expect(recordMatchingSession(save, "deleted", session)).toEqual(save);
  });
});
