using System;

[Serializable]
public class GameResult
{
    public int score;
    public string dateTime;


    //these are for the extended version, I'll add them later when I have the time to implement them in the game
    //public int itemsCaught; // coins or power-ups caught
    //public int itemsDodged; // here I should make some other values like: members banned, members caught

    public GameResult(int score)
    {
        this.score = score;
        this.dateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
    }

    // Don't delete yet!!! Better change it and add some moer interesting stats to make an extended version with more details, I'll add these values later when I have the time to implement them in the game
    //public GameResult(int score, int itemsCaught, int itemsDodged)
    //{
    //    this.score = score;
    //    this.dateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

    //    this.itemsCaught = itemsCaught;
    //    this.itemsDodged = itemsDodged;
    //}
}
