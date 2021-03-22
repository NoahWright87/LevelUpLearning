# LevelUpLearning
A collection of learning activities and games that track your "experience" in various subjects so you can "level up" your school skills.

# Current status
This project is an initial upload of a spelling test program I made for my kids years ago.  I plan to slowly expand from a simple Windows Forms app that I run on my home computer to a cross-platform, cloud-saved, gamified collection of learning activities that can be used by anyone.

# TODO
Here is the current list of things that need to be done in this project:

## Next Release
- Separate code into frontend display and backend functionality
- Create a test project and have some test coverage
- Add comments throughout
- General clean-up - this code is from years ago and may not use the best practices
  - Use string interpolation whenever possible
  - Refactor to move things into distinct methods with a single purpose
  - Replace any magic numbers/strings with references to a Constants file

## Later Releases
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
