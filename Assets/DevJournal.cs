/*
* == DEV JOURNAL ==
* 
* 05.07.2026
* 
* Today I'm going to set up all 6 game scenes for each model.
* 
* 
* 
* 04.07.2026
* 
* I have only 6 days to make working build for steam.
* Good that I have not so many things to do.
* Here are the most important things I need to do:
* 
* To Do:
* 1) fix bug with unhighlited rank
* 2) button back in leveling menu
* 3) leveling menu visual style
* 4) main menu animated banner
* 5) steam achievements
* 6) steam leaderboards
* 7) steam cloud saves
* 8) guide
* 9) webcam almanac
* 
* Today I did:
* 1) I fixed bug with unhighlited rank. Now it works properly. The problem was that I tried to call
* Refresh() in Start() or in Open(), but I needed to call it in both.
* 2) I set up leveling menu visual style. Now it looks much better.
* 3) I can't say leveling panel is beautiful, but it works and looks finished.
* 4) I found bugs with: sound, BAN floating text.
* 
* 
* 03.07.2026
* 
* Today I did:
* 1) I lined up buttons in the leveling menu and made them look straight. It was not that easy since
* I use buttons prefabs and containers for the first time.
* 2) I also have a bug with leveling menu. It doesn't update after I buy or equip any upgrade. 
* I need to fix it, but for now I have no idea how to do it.
* 
* 01.07.2026
* 
* Scenes look good now.
* I'm going to work on upgrades and progression visualization now.
* 
* Today I did:
* 1) I made an upgrades progression system.
* We have ranks and currency in game. By rank player can get access to buy upgrades for currency. 
* There are going to be 3 branches with 6  mutually exclusive checkboxes - levels of upgrades, 
* once player unlocks (by unlocks I mean get access by rank and buy it by currency) any of them, 
* then player can choose anyone of unlocked in each branch.
* First branch: saveForLater maximum (0, 5, 10, 15, 20, unlimited). 
* Second branch player movevemnt system: (laneStepMovement, inputSystem movement speed 10, 12, 13, 14) 
* Third branch is number of lanes (active spawnpoints: 2, 3, 4, 5, 6, 7).
* In addition, two independent checkboxes: Ban (PlayerSmashing) and Enlarge (player object 
* scales x2 y2).
* 2) I changed may scripts and made configs.
* 3) I added all UI objects and prefabs that needed, now I need to set it up.
* 4) I made saveForLater colum look acceptible for beggining... Keep working, that was hard and 
* painfull, I'm going to have a little break and then back to work.
* 5) I made the full leveling menu working end to end. Now I need to work on visual style of it.
* 
* 
* 30.06.2026
* 
* I keep working on UI.
* I need to make look nice all scenes and panels.
* 
* Today I did:
* 1) I made, implemented in Unity and arranged lables for viewers and total, changed text view.
* 2) I made the same for tilt and live, also I made model much bigger, now it takes 1/3 of the screen,
* on the right. Now game scene looks exactly as I wanted.
* 3) I set up stats screen and settings screen in mane nenu.
* 4) I finished working on UI.
* 5) I started setting up game scenes.
* 
* 29.06.2026
* Today I did:
* 1) strained - is the name of my game.
* 2) I added one more music clip to my soundtrack! Now there are 10 thacks.
* 3) I made floating text spawn on FallingObject's position not on Player's position like it was before.
* 4) I added new FallingObjectType saveForLater. Just great mechanic. Catching this object you you save
* it for later, stack it, then you can use one by one, catching all tk* on scene automaticly.
* 5) I added MultiSalePopup. Big bright letters with number of points collected popup when multi sale 
* hits. Looks great!
* 6) I added multiSale counter: lable and text with current multiSale counter. Works properly.
* 
* 28.06.2026
*
* Today I did:
* 1) I added PixelImpact font in Unity. Replaced some fonts in game. Workinged on a visual style and color
* 2) I made sprites: Frame, Button, Lock - for ModelSelectionPanel and arranged them in Unity.
* 
* 
* 27.06.2026
* I have 4 days left to release my game in any state.
* Today we are going to touch animations.
* The goal is to make game look as cool as it sounds ;)
* 
* Today I did:
*
* 1) I made and added animated flames for each falling object prefab. It took about 3 hours ;/
* 2) I made player's trash can burn as well, added flame there.
* 3) I added animated member image on popup panel. Now it's possible to add as many as I can make in Aseprite!
* 4) I added 5 more animated members.
* 
* 26.06.2026
* 
* I feel a little frustrated today. I think it's because yesterday I tried to start making
* pvt feature, spent some time on it, and realized that I actualli don't know, what it's going to
* be like conceptually. So I had to roll back to the stable version and lost some settings in Unity.
* 
* I'm going to work on progression today cuz it doesn't need to invent something new from me about
* gameplay. Only work with all I already have.
*
* I still need to make:
* 1) developer's shortcuts
* 2) make unlocks visible
* 3) upgrades
* 4) animation
* 5) Guide
* 6) UI sprites
* 
*       !!!Important!!!
* I think it's a good idea to make all progression data visible on a model selection panel.
*
* Today I did:
* 1) I made a meta data progression: scene unlocking, currency, win/lose stat tracking,
* and the result history are all wired up and confirmed working end to end. 
*
* 
* 
* 25.06.2026
* 
* Progression is the biggest thing that left to do. So I'm going to start doing it today.
* But first I want to improve  Undressing feature and make it work exactly as I want to.
* I want to add a pvt object.
* 
* Today I did:
* 1) I added variables and some logic in ApearanceController. Now it's based not only on Tilt Level,
* but on ScoreManager events as well. Model starts undressing when she gets enough Valuable objects.
* So that's why from now it's name is just AppearanceController, not TiltAppearanceController, as
* it was before.
* 2) I added case for MafuLegend scene in TimeScaleController adn SerializeFields for variables of
* timeScale increments for each Scene. Fixed a bug with undressed model. It happened if a game 
* started after finished one. Now static ValuableObjectsCollected = 0 on Start();
* 3) I made game duration based on audio clip length.
* 4) I moved the buffer logic into MusicPlaylist itself, keyed per-clip by name via 
* GetDurationBufferFromClip, and changed the event signature to Action<float, float> 
* (clip length + buffer) so GameManager doesn't need its own durationBufferFromClip field anymore.
* It lets me tune the "feels right" buffer per track instead of one global value.
* 
* 24.06.2026
*
* Today I did:
* 1) I added a new sprite for player game object. For so long I thouht about the way it should look
* like and I still don't have an idea I'd like, so I made it a trash can, eventually I like the
* way it looks like.
* 2) I added a mechanic of undressing in TiltAppearanceController. It works now but I'd like to
* make it more comprehensive and advanced depending not only on tilt, but on viewers (timeScale)
* or on TKs caught, for exampel if we catch anything abow 100 model stays in Underwear, next we get
* anything abowe 100 model stays naked and then it continues the way it is now.
* 
* 
* I need do animate Undressing.
* I need to make a progression.
* 
* 
* 23.06.2026
* 
* I broke up with my girlfriend two days ago.
* I'm trying to get back to work.
*
* Today I did:
* 1) I needed a mechanic of decreasing tilt without bans. I made it, I added optional decreasing
* tilt by tyme and by catching good objects. It can be turned on and off in TiltManager. 
* So it can vary from scene to scene.
* 2) Added and animated Underwear and Dressed sprites for MafuLegenda.
* 3) My last SpawnPoint in array was never used, so I changed the formula to fix it.
* 
*
* 19.06.2026
* 
* Today I did:
* 1) 6 tracks fom my game soundtrack and they are so fun! I added them in game scenes.
* 2) Yesterday I couldn't understand, why I can't animate model sprite in game scene when I attached
* it on a Player. Today I woke up with idea that it's because on a model selection screen they are
* images on a canvas, but on a game scene it's a sprite in a game world, so I need to animate 
* sprite in Animator. Yes, I did it.
* 3) I experemented a bit making a new game mode for MafuLegenda, it's different experience, so
* I think I'll be able to mace special game mode for each model.
*
*
* 18.06.2026
* 
* Today I did:
* 1) Reworked all site labels:
*   - painted 
*   - implemented in Scene 
*   - made them UI Canvas in World Space, so they attached to spawnpoints but they are UI
*   at the same time
*   - animated them
* 2)I made some UI music with for my game. It's so fun! I'm so good in that! Here my music background
* explodes! Now I can see clearly the way I'm going to make music for my game.
* 3) I worked on a game visual style in Unity.
* 4) I started making a new Scene for Mafu. Need to add and animate sprite for her, then deal with 
* sites/spawnpoints.
* 
* 17.06.2026
*     Tomorrow:
* I want to try to make a new scene for Mafu. I want to try less sites, objects falling slower,
* can't ban webcams only avoid them. It's going to be fun!
* 
* I want to make new movement system now! I want to try if it feel more impactful moving player
* not by holding arrows but tapping it. By each tap of right or left arrow, player moves one 
* row right or left. I'm going to try to make it after lunch.
* 
* Today I did:
* 1) exported 4 models with Json files from Aseprite and prepared them for implementing in Unity
* 2) made a button for each of them on a ModelSelection scene
* 3) imported them all in unity and animated them
* 4) I made NewMovement.cs
* 
* 
* 16.06.2026
*       Today I just cleanded some scripts of garbage.
*       
*   Tomorrow I need to:
*   
* 1) export 4 models with Json files from Aseprite and prepare them for implementing in Unity
* 2) make a button for each of them on a ModelSelection scene
* 3) import them all in unity
* 
* 
* 15.06.2026
* I fixed a bug with wrong Logos punching, when an object cought. That was caused by wrong X position.
* All the logic was based on player.x position, and player could catch the object by the edge, being on
* the line next to the object and all a Punch of the Logo next to the logo that was needed.
* I made LogosFeedback.cs refer to the collected object position, but not to the player position like
* it was before. Now it Always work properly. Before I did itis, It made wrong Logos punch sometimes.
* 
* Also I finished one of models that I started yesterday, then I made a new model and animated it.
* So we have 6 now. I think that's enough for the beggining. I'm going to implement them in the game
* tomorrow.
* 
* 12.06.2026
* I added 7 sites on each spawnpoint at the top of the screen. I made a LogosFeedback.cs to animate them.
* Looks much better. Now my game is much closer to the state I'd like to show to somebody.
* I need to add more models and make a progression system.
* 
* 
*   11.07.2026
* timeScale gets stuck on 0.7
* 
* Finally I reworked TimeScaleController and it works well. I made it much simplier and now it works properly.
* It's extensible now and It's going to be much easier to add new features based on timeScale changes.
* I'm going to continue modify it.
* 
*   10.06.2026
* Today I'm going to work on my TimeScaleController system.
*   Here is my analysis:
*   -timeScale 0 on pause and after I release pause it stays 0
*   -viewers number grows independently
* I reworked the formula and naming, now it works properly. Still need to fix viewers number rendering.    
*
* 
*   09.06.2026
* Today I'm going to work on polishing and fixing bugs.
* 
*     Today I did:
* 1) I fixed the bug with surviving scene transitions Pause Menu Panel. That was elementary, but still, I faced 
* such a problem for the first time. I just removed DontDestroyOnLoad. Now I know exactli the way it works -_-
* 2) I had this warning in Console: The referenced script (Unknown) on this Behaviour is missing!
* I made a FindMissingScript.cs to find it and I found it and removed.
* 3) I have a bug tha game continues even when result panel appears.
* upd I fixed that =)
* 
* I realized that my TimeScaleController logic is totally wrong. I need to rework it complitely.

* 
*   07.06.2026
* I have some bugs in my game, so I need to fix as much as I can.
*   1) MissingReferenceException. I'm trying to access destoyed canvas.
*   2) Make EndGameScreen SetActive false
* 
*   06.06.2026
*   First I'm gonna finish with things I couldn't finish yesterday.
*   - new model in Aseprite
*   - new data on gameresult screen
*   
*   Then I'm going to refactor all my 40 scripts.
*   
*     Today I did:
* I made full refactoring. I removed some scripts and added some new scripts. Fixed some bugs.
* I still need to fix some bugs, the most difficult is bug with timescale. It's hard to understand where 
* the problem is, but I'll do my best, cuz I belive in my new system. I'm convinced this system prevents
* more potential bugs in future.
* I made one new model sprite, need to animate it tomorrow.
* Tomorrow I want to make all I have now work smoothly, the way it has to work. Nothing new, just polishing.
* 
*   05.06.2026
*   Two month ago I was about to change something in scripts resposible for music. Now I'm trying to reconstruct
* the chain of events. I got some bug trying to stop music on pause. I realized that I miss the understanding of
* IEnumerator and Coroutines. That's why I stopped working and got to learn about them.
* I'm going to find the problem and fix it now.
* 
* It isn't a surprise that development of my game takes much more time that I expected.
* I can't make any plans or predictions now becaouse of lack of experience.
* 
* So the most importat thing I must do as soon as possible is to decide:
* 
*   should I finish my game by any coast by some deadline?
*           or
*   should I leave it and focuse on something else?
*   
*   I got the answer to this question. I will finish it by any coast in 10 days.
*   In 10 days I switch to another project.
*   
*   I'm going to make a new model in aseprite now.
* 
*     Today I did:
* I rebuilt PauseManager:
*   - added DontDestroyOnLoad so the singleton survives scene transitions
*   - removed SetPaused(false) from Awake and reset state directly, so Time.timeScale is not touched unless needed
*   - added OnDestroy to clean up timeScale and null the instance
*   - extracted CanPause() to a single guard used only where needed, removing the duplication
*   between Update and TogglePause
*   - renamed TogglePause to TryTogglePause to make it clear it is condidional, not guaranteed
*   - Restart now calls SetPaused(false) first to guarantee clean state, and falls back to scene reload
*   if GameManager is absent
* I rebuilt MusicPlaylist:
*   - playLoop now runs while (true) and yields inside a pause-wait loop instead of exiting when paused,
*   so the coroutine stays alive through pause/resume cycles. That's where I stuck befor I learned
*   IEnumerators and Coroutines
*   - the playback wait loop also accounts for paused state, so a clip that is paused mid-way is not skipped.
*   - update is simplified to a single paused bool, removing the duplicate null check.
*   - OnDisable now calls source.Stop() so the clip does not linger if the object is disabled.
*   - fixed the GetRandomIndex condition from >= 1 to <= 1.
*   - removed all debug logs from normal execution paths.
* I decided to finish my game in 10 days at any coast.
* I started making a new model in Aseprite. It's almost ready and animated.
* 
*   04.06.2026
*   First for today:
* Before I continue building my game, I need to finish my final exercise for IEnumerators and Coroutines.
* 
*     Today I did:
* I did it and I'm proud of myself cuz I built genuinely good code. I went beyound the excercise by introducing
* a Wave class to encapsulate the data, which is exactly the right instinct for a real project.
* Clean, readable, extensible. 
* 
*   03.06.2026
* It took almost twoo month to get back to work. I feel a bit sorry about that cuz it seems too long...
* At the same time, I had so many things to do with my life. I moved from Belgrade to Almaty, Kazakhstan and started
* a new chapter of my life. Also I had a wacation of my dream in Thailand. I settled in a new place,
* organized a work place and now I'm going to work harder then ever. I feel a bit frustrated, cuz it's so many things
* to do, and I feel so strong at the same time, cuz I feel that I'm living the life  that I dreamed to live.
* Thanks god! I'm so happy. I'm almost crying now and I feel thankful to every thing in this world. I'd like to say
* thank to everyone who supported me, but I don't really have such people hahah... I have many good people in my life,
* who helped me somehow, I'm convinced no one of them knows a thing about my struggle.
* I care a lot if I'm going to succeed or not... if I'm doing things right or not... And so often I think that 
* I'm going to die anyway, so nothing matters. I'm just going to keep trying and enjoy each moment of my life.
* I moved to anomther country, now I'm in relationships with a woman, that I dreamed about for so long, and sometimes,
* I feel I took on too much. It scares me a bit. But I know I'm not a coward, and I act even if I'm scared. I do a lot of 
* mistakes may be... but I learn from them... I loose time and money (like a hollow loses time and souls in Dark Souls),
* but I'm going to reach my goal soon or later. I hope sooner than I die =).
* 
*   07.04.2026
* I want to highlite the row that triggered by player.
* I want to add a new button. For now we have only left, right and down arrows.
* I want to add up arrow to "accept" "Requests" on a highlited row.
* Besides I want to start playing without timescaling with time.
* I need to find the way to increase and decrease the game speed but leave the game duration constantly the same. (done)
* 
* I need to remake MusicPlaylist make separate tracks for paused and unpaused states.
* 
*     Today I did:
* I solved the problem with timeScale and game duration.
* Now I can change the timeScale without changing the game duration.
* I made music pause when the game is paused.

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
*     -scripts for buttons
*     -make it look normal in Unity
*     -make a copy of Game scene for each model, and change some settings in inspector for them
*     
*  I added Underwear and Dressed objects for models and animations for them. It looks good.
*  Now I'm able to "undress" them easily if it's needed.
*
*     Today I did:
* I made ModelSelectionPanelUI script that contains methods for buttons on the model selection screen.
* I made two copies of a Game scene for each model, and changed some settings in inspector for them.
* For now, the soundtrack is the only thing that differs between models.
* I worked on the visual style of the UI in menu and game scenes.

*   02.04.2026
* I can see now the way I'm going to make different models.
*     Now I got animated sprites for two of them.
*     After a start bottom we are going to get to a model selection screen.
*     Each model is a button wich switches the scene of the game.
*     The scene based on my existing scene Game, with some changes in inspector has 
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
