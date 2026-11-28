/** Shared site content constants. The words themselves live in `src/content/copy.ts`. */

import { COPY } from "@/content/copy";

export const SITE = {
  name: "Level Up Learning",
  tagline: COPY.site.tagline.text,
  description: COPY.site.description.text,
  author: "Noah Wright",
  authorUrl: "https://noahwright.dev",
  github: "https://github.com/NoahWright87/LevelUpLearning",
} as const;

/**
 * The activities, one per subject. `ready: false` shows the card as "coming soon" and serves a
 * placeholder from `/[activity]`; flip it once the activity has its own route under `src/app/`.
 */
export const ACTIVITIES = [
  { slug: "matching", title: "Matching", emoji: "🃏", description: COPY.activities.matching.description.text, ready: true },
  {
    slug: "spelling",
    title: "Spelling",
    emoji: "🔤",
    description: COPY.activities.spelling.description.text,
    ready: true,
  },
  {
    slug: "math",
    title: "Math",
    emoji: "➗",
    description: COPY.activities.math.description.text,
    ready: true,
  },
] as const;

export const NAV_ITEMS = [
  ...ACTIVITIES.map((a) => ({ label: a.title, href: `/${a.slug}` })),
  { label: "Progress", href: "/progress" },
];
