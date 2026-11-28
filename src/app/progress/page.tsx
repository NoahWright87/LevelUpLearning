import type { Metadata } from "next";
import ProgressPage from "@/components/ProgressPage";
import { COPY } from "@/content/copy";

export const metadata: Metadata = {
  title: "Progress",
  description: COPY.progress.metaDescription.text,
};

export default function Progress() {
  return <ProgressPage />;
}
