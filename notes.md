# Project Notes

##Todo
  - First of all, I'll probably rearrange this into some more formal system of proposing and thinking through things to fix.
  - At some point I should work out how to use the repo pattern as well and provide serialisation for objects.
    - I know that there is an XmlSerialiser class in c#, and so maybe I'll look into that.
    - Before that, just add it as a method, and figure out how to extract that into another class later.
  - I can do the same for the TodoList class.
    - Separate out the serialization and save logic.
  - The view needs a major overhaul.
    - I have just been doing whatever as far as the view is concerned, but maybe sit down and consciously design how its supposed to display.
  - Some of the methods return the readonly list. This should probably be reworked.
  - Error handling.
    - I haven't really figured out in any kind of detailed way how to handle exceptions.
    - We need a way to validate the xml when loading, and to make sure saves and updates happen.
    - Some kind of feedback from the objects that are being updated.
  - The api has to be worked out.
  - Logging
  - Eventually, I need some kind of way to insert different storage options.
  - Remove options parsing into its own class. Perhaps in a commonlib.
  - I think I will stop the TodoList from loading from the file automatically, and have a manually called load function.