/** Shared site content constants. */

export const SITE = {
  name: "Level Up Learning",
  tagline: "Practice school skills and level up, RPG-style",
  description:
    "Learning activities for kids that track your experience in each subject, so practicing spelling or math feels like leveling up a character.",
  author: "Noah Wright",
  authorUrl: "https://noahwright.dev",
  github: "https://github.com/NoahWright87/LevelUpLearning",
} as const;

/**
 * The activities, one per subject. `ready: false` shows the card as "coming soon" and serves a
 * placeholder from `/[activity]`; flip it once the activity has its own route under `src/app/`.
 */
export const ACTIVITIES = [
  {
    slug: "spelling",
    title: "Spelling",
    emoji: "🔤",
    description: "Hear a word, spell it, and level up as the words get tougher.",
    ready: true,
  },
  {
    slug: "math",
    title: "Math",
    emoji: "➗",
    description: "Think fast, build math combos, and light up the sky with fireworks.",
    ready: true,
  },
] as const;

export const NAV_ITEMS = [
  ...ACTIVITIES.map((a) => ({ label: a.title, href: `/${a.slug}` })),
  { label: "Progress", href: "/progress" },
];
