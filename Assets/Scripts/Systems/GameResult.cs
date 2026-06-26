using System;

[Serializable]
public class GameResult
{
	public int score;
	public GameOutcome outcome;
	public string dateTime;

	public GameResult(int score, GameOutcome outcome)
	{
		this.score = score;
		this.outcome = outcome;
		this.dateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
	}
}