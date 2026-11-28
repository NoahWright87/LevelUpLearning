import type { Metadata } from "next";
import SpellingQuizApp from "@/components/spelling/SpellingQuizApp";
import { COPY } from "@/content/copy";

export const metadata: Metadata = {
  title: "Spelling",
  description: COPY.spelling.metaDescription.text,
};

export default function SpellingPage() {
  return <SpellingQuizApp />;
}
