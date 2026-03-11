/*
 * == DEV JOURNAL ==
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
 * TOMOROW:
 * idk for now, I think I'm gonna start with first position. Have a sweet dreams dear journal ;*
 */
