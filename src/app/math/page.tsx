import type { Metadata } from "next";
import MathGameApp from "@/components/math/MathGameApp";
import { COPY } from "@/content/copy";

export const metadata: Metadata = {
  title: COPY.math.metaTitle.text,
  description: COPY.math.metaDescription.text,
};

export default function MathPage() {
  return <MathGameApp />;
}
