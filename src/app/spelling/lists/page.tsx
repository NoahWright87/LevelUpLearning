import type { Metadata } from "next";
import ListManager from "@/components/spelling/ListManager";

export const metadata: Metadata = {
  title: "Spelling word lists",
  description: "Upload, create and download spelling word lists.",
};

export default function SpellingListsPage() {
  return <ListManager />;
}
