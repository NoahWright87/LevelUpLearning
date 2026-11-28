/**
 * Site copy: the words people read on learning.noahwright.dev, kept in one place so they are
 * easy to find and rewrite.
 *
 * Every entry says who wrote it:
 *   - `author: "ai"`   — drafted by an AI assistant. Fair game for a rewrite.
 *   - `author: "noah"` — written (or rewritten and approved) by Noah. Leave it alone.
 *
 * When Noah rewrites an entry, change its `text` and flip `author` to "noah".
 * If he reads an AI entry and is happy with it as is, flipping `author` is enough.
 * New copy goes here too, marked "ai" until he's had a go at it.
 *
 * What's here: page titles and descriptions, headings, intros, how-to-play text, empty states,
 * call-to-action buttons and messages on the site's own pages. What isn't: one-word game
 * controls (Check, Quit, Clear...), in-game feedback and results screens, and game data.
 *
 * Copy that lives elsewhere and isn't marked yet:
 *   - `src/lib/site.ts`               (site name, activity names; descriptions come from here)
 *   - `src/lib/spelling/difficulty.ts` (difficulty labels and hints)
 *   - `src/lib/spelling/speech.ts`     (spoken cheers) and `src/lib/spelling/combo.ts` (combo cheers)
 *   - `src/data/spellingLists.json`    (built-in word lists and sentences: game data)
 *   - `src/components/SettingsMenu.tsx` (copied from noahwright.dev; kept in step with it)
 * Not moved yet (open PR):
 *   - `src/components/ProgressPage.tsx` (the /progress page itself)
 *   - `src/components/matching/MatchingApp.tsx` and `src/lib/matching/*` (the /matching game)
 *
 * Plain data only: no React, no imports, so tools and tests can read it.
 */

export type Author = "ai" | "noah";

export interface CopyEntry {
  readonly author: Author;
  readonly text: string;
}

export const COPY = {
  site: {
    tagline: { author: "ai", text: "Practice school skills and level up, RPG-style" },
    /** Home page intro and the default meta description. */
    description: {
      author: "ai",
      text: "Learning activities for kids that track your experience in each subject, so practicing spelling or math feels like leveling up a character.",
    },
  },

  home: {
    activitiesHeading: { author: "ai", text: "Pick an activity" },
    comingSoon: { author: "ai", text: "Coming soon" },
  },

  /** Activity card blurbs on the home page (and the placeholder page for unfinished ones). */
  activities: {
    matching: {
      description: {
        author: "ai",
        text: "Match quick math facts or your own cards in a race against the clock.",
      },
    },
    spelling: {
      description: { author: "ai", text: "Hear a word, spell it, and level up as the words get tougher." },
    },
    math: {
      description: {
        author: "ai",
        text: "Think fast, build math combos, and light up the sky with fireworks.",
      },
    },
  },

  /** Shown at `/[activity]` for activities that aren't built yet. */
  placeholder: {
    stillBuilding: { author: "ai", text: "🚧 This activity is still being built. Check back soon!" },
    backButton: { author: "ai", text: "Back to activities" },
  },

  players: {
    whoIsPlaying: { author: "ai", text: "Who's playing?" },
    skipHint: { author: "ai", text: "Or skip this: results save as “Player 1”." },
  },

  matching: {
    metaDescription: { author: "ai", text: "Speed drills for math facts and your own matching cards." },
  },

  math: {
    metaTitle: { author: "ai", text: "Math sprint" },
    metaDescription: {
      author: "ai",
      text: "Practice addition and multiplication, build quick combos, and light up the sky with fireworks.",
    },
    kicker: { author: "ai", text: "THINK FAST. BUILD YOUR STREAK." },
    heading: { author: "ai", text: "Math sprint" },
    intro: {
      author: "ai",
      text: "Little problems. Big combos. Light up the sky as you learn your math facts.",
    },
    /** How to play: each rule is a bold lead-in, then the rest of the sentence. */
    rules: {
      hearts: {
        lead: { author: "ai", text: "♥ Three hearts." },
        body: { author: "ai", text: "Each wrong answer costs one and resets your combo." },
      },
      timer: {
        lead: { author: "ai", text: "Five seconds to keep it going." },
        body: {
          author: "ai",
          text: "Each correct answer refills the bar. An empty bar resets your combo, with no heart lost.",
        },
      },
      points: {
        lead: { author: "ai", text: "10 points × your combo." },
        body: {
          author: "ai",
          text: "Keep answering quickly for bigger scores and more fireworks. Take your time whenever you need to.",
        },
      },
    },
    playButton: { author: "ai", text: "Let’s play →" },
  },

  progress: {
    metaDescription: { author: "ai", text: "Players, quiz history, and the save file." },
  },

  spelling: {
    metaDescription: { author: "ai", text: "Hear a word, spell it, and level up as the words get tougher." },
    heading: { author: "ai", text: "🔤 Spelling" },
    intro: {
      author: "ai",
      text: "Listen to each word, then type how it's spelled. Pick some lists and a difficulty to begin.",
    },
    listsHeading: { author: "ai", text: "1. Pick word lists" },
    listsLink: { author: "ai", text: "Upload or download lists →" },
    noListsPicked: { author: "ai", text: "No lists picked yet" },
    difficultyHeading: { author: "ai", text: "2. Choose a difficulty" },
    customDifficulty: { author: "ai", text: "Custom: set it up yourself" },
    soundHeading: { author: "ai", text: "3. Check the sound" },
    testVoiceButton: { author: "ai", text: "🔊 Test the voice" },
    /** Spoken by "Test the voice"; `%%` is where the word "spelling" goes. */
    testVoiceSentence: { author: "ai", text: "I love practicing my %% words!" },
    noSpeech: {
      author: "ai",
      text: "This browser can't speak the words aloud. Use a browser with text-to-speech support for the spoken quiz.",
    },
    startButton: { author: "ai", text: "Start the quiz" },
  },

  spellingLists: {
    metaTitle: { author: "ai", text: "Spelling word lists" },
    metaDescription: { author: "ai", text: "Upload, create and download spelling word lists." },
    backLink: { author: "ai", text: "← Back to spelling" },
    heading: { author: "ai", text: "📚 Word lists" },
    /** One sentence with a link to /progress in the middle: `before` [link] `after`. */
    intro: {
      before: { author: "ai", text: "Lists you add are saved in this browser and included in your" },
      link: { author: "ai", text: "save file" },
      after: { author: "ai", text: ". Download a list here to share it on its own." },
    },
    uploadHeading: { author: "ai", text: "Upload lists" },
    uploadIntro: { author: "ai", text: "Pick one or more .json files, like ones downloaded from this page." },
    formatSummary: { author: "ai", text: "What goes in the file?" },
    quickCreateHeading: { author: "ai", text: "Quick-create a list" },
    wordsLabel: { author: "ai", text: "Words: one per line, optionally with a sentence after a dash" },
    createButton: { author: "ai", text: "Create list" },
    myListsHeading: { author: "ai", text: "My lists" },
    noListsYet: { author: "ai", text: "No lists of your own yet. Upload or create one above." },
    builtInHeading: { author: "ai", text: "Built-in lists" },
    notices: {
      saveFailed: {
        author: "ai",
        text: "This browser wouldn't save the lists (private browsing or storage full).",
      },
      /** `{names}` is the quoted list names. */
      added: { author: "ai", text: "Added {names}. They're ready to pick on the spelling page." },
      needName: { author: "ai", text: "Give the list a name." },
      needWord: { author: "ai", text: "Add at least one word." },
      /** `{name}` is the list's name. */
      confirmDelete: { author: "ai", text: "Delete “{name}”? Download it first if you might want it back." },
      deleted: { author: "ai", text: "Deleted “{name}”." },
    },
  },
} as const satisfies CopyTree;

/** Any nesting of entries, arrays of entries and groups. */
type CopyTree = { readonly [key: string]: CopyEntry | readonly CopyEntry[] | CopyTree };

/** Fill `{name}` placeholders: `fill(COPY.spellingLists.notices.deleted, { name: "Week 1" })`. */
export function fill(entry: CopyEntry, values: Record<string, string | number>): string {
  return entry.text.replace(/\{(\w+)\}/g, (match, name: string) =>
    name in values ? String(values[name]) : match,
  );
}
