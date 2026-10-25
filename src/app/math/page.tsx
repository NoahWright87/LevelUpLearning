import type { Metadata } from "next";
import MathGameApp from "@/components/math/MathGameApp";

export const metadata: Metadata = {
  title: "Math sprint",
  description: "Practice addition and multiplication, build quick combos, and light up the sky with fireworks.",
};

export default function MathPage() {
  return <MathGameApp />;
}
