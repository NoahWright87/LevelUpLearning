import type { Metadata } from "next";
import ListManager from "@/components/spelling/ListManager";
import { COPY } from "@/content/copy";

export const metadata: Metadata = {
  title: COPY.spellingLists.metaTitle.text,
  description: COPY.spellingLists.metaDescription.text,
};

export default function SpellingListsPage() {
  return <ListManager />;
}
