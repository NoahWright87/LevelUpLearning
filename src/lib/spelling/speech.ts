/**
 * Text to speech through the browser's Web Speech API. Every call cancels whatever is still
 * being said, so a quick tap on "Hear it again" never queues up a backlog.
 */

import { PLACEHOLDER } from "./lists";

const CONGRATS = [
  "You did it!",
  "Good job!",
  "That's right!",
  "Hooray!",
  "Way to go!",
  "Super!",
  "Fantastic!",
  "Correct!",
  "Amazing!",
  "Great job!",
  "You're a genius!",
  "Another one!",
  "Incredible!",
  "Couldn't have done better myself!",
];

export type SpeechOptions = { voiceURI?: string; rate?: number };

export function speechSupported(): boolean {
  return typeof window !== "undefined" && "speechSynthesis" in window && "SpeechSynthesisUtterance" in window;
}

/** English voices, the browser's default first. Empty until the browser has loaded them. */
export function englishVoices(): SpeechSynthesisVoice[] {
  if (!speechSupported()) return [];
  return window.speechSynthesis
    .getVoices()
    .filter((v) => v.lang.toLowerCase().startsWith("en"))
    .sort((a, b) => Number(b.default) - Number(a.default));
}

/** Calls `onChange` whenever the voice list changes (browsers load voices asynchronously). */
export function subscribeVoices(onChange: () => void): () => void {
  if (!speechSupported()) return () => {};
  window.speechSynthesis.addEventListener("voiceschanged", onChange);
  return () => window.speechSynthesis.removeEventListener("voiceschanged", onChange);
}

export function stopSpeaking(): void {
  if (speechSupported()) window.speechSynthesis.cancel();
}

/** Says each part in order, a beat apart. Slow parts (the word itself) are said a little slower. */
function say(parts: Array<{ text: string; slow?: boolean }>, options: SpeechOptions): void {
  if (!speechSupported()) return;
  const synth = window.speechSynthesis;
  synth.cancel();
  const voice = englishVoices().find((v) => v.voiceURI === options.voiceURI) ?? englishVoices()[0];
  const rate = options.rate ?? 0.9;
  for (const { text, slow } of parts) {
    if (!text.trim()) continue;
    const utterance = new SpeechSynthesisUtterance(text.replace(/&/g, " and "));
    if (voice) utterance.voice = voice;
    utterance.lang = voice?.lang ?? "en-US";
    utterance.rate = slow ? rate * 0.8 : rate;
    synth.speak(utterance);
  }
}

/** "Cake. Blow out the candles on your cake." */
export function sayWordPrompt(word: string, sentence: string, options: SpeechOptions = {}): void {
  say(
    [
      { text: word, slow: true },
      { text: sentence.split(PLACEHOLDER).join(word) },
    ],
    options,
  );
}

export function sayCongrats(options: SpeechOptions = {}): void {
  say([{ text: CONGRATS[Math.floor(Math.random() * CONGRATS.length)] }], options);
}

/** "The word was cake. C, A, K, E." Spelling it out helps more than reading back the mistake. */
export function sayCorrection(word: string, options: SpeechOptions = {}): void {
  const letters = [...word].filter((ch) => /\p{L}/u.test(ch)).map((ch) => ch.toUpperCase());
  say(
    [
      { text: `The word was ${word}.` },
      { text: letters.join(", "), slow: true },
    ],
    options,
  );
}

export function sayText(text: string, options: SpeechOptions = {}): void {
  say([{ text }], options);
}
