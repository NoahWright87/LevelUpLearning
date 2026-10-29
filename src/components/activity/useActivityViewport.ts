"use client";

import { useLayoutEffect, useRef } from "react";

/** Focused-layout workaround pending NoahWright87/design#41; includes mobile browser chrome. */
export function useActivityViewport(focused: boolean) {
  const ref = useRef<HTMLDivElement>(null);
  useLayoutEffect(() => {
    const root = ref.current;
    const content = root?.querySelector<HTMLElement>(".nw-layout__content");
    if (!focused || !root || !content) return;
    const viewport = window.visualViewport;
    const measureContent = () => {
      root.style.setProperty("--activity-content-height", `${content.getBoundingClientRect().height}px`);
    };
    const resize = () => {
      root.style.setProperty("--activity-viewport-height", `${viewport?.height ?? window.innerHeight}px`);
      measureContent();
    };
    resize();
    const observer = new ResizeObserver(measureContent);
    observer.observe(content);
    viewport?.addEventListener("resize", resize);
    window.addEventListener("resize", resize);
    return () => {
      observer.disconnect();
      viewport?.removeEventListener("resize", resize);
      window.removeEventListener("resize", resize);
      root.style.removeProperty("--activity-viewport-height");
      root.style.removeProperty("--activity-content-height");
    };
  }, [focused]);
  return ref;
}
