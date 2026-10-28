"use client";

import { useEffect } from "react";
import NextLink from "next/link";
import { Text } from "@noahwright/design";
import { ActivityResults } from "@/components/activity/ActivityUI";
import { scoreMultiplier } from "@/lib/spelling/difficulty";
import { summarize, type QuizState } from "@/lib/spelling/quiz";
import { sayText } from "@/lib/spelling/speech";
import type { SavedResult } from "./SpellingQuizApp";

type Props = {
  quiz: QuizState;
  voiceURI?: string;
  saved: SavedResult;
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

export default function QuizResults({ quiz, voiceURI, saved, onAgain, onSetup }: Props) {
  const multiplier = scoreMultiplier(quiz.settings);
  const summary = summarize(quiz, multiplier);
  const percent = Math.round(summary.accuracy * 100);

  useEffect(() => {
    sayText(percent === 100 ? "All done! Perfect score!" : "All done! Let's see how you did.", { voiceURI });
  }, [percent, voiceURI]);

  const newBests = saved.session.lists.filter((l) => {
    const prev = saved.previousBests.get(l.id);
    return prev && l.points > prev.points;
  });

  // Hardest first: most misses, then fewest right on the first try.
  const words = [...quiz.words].sort((a, b) => b.attempts - b.correct - (a.attempts - a.correct));

  return (
    <ActivityResults title="🎉 All done!" onAgain={onAgain} onSetup={onSetup} setupLabel="Change lists or difficulty">
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

      <div className="spelling-saved" role="status">
        {saved.stored ? (
          <>
            💾 Saved to <strong>{saved.playerName}</strong>&apos;s progress.{" "}
            {newBests.length ? <>🏆 New best on {newBests.map((l) => l.name).join(", ")}! </> : null}
            <NextLink href="/progress" className="spelling-link">
              See all progress →
            </NextLink>
          </>
        ) : (
          <>⚠️ This browser wouldn&apos;t save your progress (private browsing or storage full).</>
        )}
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

    </ActivityResults>
  );
}
