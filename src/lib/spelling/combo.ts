/** A cheer for consecutive answers, growing sillier as the hot streak builds. */
export function comboCheer(combo: number): string | null {
  if (combo < 2) return null;
  if (combo < 5) return `🔥 ${combo} in a row!`;
  if (combo < 10) return `🔥🔥 ${combo} in a row! Wow!`;
  if (combo < 20) return `🔥🔥🔥 ${combo} in a row! Unstoppable!`;
  if (combo < 50) return `🔥🔥🔥🔥 ${combo} in a row! Legendary!`;
  return `🔥🔥🔥🔥🔥 ${combo} in a row! Absolutely ridiculous!`;
}
