"use client";

import { useCallback, useEffect, useRef, useState } from "react";

/** One monotonic browser clock. Activities decide what a tick means and when the clock runs. */
export function useActivityClock(running: boolean, onTick?: (now: number) => void) {
  const [now, setNow] = useState(0);
  const tick = useRef(onTick);
  useEffect(() => { tick.current = onTick; }, [onTick]);

  // Inputs sample the same clock independently of delayed/background interval callbacks.
  const sample = useCallback(() => {
    const time = performance.now();
    setNow(time);
    return time;
  }, []);

  useEffect(() => {
    if (!running) return;
    const timer = window.setInterval(() => {
      const time = sample();
      tick.current?.(time);
    }, 50);
    return () => window.clearInterval(timer);
  }, [running, sample]);

  return { now, sample };
}
