using System;

[Serializable]
public class GameResult
{
    // these two are good =)
    public int score;
    public string dateTime;

    public int itemsCaught; // coins or power-ups caught
    public int itemsDodged; // here I should make some other values like: members banned, members caught
    public float timeSurvived; // no need this I'll delete it later
    
    public GameResult(int score, int itemsCaught, int itemsDodged, float timeSurvived)
    {
        this.score = score;
        this.dateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        
        this.itemsCaught = itemsCaught;
        this.itemsDodged = itemsDodged;
        this.timeSurvived = timeSurvived;
    }
}
