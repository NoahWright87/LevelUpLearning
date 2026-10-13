import { logoSvg } from "@/lib/logo";

export function GET() {
  return new Response(logoSvg(), {
    headers: { "Content-Type": "image/svg+xml" },
  });
}
