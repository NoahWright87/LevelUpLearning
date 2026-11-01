import { expect, it } from "vitest";
import { ANSWER_LIMIT, keypadEntry, validAnswer } from "./answer";

it("edits numeric entries without duplicate decimals, handles backspace and bounds length", () => {
  expect(keypadEntry("", ".")).toBe("0.");
  expect(keypadEntry("12.3", ".")).toBe("12.3");
  expect(keypadEntry("12.3", "backspace")).toBe("12.");
  expect(keypadEntry("", "backspace")).toBe("");
  expect(keypadEntry("", "0")).toBe("0");
  expect(keypadEntry("12", "3")).toBe("123");
  expect(keypadEntry("12", "a")).toBe("12");
  expect(keypadEntry("9".repeat(ANSWER_LIMIT), "0")).toHaveLength(ANSWER_LIMIT);
});

it("accepts ordinary decimal numbers and rejects malformed or unsafe values", () => {
  for (const value of ["0", "0.", ".5", "3.0", "2.5", " 12 "]) expect(validAnswer(value)).toBe(true);
  for (const value of ["", " ", ".", "1.2.3", "1e2", "Infinity", "-1", "9007199254740992"]) expect(validAnswer(value)).toBe(false);
});
