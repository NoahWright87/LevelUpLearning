"use client";

import NextLink from "next/link";
import { usePathname } from "next/navigation";
import {
  Header,
  SceneFooter,
  Layout,
  Container,
  Link,
  MobileNav,
  type SceneEffects,
} from "@noahwright/design";
import SettingsMenu from "@/components/SettingsMenu";
import Logo from "@/components/Logo";
import { NAV_ITEMS, SITE } from "@/lib/site";

// Same horizon scene as noahwright.dev: sun and moon toward the middle so their reflections
// stay clear of the footer text on either side. Module-level so the reference is stable.
const FOOTER_SCENE_OPTIONS = { sunX: 0.62, moonX: 0.38 };

export default function SiteShell({ children, effects, compactScene = false }: {
  children: React.ReactNode;
  effects?: SceneEffects;
  compactScene?: boolean;
}) {
  const pathname = usePathname();

  return (
    <Layout
      header={
        <Header
          shadow={false}
          left={
            <Link href="/" className="site-home-link">
              <Logo />
              <strong className="site-name">{SITE.name}</strong>
            </Link>
          }
          right={
            <>
              <MobileNav label="Site menu">
                {NAV_ITEMS.map((item) => (
                  <NextLink
                    key={item.href}
                    href={item.href}
                    aria-current={pathname === item.href ? "page" : undefined}
                  >
                    {item.label}
                  </NextLink>
                ))}
              </MobileNav>
              <SettingsMenu />
            </>
          }
        />
      }
      footer={
        <SceneFooter
          scene="horizon"
          effects={effects}
          skyHeight={compactScene ? "clamp(64px, 18svh, 144px)" : undefined}
          seaHeight={compactScene ? "clamp(48px, 10svh, 80px)" : undefined}
          sceneOptions={FOOTER_SCENE_OPTIONS}
          left={
            <span>
              © {new Date().getFullYear()} <a href={SITE.authorUrl}>{SITE.author}</a>
            </span>
          }
          right={
            <a href={SITE.github} target="_blank" rel="noreferrer">
              GitHub
            </a>
          }
        />
      }
    >
      <Container direction="vertical" alignItems="center" padding="none" noGutters>
        <Container direction="vertical" fullWidth={false} width="min(100%, 1000px)" padding="none" noGutters>
          {children}
        </Container>
      </Container>
    </Layout>
  );
}
