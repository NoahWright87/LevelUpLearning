# Building another activity

Reuse the parts of a game screen that already have a common convention:

- `ActivityPage` provides the site shell, compact footer and content width.
- `ActivityToolbar` provides the round title and Quit action.
- `ActivityStats` accepts `{ label, value }` items; values can include timers or accessible indicators.
- `ActivityFeedback` reserves room for short, politely announced feedback.
- `ActivityResults` focuses the result heading and provides replay/setup actions. Pass the
  game's own score, statistics and extra content as children.

Use design-system controls, `CardGrid` for setup fields, and `Link as={NextLink}` for navigation.
The shared stylesheet provides `activity-stack`, `activity-actions`, `activity-rules`, and
`activity-result-score` for common composition. Keep board geometry and game-specific
animations in the activity's stylesheet.

For timed activities, `useActivityClock(running, onTick?)` supplies `now` and `sample()`.
Sample again on input so delayed interval callbacks cannot extend deadlines. The callback
can apply the game's pure time transition; omit it when the screen only needs elapsed time.
The hook stops its interval when the round finishes or the component unmounts.

Keep rules and scoring in `src/lib/<activity>/`, tested independently of React. Each game
still owns its setup and state transitions; these components do not impose a game engine.
