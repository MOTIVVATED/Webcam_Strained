/*
 * == DEV JOURNAL ==
 *    12.03.2026
 *  Today I want to optimize my FallingObjectTypes. Make this enum byte instead of int
 *  add some new types of objects, like powerups and so on. 
 *  now I think it's better to make two different enums: for tokens and for the rest of the objects.
 *  
 *  I spent half of the day trying to change algorithm of spawning objects.
 *  I got a bug that I'm still trying to fix. And I will.
 *  
 *  Fuck это просто пиздачес лютый, я того всё ебал. I spent whole day making a new logick of Spawn in SpawnManager.
 *  It finally works properly, each new object spawns around the previous one. It turns off "bad luck", and makes it possible
 *  to catch all objects perfectly if you try hard enough. I made it using out word transmitting the x position of the object
 *  to the next one.
 *  I feel so dump but at the same time, I'm proud of myself, cuz I did it by myself. If you gonna be dump, you gotta be tough...

 *    11.03.2026
 * I finished the feature of saving game results and showing them in the UI. I also added a "best score" display. 
 * The results are saved in a JSON file in the persistent data path, and they are loaded when the game starts.
 * It's avaliable in Win or Lose screens, and also in the main menu.
 * The UI shows the most recent 10 results, along with the best score at the top. 
 * I also made sure to wait until the GameResultsManager is initialized before trying to refresh the UI, to avoid any null reference issues. 
 * Overall, this should provide a nice way for players to see their progress and compare their scores over time.
 * 
 *  I added a stats button to the main menu, which opens the results panel.
 * As a template I used the settings panel, but I had to make some adjustments to fit the new functionality.
 * Made some settings for the UI elements, like the text fields for showing the history and best score.
 * 
 * This the first day in my life I'm trying to keep a dev journal, and now I see, I hould think about things to write in while doing them =)
 * All the shit abowe made by AI :)
 * 
 * In general I see only a few things left to do:
 *  -powerups and some other types of falling objects, it's the biggest thing, cuz I need to implement new machanics for them
 *  -player save profiles to keep progression
 *  -different models = different game presets
 *  -laser beams and animation for smashing objects ;3
 *  -refactoring, cleaning and polishing code
 * TOMOROW:
 * idk for now, I think I'm gonna start with first position. Have a sweet dreams dear journal ;*
 */
