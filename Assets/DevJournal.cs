/*
* == DEV JOURNAL ==
*   07.04.2026
* I want to highlite the row that triggered by player.
* I want to add a new button. For now we have only left, right and down arrows.
* I want to add up arrow to "accept" "Requests" on a highlited row.
* Besides I want to start playing without timescaling with time.
* I need to find the way to increase and decrease the game speed but leave the game duration instantly the same.
*   
*   06.04.2026
* I'm not sure, if I'm going too deep, but now I feel like I should implement the feature that I thought about before.
* Now we got one spawn point.
* I want to try to make rows. In the beggining of each row on the top of the screen I want to see a name of member
* and his message. Then I want to make a list of spawnPoints instead of one. I won't use a range of minX-maxX,
* I want to try to make each spawnPoint stable.
* 
*     Today I did:
* I made 7 spawnpoints with attached almost transparent rows, which makes trajectory of falling objects visible for players
* Now I can attach "members" there.
* 
*   05.04.2025
* First thing I'm going to do today is to dicide, what I'm gonna do today. Let's get busy.
*...
* Aaand here we go!
* From very beginning I had an idea of pvt mode, but I didn't know how to implement it.
* Now I see the way to do it.
* 
* Here are the conditions:
* 
*   The golden rule is: Never accept bad PVT. Accept good.
*   to enter the pvt mode player need to reaach maximum timeScale (for the player it's a number of viewers)
*   then objects of pvt start to spawn randomly, and player needs to smash bad ones untill he catches good one.
*     
*     grey ones we don't need, cuz we will just slow down, lose viewers and get out of pvt mode soon
*     
*     purple ones are good
*       here is the juice!!!
*       -player sees BIG letters like: "hi bb", or "lets funny", and he needs to input them in two seconds
*         letters fill up if the player inputs them correctly
*         the progress bar shows how much time is left to input the word, so it has to be fast
*         
*       -all points are going to be collected automatically, so we don't need to catch tehm
*       -we only need to catch "Request" objects (like members requests: doggy, flash or smth)
*       -instead of Viewers number, we will see "PVT".
*       -pvt continues as long as we catch "Request" objects.
*         (but it's not gonna be too long, cuz we don't have timeScale maximum in pvt)
*   That's it about pvt.
*   
*   While I was writing this, I invented another new thing!
*   Request objects. We will no longer use increasing timeScale with time. We will increase this by 
*   catching Requests
*   So we need a pool of requests and animations for them.
*   They go slowly so we can see the note and catch them only if this speed up is acceptable for us.
*   Cuz if we catch some request that speed us up too much, we get tilted fast and loose the game.
* 
* Now I'm going to start working on a new "Request" object.
* 
*   03.04.2026
* I made a canvas for model selection yesterday.
* Today I'm going to make it work properly.
*     -scripts fo buttons
*     -make it look normal in Unity
*     -make a copy of Game scene for each model, and change some settings in inspector for them
*     
*  I added Underwear and Dressed objects for models and animations for them. It looks good.
*  Now I'm able to "undress" them easily if it's needed.
*
*     Today I did:
* I made ModelSelectionPanelUI script that contains methods for buttons on the model selection screen.
* I made two copies of Game scene for each model, and changed some settings in inspector for them.
* For now, the soundtrack is the only thing that differs between models.
* I worked on the visual style of the UI in menu and game scenes.

*   02.04.2026
* I can see now the way I'm going to make different models.
*     Now I got animated sprites for two of them.
*     After a start bottom we are going to get to a model selection screen.
*     Each model is a button wich switches the scene of the game.
*     The scene based on my existing scene Game, with some changes in inspectorhas 
*     The scene has the same name as a model.
*   
* So what exactly I need to do:
*     -another canvas with model selection screen in the main menu scene
*     -two bottoms with sprites of models (for now only two)
*       models has to be animated, and It's a big deal cuz I need to find the way
*       to add .aseprite animations in unity and make them work properly
*       
*     Today I did:
* I reordered objects in the hierarchy in Menu and Game scenes.
* I added a new canvas for the model selection screen.
* I walk through whole the path of export animated sprites from Aseprite and import them to Unity.
* I added two buttons with animated sprites of models on a new canvas.
* I faced some problems with animation and color profiles. It took a few hours but I fixed them clearly.
* The battle was cruel, but I slayed them all. As always.

*		01.04.2026
*	Now I want to make a fake chat to make the game look more realistic, atmospheric and natural.
*	
*	Today I started to work on a game visual style.
*	I'm convinced that graphic doesn't matter, but visual style means a lot.
*	I made a colour palette of CB in Aseprite and in Unity.
*	I changed some elements of UI in game and it looks better now.
*	But still there is a lot of work to do.
*	
*	Now I see more clearly what I need to do untill my game is ready to be released:
*		-players account to keep progression and stats
*		-six models with different presets and difficulties
*			(model name and the sign of a model on a 'player game object')
*		-fake chat with messages about the game
*			(model name in the top of the chat)
*		-new object of PVT mode
*		-pvt mode itself
*		-min and max spawn X values depending on viewersnumber
*			it's important to figure out how to make the range of spawn visible for a player
*		-Sounds
*		  that's a big thing to do
*		  1) I need soundtracks, I think I'll do something with AI
*		  2) Sounds of model
*		  3) Sounds of members
*			
*	Will see tomorrow, what with we're going to start...
  
*		30.03.2026
*	New feature with distracting popup windws works and it plays fun.
*	Now I need:
*		-make it work with floating text of BAN
*		-implemen it in tilt manager
*		-for now I will disable BAD objects, I don't like the way they look like
* 
*	I did things above easily.
*	Moreover, I reordered folders and delited some useless scripts.
*	That's why I got a NullReferenceException xD with sound and changing sound volume in sttings.
*	It took just 15-20 minutes to find out where the problem is and fix it.
*	
*	Besides, I made a new sprite for slow object and set it up.
*	I made it 32x32 but other objects are 64x64 =(
*	
*	I changed minX and maxX values in SpawnManager from -4 and 4 to -3 and 3.
*	It makes possible to increase the speed of a game and I think it's more fun and drive.
*	I deleted all old and useless prefabs.
 	
*		29.03.2026
*	I'm here to make it work properly.
* 
*		28.03.2026
*	I've tried to make that shit work, but I didn't make it.
*	I feel so tired, but now I see the way I'm gonna make it tomorrow and that's a lot!  =)
*	
*		26.03.2026
*	I finally made a new object Webcam and a new mechanic of a popup distractive object for it.
*	It works but not stable for now, so I have to polish it tomorrow.
*	I made some some UI sprites: frame and X button.
*	So All I gonna do tomorrow is polishing this feature.
*		
*		25.03.2026
*	I made a new type of object that reduces time scale when collected.
*	I made a script for it complitely on my own =)
*	I fixed some bugs with missing new objects, that I added yesterday.
*	Now it works properly.
*	I added one new soundtrack for a game scene.
*	I made an animated model sprite in Aseprite.
*	Some day I need do implement it in the game.
* 
*		24.03.2026
*	It scares me, when I see how often I get back to work =( I was sick...
*	I've already made some new sprites this morning for new objects: 222, 555, 666, 1111.
*	I hope it's not gonna be too hard to implement them. This is what I'm gonna do today.
*	Then I'll continue with powerups. I'm still not finished with the distraction mechanic.
*	
*	I made new sprites for the new objects, and prefabs for them: 222 , 555, 666, 1111.
*	I made changes in code so it works properly now.
*	I also find a better balance of fallspeed and spawn rate.
*	I changed the distribution of sounds for sound feedback, and added a new one for the biggest object.
*	I got an idea to of a new powerup, which will slow down the time.
*	It's going to roll back the timescale to give a player a chance to handle till the end.
*	
* 
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
*  
*  TOMORROW:
*  I need to make it work for valuable objects(15-111).

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
