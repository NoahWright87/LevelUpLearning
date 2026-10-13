import type { Metadata } from "next";
import SpellingQuizApp from "@/components/spelling/SpellingQuizApp";

export const metadata: Metadata = {
  title: "Spelling",
  description: "Hear a word, spell it, and level up as the words get tougher.",
};

export default function SpellingPage() {
  return <SpellingQuizApp />;
}
