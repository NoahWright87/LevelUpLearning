/** Device setup preferences, separate from the portable save file's decks and progress. */
import { DEFAULT_SETTINGS, normalizeSettings, type Settings } from "./game";

const SETTINGS_KEY = "lul.matching.settings.v1";

export function parseSettings(text: string): Settings {
  try {
    const raw: unknown = JSON.parse(text);
    if (raw && typeof raw === "object" && !Array.isArray(raw)) {
      return normalizeSettings({ ...DEFAULT_SETTINGS, ...raw } as Settings);
    }
  } catch {
    // Invalid or old device preferences must not prevent a game from starting.
  }
  return { ...DEFAULT_SETTINGS };
}

export function loadSettings(): Settings {
  try { return parseSettings(window.localStorage.getItem(SETTINGS_KEY) ?? "null"); }
  catch { return { ...DEFAULT_SETTINGS }; }
}

export function saveSettings(settings: Settings): void {
  try { window.localStorage.setItem(SETTINGS_KEY, JSON.stringify(normalizeSettings(settings))); }
  catch { /* Settings still work for this visit if browser storage is unavailable. */ }
}
