"use client";

import { useEffect } from "react";
import { Button, Heading, Text } from "@noahwright/design";
import { scoreMultiplier } from "@/lib/spelling/difficulty";
import { summarize, type QuizState } from "@/lib/spelling/quiz";
import { sayText } from "@/lib/spelling/speech";

type Props = {
  quiz: QuizState;
  voiceURI?: string;
  onAgain: () => void;
  onSetup: () => void;
};

function Stars({ value }: { value: number }) {
  return (
    <span className="spelling-stars" role="img" aria-label={`${value} out of 5 stars`}>
      {[1, 2, 3, 4, 5].map((n) => (
        <span key={n} className={value >= n ? "is-full" : value >= n - 0.5 ? "is-half" : ""} aria-hidden="true">
          ★
        </span>
      ))}
    </span>
  );
}

export default function QuizResults({ quiz, voiceURI, onAgain, onSetup }: Props) {
  const multiplier = scoreMultiplier(quiz.settings);
  const summary = summarize(quiz, multiplier);
  const percent = Math.round(summary.accuracy * 100);

  useEffect(() => {
    sayText(percent === 100 ? "All done! Perfect score!" : "All done! Let's see how you did.", { voiceURI });
  }, [percent, voiceURI]);

  // Hardest first: most misses, then fewest right on the first try.
  const words = [...quiz.words].sort((a, b) => b.attempts - b.correct - (a.attempts - a.correct));

  return (
    <div className="spelling-results">
      <Heading level={1}>🎉 All done!</Heading>
      <div className="spelling-results__score">
        <Stars value={summary.stars} />
        <Text>
          {summary.correct} right out of {summary.attempts} tries ({percent}%).
        </Text>
        <Text>
          <strong>{summary.points} points</strong>{" "}
          <Text tone="muted" inline>
            (×{multiplier.toFixed(2)} difficulty bonus: harder settings earn more)
          </Text>
        </Text>
      </div>

      <table className="spelling-table">
        <thead>
          <tr>
            <th scope="col">Word</th>
            <th scope="col">Right / tries</th>
            <th scope="col">Misspelled as</th>
          </tr>
        </thead>
        <tbody>
          {words.map((w) => (
            <tr key={w.word}>
              <td>{w.word}</td>
              <td>
                {w.correct} / {w.attempts}
              </td>
              <td>
                {Object.entries(w.misspellings)
                  .map(([spelling, n]) => (n > 1 ? `${spelling} (×${n})` : spelling))
                  .join(", ") || "—"}
              </td>
            </tr>
          ))}
        </tbody>
      </table>

      <div className="spelling-results__actions">
        <Button color="primary" onClick={onAgain}>
          Play again
        </Button>
        <Button variant="outline" onClick={onSetup}>
          Change lists or difficulty
        </Button>
      </div>
    </div>
  );
}
