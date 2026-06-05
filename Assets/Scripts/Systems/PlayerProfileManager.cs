using System.IO;
using UnityEngine;

public class PlayerProfileManager : MonoBehaviour
{
	public static PlayerProfileManager Instance { get; private set; }
	
	private PlayerProfile _profile = new PlayerProfile();
	private string _savePath;

	private void Awake()
	{
		if (Instance != null && Instance != this) { Destroy(gameObject); return; }
		Instance = this;
		transform.SetParent(null);
		DontDestroyOnLoad(gameObject);
		_savePath = Path.Combine(Application.persistentDataPath, "player_profile.json");
		Load();
	}

	public PlayerProfile GetProfile() => _profile;
	
	public void SetName(string playerName)
	{
		_profile.playerName = playerName;
		Save();
	}

	public void RecordGame(int score)
	{
		_profile.gamesPlayed++;
		_profile.totalScore += score;
		_profile.money += CalculateMoney(score);
		Save();
		Debug.Log($"Profile updated: avg={_profile.GetAverageScore()}, rank={_profile.GetRank()}");
	}

	public void SpendMoney(int amount)
	{
		_profile.money = Mathf.Max(0, _profile.money - amount);
		Save();
	}

	private void Save()
	{
		string json = JsonUtility.ToJson(_profile, prettyPrint: true);
		File.WriteAllText(_savePath, json);
	}

	private void Load()
	{
		if (File.Exists(_savePath))
		{
			string json = File.ReadAllText(_savePath);
			_profile = JsonUtility.FromJson<PlayerProfile>(json) ?? new PlayerProfile();
		}
	}

	private int CalculateMoney(int total)
	{
		int moneyEarned = (total * 75 / 10000);
		
		if (moneyEarned < 20) { moneyEarned = 20; }

		return moneyEarned;
	}
}
