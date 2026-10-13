import { describe, expect, it } from "vitest";
import { PRESETS } from "../spelling/difficulty";
import { isDone, nextWord, startQuiz, submitAnswer, type QuizState } from "../spelling/quiz";
import type { SpellingList } from "../spelling/lists";
import {
  activePlayer,
  addPlayer,
  emptySave,
  ensurePlayer,
  parseSaveFile,
  removePlayer,
  SaveFileError,
  serializeSave,
} from "./schema";
import { buildSession, listBests, MAX_SESSIONS, recordSession, totals, trickyWords } from "./spellingProgress";

const LISTS: SpellingList[] = [
  { id: "a", name: "List A", words: [{ word: "cake", sentence: "" }, { word: "rain", sentence: "" }] },
  { id: "b", name: "List B", words: [{ word: "rain", sentence: "" }, { word: "house", sentence: "" }] },
];

/** Plays a quiz to the end, missing `miss` once first. */
function play(miss?: string): QuizState {
  let rng = 0.42;
  const next = () => (rng = (rng * 9301 + 49297) % 233280 / 233280);
  let state = startQuiz(LISTS.flatMap((l) => l.words), { ...PRESETS.easy.settings, timesToFinish: 1 }, next);
  let missed = false;
  while (!isDone(state)) {
    const word = state.words[state.current!].word;
    const wrong: boolean = word === miss && !missed;
    missed ||= wrong;
    state = nextWord(submitAnswer(state, wrong ? "oops" : word).state, next);
  }
  return state;
}

describe("save file", () => {
  it("round-trips through serialize and parse", () => {
    let save = addPlayer(emptySave(), "Ava");
    save = recordSession(save, save.activePlayerId!, buildSession(play("rain"), LISTS, "easy"));
    save = { ...save, spelling: { lists: [{ id: "mine", name: "Mine", words: [{ word: "x", sentence: "" }] }] } };
    expect(parseSaveFile(serializeSave(save))).toEqual(save);
  });

  it("rejects other files with a helpful message", () => {
    expect(() => parseSaveFile("nope")).toThrow(SaveFileError);
    expect(() => parseSaveFile(JSON.stringify({ format: "level-up-learning.spelling-lists", lists: [] }))).toThrow(
      /word list file/,
    );
    expect(() => parseSaveFile(JSON.stringify({ name: "Week 1", words: ["rain"] }))).toThrow(/word list file/);
    expect(() => parseSaveFile(JSON.stringify({ format: "level-up-learning.save", version: 99 }))).toThrow(/newer/);
  });

  it("keeps the valid parts of a damaged file", () => {
    const save = parseSaveFile(
      JSON.stringify({
        format: "level-up-learning.save",
        version: 1,
        activePlayerId: "gone",
        players: [
          { id: "p", name: "Ava", spelling: { sessions: [{ attempts: -3, words: [{ word: "cake", correct: 2 }, { bad: 1 }] }, "junk"] } },
          { name: "" },
        ],
        spelling: { lists: [{ id: "grade-1-list-1", name: "Clash", words: ["x"] }] },
      }),
    );
    expect(save.players).toHaveLength(1);
    expect(save.activePlayerId).toBe("p");
    const [session] = save.players[0].spelling.sessions;
    expect(session.attempts).toBe(0);
    expect(session.words).toEqual([{ word: "cake", attempts: 0, correct: 2, misspellings: {} }]);
    // A saved list can't take over a built-in list's id.
    expect(save.spelling.lists[0].id).not.toBe("grade-1-list-1");
  });
});

describe("players", () => {
  it("creates Player 1 only when there's nobody", () => {
    const save = ensurePlayer(emptySave());
    expect(save.players.map((p) => p.name)).toEqual(["Player 1"]);
    expect(ensurePlayer(save)).toBe(save);
  });

  it("moves to another player when the active one is deleted", () => {
    let save = addPlayer(addPlayer(emptySave(), "Ava"), "Ben");
    save = removePlayer(save, save.activePlayerId!);
    expect(activePlayer(save)?.name).toBe("Ava");
  });
});

describe("spelling progress", () => {
  it("scores each list on its own words", () => {
    const session = buildSession(play("cake"), LISTS, "easy");
    const [a, b] = session.lists;
    expect(a).toMatchObject({ id: "a", attempts: 3, correct: 2 });
    expect(b).toMatchObject({ id: "b", attempts: 2, correct: 2, stars: 5 });
  });

  it("works out bests, totals and tricky words from history", () => {
    let save = addPlayer(emptySave(), "Ava");
    const id = save.activePlayerId!;
    save = recordSession(save, id, buildSession(play("cake"), LISTS, "easy"));
    save = recordSession(save, id, buildSession(play(), LISTS, "easy"));
    const player = activePlayer(save);
    expect(listBests(player).get("a")).toMatchObject({ stars: 5, plays: 2 });
    expect(totals(player)).toMatchObject({ sessions: 2, words: 3 });
    expect(trickyWords(player).map((w) => w.word)).toEqual(["cake"]);
    expect(trickyWords(player)[0].misspellings).toEqual({ oops: 1 });
  });

  it("keeps only the newest sessions", () => {
    let save = addPlayer(emptySave(), "Ava");
    const session = buildSession(play(), LISTS, "easy");
    for (let i = 0; i < MAX_SESSIONS + 3; i++) save = recordSession(save, save.activePlayerId!, { ...session, id: String(i) });
    const sessions = activePlayer(save)!.spelling.sessions;
    expect(sessions).toHaveLength(MAX_SESSIONS);
    expect(sessions.at(-1)!.id).toBe(String(MAX_SESSIONS + 2));
  });
});
