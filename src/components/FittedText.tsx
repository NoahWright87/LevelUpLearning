"use client";

import { useLayoutEffect, useRef } from "react";

/** Small fitted-text workaround pending NoahWright87/design#39. Parent supplies fixed bounds. */
export default function FittedText({ children }: { children: string }) {
  const container = useRef<HTMLSpanElement>(null);
  const text = useRef<HTMLSpanElement>(null);

  useLayoutEffect(() => {
    const box = container.current;
    const label = text.current;
    if (!box || !label) return;
    let disposed = false;

    const fit = () => {
      if (disposed || !box.clientWidth || !box.clientHeight) return;
      let low = 0;
      let high = box.clientHeight;
      while (high - low > 0.1) {
        const size = (low + high) / 2;
        label.style.fontSize = `${size}px`;
        if (label.scrollWidth <= box.clientWidth && label.getBoundingClientRect().height <= box.clientHeight) low = size;
        else high = size;
      }
      label.style.fontSize = `${Math.floor(low * 10) / 10}px`;
    };

    fit();
    const observer = new ResizeObserver(fit);
    observer.observe(box);
    void document.fonts.ready.then(fit);
    document.fonts.addEventListener("loadingdone", fit);
    return () => {
      disposed = true;
      observer.disconnect();
      document.fonts.removeEventListener("loadingdone", fit);
    };
  }, [children]);

  return (
    <span ref={container} style={{ display: "grid", placeItems: "center", width: "100%", height: "100%", minWidth: 0, minHeight: 0, overflow: "hidden" }}>
      <span ref={text} style={{ display: "block", width: "100%", lineHeight: 1.1, overflowWrap: "anywhere", whiteSpace: "normal" }}>{children}</span>
    </span>
  );
}
