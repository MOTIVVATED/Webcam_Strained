using System;

[Serializable]
public class GameResult
{
	public int score;
	public string dateTime;

	public GameResult(int score)
	{
		this.score = score;
		this.dateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
	}
}
