# LevelUpLearning
### [Download/install the latest release](https://github.com/NoahWright87/LevelUpLearning/releases/latest/download/LevelUpLearning.Setup.msi) or [check the release notes](https://github.com/NoahWright87/LevelUpLearning/releases/latest).

Welcome to Level Up Learning!  This is a side project by [Noah Wright](https://github.com/NoahWright87) to help his kids (and anybody else's) with various learning activities.  Started years ago and revisited due to the at-home-learning debacle that came along with COVID-19, this is a work-in-progress that will be added onto over time.

Looking for spelling words to import?  Check out the [Spelling Lists](https://github.com/NoahWright87/LevelUpLearning/tree/main/Spelling%20Lists) folder!  Or, use the option Level Up Learning to import them automatically!

**The end goal**: This will be a collection of learning activities and games that track your "experience" in various subjects so you can "level up" your school skills.  Eventually this will expand to be a variety of skills at various grade levels, available to be used by multiple kids.

## Current Features
NOTE: Features preceded by a ❗ were recently added.

### Recent Bug Fixes 🕷
- User's spelling level is now limited to stay between 0 and the max spelling word difficulty.  Previously it was possible to do really well on your first spelling test and end up with a value that's WAY too high.

### General
- Create and edit users.  Currently this is very limited, but if you have multiple kids (like me) it means you can track their performance separately.
- Program shows the current version and alerts you if GitHub has a newer version available.

### Spelling Test
Currently, you can:
- Create spelling words / list
  - Each list is named and can contain any number of words
  - Each word can have an example sentence which will be spoken to the user
  - Words have a rough difficulty calculated based on various characteristics
  - Lists can be made by-hand, editted later, or import/exported
  - ❗ For very long lists of words, they can automatically be split into several smaller lists
- Quiz yourself on those lists
  - ❗ Automatically progress in spelling difficulty, RPG-style!
  - Can select any number of lists to quiz yourself on
  - Can adjust the difficulty (multiple presets, plus custom settings)
    - Difficulty settings result in a score percentage adjustment
  - Each word is read (via Windows text-to-speech), with possible hints shown to the user
- View performance of your tests
  - Automatically see the performance when you finish a test
  - View previous performance of any list and its included words
    - See a rating (out of 5 stars) based on your performance and the difficulty settings used
    - See an overall score based on difficulty of the list and difficulty settings used

### Math Test
- ❗ Still very early, but you can quiz yourself on math problems!
- ❗ Difficulty ramps up as your level increases - the balance on this may be out of whack and will be tweaked in the future.

## Upcoming Features
Some code organization and using better practices to prepare for adding more activities.

Feel free to check out [this repo's project](https://github.com/NoahWright87/LevelUpLearning/projects/2) to see what else is being worked and what's planned down the road.

## Credits
- http://mathparser.org/ is used to parse the results of the math formulas