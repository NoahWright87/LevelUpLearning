import type { Metadata } from "next";
import ProgressPage from "@/components/ProgressPage";

export const metadata: Metadata = {
  title: "Progress",
  description: "Players, quiz history, and the save file.",
};

export default function Progress() {
  return <ProgressPage />;
}
