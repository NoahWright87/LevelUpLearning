"use client";

import { useEffect, useRef, type ReactNode } from "react";
import { Button, Container, Heading, Text, type SceneEffects } from "@noahwright/design";
import SiteShell from "@/components/SiteShell";
import { formatClock } from "@/lib/activity/clock";
import "./activity.css";

/** Shared site frame for a game; the activity owns its screens and rules. */
export function ActivityPage({ children, effects, focused = false }: { children: ReactNode; effects?: SceneEffects; focused?: boolean }) {
  return (
    <SiteShell effects={effects} compactScene focused={focused}>
      <Container padding="sm" alignItems="center" noGutters>
        <Container padding="none" itemSpacing="none" width="min(100%, 700px)" fullWidth={false} noGutters>
          {children}
        </Container>
      </Container>
    </SiteShell>
  );
}

export function ActivityToolbar({ title, onQuit, className = "" }: {
  title: ReactNode; onQuit: () => void; className?: string;
}) {
  return (
    <div className={`activity-toolbar ${className}`}>
      <strong>{title}</strong>
      <Button size="small" variant="text" onClick={onQuit}>Quit</Button>
    </div>
  );
}

/** Compact game HUD; shared statistics presentation tracks NoahWright87/design#38. */
export function ActivityBanner({ score, scoreLabel, seconds, clockLabel, onExit }: {
  score: ReactNode; scoreLabel: string; seconds: number; clockLabel: string; onExit: () => void;
}) {
  return (
    <div className="activity-banner" role="group" aria-label="Round status">
      <output aria-label={scoreLabel}>{score}</output>
      <span className="activity-clock" role="timer" aria-label={clockLabel}>{formatClock(seconds)}</span>
      <Button variant="text" size="small" style={{ padding: 0, width: 36, height: 36, minWidth: 36 }} onClick={onExit}>
        <span aria-hidden="true">X</span><span className="activity-sr-only">Exit game</span>
      </Button>
    </div>
  );
}

type Stat = { label: string; value: ReactNode };

/** Small local substitute until NoahWright87/design#38 provides a statistics group. */
export function ActivityStats({ items, className = "" }: { items: Stat[]; className?: string }) {
  return (
    <dl className={`activity-stats ${className}`}>
      {items.map(({ label, value }) => (
        <div key={label}><dt>{label}</dt><dd>{value}</dd></div>
      ))}
    </dl>
  );
}

export function ActivityFeedback({ children, className = "" }: { children: ReactNode; className?: string }) {
  return <div className={`activity-feedback ${className}`} role="status" aria-live="polite" aria-atomic="true">{children}</div>;
}

/** Focus and replay actions are consistent; results content stays activity-specific. */
export function ActivityResults({ title, description, children, onAgain, onSetup, setupLabel = "Change setup" }: {
  title: ReactNode; description?: ReactNode; children: ReactNode;
  onAgain: () => void; onSetup: () => void; setupLabel?: string;
}) {
  const heading = useRef<HTMLDivElement>(null);
  useEffect(() => { heading.current?.focus({ preventScroll: true }); }, []);

  return (
    <div className="activity-stack">
      <div ref={heading} tabIndex={-1}>
        <Heading level={1}>{title}</Heading>
        {description && <Text>{description}</Text>}
      </div>
      {children}
      <div className="activity-actions">
        <Button color="primary" onClick={onAgain}>Play again →</Button>
        <Button variant="outline" onClick={onSetup}>{setupLabel}</Button>
      </div>
    </div>
  );
}
