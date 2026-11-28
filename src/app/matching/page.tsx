import type { Metadata } from "next";
import MatchingApp from "@/components/matching/MatchingApp";
import { COPY } from "@/content/copy";
export const metadata: Metadata = { title: "Matching", description: COPY.matching.metaDescription.text };
export default function MatchingPage() { return <MatchingApp />; }
