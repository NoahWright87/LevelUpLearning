# LevelUpLearning
A collection of learning activities and games that track your "experience" in various subjects so you can "level up" your school skills.

# Current status
This project is an initial upload of a spelling test program I made for my kids years ago.  I plan to slowly expand from a simple Windows Forms app that I run on my home computer to a cross-platform, cloud-saved, gamified collection of learning activities that can be used by anyone.

# TODO
Here is the current list of things that need to be done in this project:

## Recent Changes
- Separated code into frontend display and backend functionality
- Created a test project
- Some general clean-up done.  Still not up to best practices, but it's getting there
  - Used string interpolation where possible
  - Moved some hardcoded values to a Constants class
  - Began to separate things into single responsibility, removed repetition
- Added comments on most of the backend methods/variables

## Next Release
- Add some basic unit tests
- Improve on what's already there
  - Provide interface where words / sentences can be created and saved
    - Allow lists of words to be named, can be tested on one or more lists simultaneously
    - Remember which lists were checked from previous session
    - Make interface friendly for non-technical people, but insert the spelling word as a symbol placeholder
    - Make sure words/sentences can be added quickly - parents dont' have much time
  - Have a "login" where people can choose a name from a dropdown
  - Store stats (per name chosen above) between sessions
    - All this will be in XML for now, but keep things generic so it can later be put in a legit database

## Later Releases
- Noises /animations when things are right/wrong
- Some logic to say things were very close
- Utilize Amazon Polly for text-to-speech: it may be slower than using native Windows voices, but will allow the code to be portable
- Recreate UI in UWP
- Go beyond spelling tests
  - Math activities
  - Grammar, parts of speech, and other non-spelling English activities
  - Like the original idea - see what my kids need to study and incorporate it
- Have accounts, so multiple people can use the same app, and store performance into a database
  - Initially, local accounts stored in XML/JSON file
  - Eventually, use Amazon Cognos (or similar) to have logins
  - Eventually, store data in a cloud DB so progress is saved across devices
- Create non-Windows UIs
  - Use Xamarin to make an Android app
  - Create a web-based interface
