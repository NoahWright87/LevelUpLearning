"use client";

import { useEffect, useState } from "react";
import { englishVoices, speechSupported, subscribeVoices } from "@/lib/spelling/speech";

/** English voices once the browser has loaded them, and whether speech works at all. */
export function useVoices() {
  const [voices, setVoices] = useState<SpeechSynthesisVoice[]>([]);
  const [supported, setSupported] = useState(true);

  useEffect(() => {
    setSupported(speechSupported());
    const refresh = () => setVoices(englishVoices());
    refresh();
    return subscribeVoices(refresh);
  }, []);

  return { voices, supported };
}
