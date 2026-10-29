import { expect, it } from "vitest";
import { formatClock } from "./clock";

it("formats clocks across minute boundaries without wrapping long rounds", () => {
  expect(formatClock(0)).toBe("00:00");
  expect(formatClock(59.9)).toBe("00:59");
  expect(formatClock(60)).toBe("01:00");
  expect(formatClock(3601)).toBe("60:01");
  expect(formatClock(-1)).toBe("00:00");
});
