using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[Serializable]
public class GameResultsList
{ 
    public List<GameResult> results = new List<GameResult>();
}

public class GameResultsManager : MonoBehaviour
{
    public static GameResultsManager Instance { get; private set; }

    private GameResultsList _data = new GameResultsList();
    private string _savePath;

    private void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;

        transform.SetParent(null); // Detach from any parent to ensure it persists across scenes
        DontDestroyOnLoad(gameObject);

        _savePath = Path.Combine(Application.persistentDataPath, "game_results.json");
        Load();
    }

    public void SaveResult(int score)
    {
        var result = new GameResult(score);
        _data.results.Add(result);
        Save();
        Debug.Log($"Result saved: Score ={score}, Date={result.dateTime}");
    }

    public List<GameResult> GetAllResults() => _data.results;

    public GameResult GetBestScore()
    {
        if (_data.results.Count == 0) return null;
        GameResult best = _data.results[0];
        foreach (var r in _data.results)
            if (r.score > best.score) best = r;
        return best;
    }

    public void ClearAll()
    {
        _data.results.Clear();
        Save();
    }

    private void Save()
    {
        string json = JsonUtility.ToJson(_data, prettyPrint: true);
        File.WriteAllText(_savePath, json);
    }

    private void Load()
    {
        if (File.Exists(_savePath))
        {
            string json = File.ReadAllText(_savePath);
            _data = JsonUtility.FromJson<GameResultsList>(json) ?? new GameResultsList();
        }
    }
}
