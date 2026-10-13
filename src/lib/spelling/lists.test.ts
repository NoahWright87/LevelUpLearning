import { describe, expect, it } from "vitest";
import { BUILT_IN_LISTS } from "./builtInLists";
import { ListFileError, parseListFile, parseQuickList, serializeLists, withPlaceholder } from "./lists";

describe("parseListFile", () => {
  it("round-trips its own download format", () => {
    const text = serializeLists(BUILT_IN_LISTS.slice(0, 2));
    expect(parseListFile(text)).toEqual(BUILT_IN_LISTS.slice(0, 2));
  });

  it("accepts a bare list or an array, with words as plain strings", () => {
    expect(parseListFile(JSON.stringify({ name: "Week 1", words: ["rain", { word: "cake" }] }))).toEqual([
      { id: "week-1", name: "Week 1", grade: undefined, words: [{ word: "rain", sentence: "" }, { word: "cake", sentence: "" }] },
    ]);
    expect(parseListFile(JSON.stringify([{ name: "A", words: ["x"] }, { name: "A", words: ["y"] }])).map((l) => l.id)).toEqual([
      "a",
      "a-2",
    ]);
  });

  it("renames ids that are already taken", () => {
    const [list] = parseListFile(JSON.stringify({ id: "grade-1-list-1", name: "Grade 1 List 1", words: ["no"] }), [
      "grade-1-list-1",
    ]);
    expect(list.id).toBe("grade-1-list-1-2");
  });

  it("explains what's wrong with a bad file", () => {
    expect(() => parseListFile("not json")).toThrow(ListFileError);
    expect(() => parseListFile(JSON.stringify({ lists: [] }))).toThrow(/No spelling lists/);
    expect(() => parseListFile(JSON.stringify({ words: ["a"] }))).toThrow(/needs a "name"/);
    expect(() => parseListFile(JSON.stringify({ name: "Empty", words: [] }))).toThrow(/has no words/);
  });
});

describe("withPlaceholder", () => {
  it("swaps the word for %% when the sentence spells it out", () => {
    expect(withPlaceholder("cake", "I ate the Cake today.")).toBe("I ate the %% today.");
    expect(withPlaceholder("cake", "Your %% is ready.")).toBe("Your %% is ready.");
    expect(withPlaceholder("cake", "No match here.")).toBe("No match here.");
  });
});

describe("parseQuickList", () => {
  it("reads one word per line with optional sentences", () => {
    expect(parseQuickList("cake - I ate the cake.\n\nice-cream: Cold ice-cream!\nfriend")).toEqual([
      { word: "cake", sentence: "I ate the %%." },
      { word: "ice-cream", sentence: "Cold %%!" },
      { word: "friend", sentence: "" },
    ]);
  });
});

describe("built-in lists", () => {
  it("has every list from the old app, each word with a placeholder sentence", () => {
    expect(BUILT_IN_LISTS).toHaveLength(22);
    expect(BUILT_IN_LISTS.flatMap((l) => l.words)).toHaveLength(390);
    expect(BUILT_IN_LISTS.every((l) => l.words.every((w) => w.sentence.includes("%%")))).toBe(true);
  });
});
