"use client";

import { forwardRef } from "react";

/**
 * The answer box. Uses the design system's Input classes on a plain <input>, because Input
 * can't yet take a ref or turn off spellcheck and autocorrect, and a browser that underlines
 * or autocorrects a misspelling gives the answer away. Swap back to `Input` once
 * NoahWright87/design#32 lands.
 */
const AnswerInput = forwardRef<
  HTMLInputElement,
  { label: string; value: string; onChange: (value: string) => void; disabled?: boolean }
>(function AnswerInput({ label, value, onChange, disabled }, ref) {
  return (
    <label className="nw-input spelling-answer">
      <span className="nw-input__label">{label}</span>
      <input
        ref={ref}
        className="nw-input__field"
        type="text"
        value={value}
        onChange={(e) => onChange(e.target.value)}
        disabled={disabled}
        spellCheck={false}
        autoComplete="off"
        autoCorrect="off"
        autoCapitalize="none"
        enterKeyHint="done"
      />
    </label>
  );
});

export default AnswerInput;
