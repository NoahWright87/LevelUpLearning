import type { Metadata } from "next";
import MatchingApp from "@/components/matching/MatchingApp";
export const metadata: Metadata = { title: "Matching", description: "Speed drills for math facts and your own matching cards." };
export default function MatchingPage() { return <MatchingApp />; }
