# Archive: the original Level Up Learning (2020–2021)

Everything in this folder is the **old Windows desktop app**, kept here only until the
useful parts have been salvaged into the new web app at the repo root. It is not built,
deployed, or maintained. **Once the salvage list below is done, delete this folder** —
git history keeps it either way.

The original project README is preserved as [`ORIGINAL_README.md`](ORIGINAL_README.md).

## What's here

| Path | What it is |
| --- | --- |
| `KidQuiz/LevelUpLearning.sln` | Visual Studio solution (.NET Framework, WinForms) |
| `KidQuiz/LevelUpLearning.Core/` | UI-free logic: data model, save/load, word difficulty, math problem generator, leveling |
| `KidQuiz/LevelUpLearning.WinForm/` | The desktop UI (spelling quiz, math quiz, list editor, users) |
| `KidQuiz/LevelUpLearning.SpeechWindows/` | Windows-only text-to-speech wrapper |
| `KidQuiz/LevelUpLearning.Setup/` | MSI installer project |
| `KidQuiz/LevelUpLearning.Tests/` | A handful of unit tests |
| `KidQuiz/KidQuiz/`, `KidQuiz/TestProjectNetStandard/` | Dead code: the pre-rewrite app and an empty project |
| `Spelling Lists/` | 22 spelling lists (grades 1–6, ~390 words) as XML |
| `.gitignore` | The old Visual Studio ignore file |

## Salvage list

- [x] **Spelling lists** (`Spelling Lists/`). Done: converted by `scripts/convert-spelling-lists.mjs`
  into `src/data/spellingLists.json`, example sentences (`%%` marks the word) kept as written.
- [ ] **Word difficulty heuristic**: `LevelUpLearning.Core/Extensions/StringExtensions.cs`
  (`Difficulty()`): length, repeated letters, runs of vowels/consonants, tricky letters and patterns.
- [ ] **Math problem generator**: `LevelUpLearning.Core/MathUtils.cs` (`GetMathProblemFor`):
  builds a problem aimed at a target level by adding digits, terms, decimals, negatives.
- [ ] **Leveling curve**: `LevelUpLearning.Core/Utils.cs` (`LevelChange`): how a player's
  level moves after a right or wrong answer, relative to the problem's level.
- [x] **Spelling quiz settings**: the difficulty presets (blanks, hint letters, times to finish,
  streak penalty) and score multiplier are in `src/lib/spelling/difficulty.ts`.
- [x] **Saved progress**: users and per-list / per-word performance (`DataRoot`,
  `UserSpellingPerformance`) are now players and session history in the save file (`src/lib/save/`).
- [ ] **Ideas from the old UI** still to bring over: the auto-leveling "adventure" mode
  (`frmSpellingQuizDifficultyAdventure`, `Adventure_*` in `frmSpellingQuiz.cs`).

Rewrite, don't port: the logic is small, and the new app should get it in TypeScript with tests.

## Known side effect of the move

Installed copies of the old desktop app download lists from `Spelling%20Lists` at the
repo root (`GitHubUtility.cs`). Once this move reaches `main`, that import option in the
old app stops finding lists. That's accepted: the desktop app is retired.
