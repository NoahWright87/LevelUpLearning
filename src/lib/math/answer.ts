export const ANSWER_LIMIT = 12;
export const NUMPAD_KEYS = ["7", "8", "9", "4", "5", "6", "1", "2", "3", ".", "0"] as const;

/** Numeric answers allow a decimal, but never exponent notation or incomplete input. */
export function validAnswer(value: string): boolean {
  const text = value.trim();
  return /^(?:\d+(?:\.\d*)?|\.\d+)$/.test(text)
    && Number.isFinite(Number(text)) && Number(text) <= Number.MAX_SAFE_INTEGER;
}

export function keypadEntry(value: string, key: string): string {
  if (key === "backspace") return value.slice(0, -1);
  if (!/^[0-9.]$/.test(key) || value.length >= ANSWER_LIMIT || (key === "." && value.includes("."))) return value;
  return value === "" && key === "." ? "0." : value + key;
}
