// One-off: converts the old desktop app's XML spelling lists (archive/Spelling Lists)
// into src/data/spellingLists.json, the web app's built-in lists. Kept as a record of
// how that file was made; delete it along with archive/.
import { readdirSync, readFileSync, writeFileSync } from "node:fs";
import { join } from "node:path";

const SRC = "archive/Spelling Lists";
const OUT = "src/data/spellingLists.json";

const decode = (s) =>
  s.replace(/&lt;/g, "<").replace(/&gt;/g, ">").replace(/&quot;/g, '"').replace(/&apos;/g, "'").replace(/&amp;/g, "&");
const tag = (xml, name) => decode(xml.match(new RegExp(`<${name}>([\\s\\S]*?)</${name}>`))?.[1] ?? "").trim();

const lists = [];
for (const folder of readdirSync(SRC).sort()) {
  const grade = Number(folder.match(/Grade (\d+)/)?.[1]);
  const files = readdirSync(join(SRC, folder)).sort((a, b) => a.localeCompare(b, undefined, { numeric: true }));
  for (const file of files) {
    const xml = readFileSync(join(SRC, folder, file), "utf8");
    const name = tag(xml, "ListName");
    const words = [...xml.matchAll(/<SpellingWord>([\s\S]*?)<\/SpellingWord>/g)].map(([, w]) => ({
      word: tag(w, "Word"),
      sentence: tag(w, "Prompt"),
    }));
    lists.push({ id: name.toLowerCase().replace(/[^a-z0-9]+/g, "-"), name, grade, words });
  }
}

writeFileSync(OUT, JSON.stringify({ format: "level-up-learning.spelling-lists", version: 1, lists }, null, 2) + "\n");
console.log(`Wrote ${lists.length} lists, ${lists.reduce((n, l) => n + l.words.length, 0)} words to ${OUT}`);
