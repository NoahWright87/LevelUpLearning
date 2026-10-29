import { expect, it } from "vitest";
import { DEFAULT_SETTINGS } from "./game";
import { parseSettings } from "./storage";

it("restores chosen timings and other setup preferences", () => {
  const settings = { ...DEFAULT_SETTINGS, spawnSeconds: 7.5, reductionSeconds: 2, rows: 3 };
  expect(parseSettings(JSON.stringify(settings))).toEqual(settings);
});

it("defaults missing or damaged preferences without losing valid fields", () => {
  for (const raw of ["not json", "null", "[]"]) expect(parseSettings(raw)).toEqual(DEFAULT_SETTINGS);
  expect(parseSettings('{"rows":4}')).toEqual({ ...DEFAULT_SETTINGS, rows: 4 });
  expect(parseSettings('{"spawnSeconds":"bad","reductionSeconds":null}')).toEqual(DEFAULT_SETTINGS);
});

it("bounds untrusted tuning values while allowing zero and fractional seconds", () => {
  expect(parseSettings('{"spawnSeconds":-10,"reductionSeconds":999}')).toMatchObject({ spawnSeconds: 0, reductionSeconds: 120 });
  expect(parseSettings('{"spawnSeconds":2.57,"reductionSeconds":0}')).toMatchObject({ spawnSeconds: 2.6, reductionSeconds: 0 });
});
