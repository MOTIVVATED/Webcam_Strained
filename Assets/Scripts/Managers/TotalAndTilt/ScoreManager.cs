using System;
using UnityEngine;
using UnityEngine.UIElements;

public class ScoreManager : MonoBehaviour
{
  public static ScoreManager Instance { get; private set; }

  public int Total { get; private set; }

  //[SerializeField] private int tk1 = 1;

  [SerializeField] private int tk15 = 15;

  [SerializeField] private int tk25 = 25;

  [SerializeField] private int tk111 = 111;

  [SerializeField] private int tk222 = 222;

  [SerializeField] private int tk555 = 555;

  [SerializeField] private int tk666 = 666;

  [SerializeField] private int tk999 = 1111;

	[SerializeField] private GameObject player;

  [SerializeField] FloatingTextSpawner floatingTextSpawner;

  public event Action<int> OnScoreChanged;

  public event Action OnTK_1_15_Collected;
  public event Action OnTK_25_Collected;
  public event Action OnTK_111_Collected;
  public event Action OnTK_222_Collected;
  public event Action OnTK_555_Collected;
  public event Action OnTK_666_Collected;
  public event Action OnTK_1111_Collected;

	private void Awake()
  {
        
    if (Instance != null && Instance != this)
    {
      Destroy(gameObject);
      return;
    }
    Instance = this;
  }
  public void HandleCollected(FallingObjectType type)
  {
    switch (type)
    {
      case FallingObjectType.type_15tk:
        AddScore(tk15);
        OnTK_1_15_Collected?.Invoke();
        floatingTextSpawner.Spawn(tk15, player.transform.position, type);
        break;
      case FallingObjectType.type_25tk:
        AddScore(tk25);
        OnTK_25_Collected?.Invoke();
        floatingTextSpawner.Spawn(tk25, player.transform.position, type);
        break;
      case FallingObjectType.type_111tk:
        AddScore(tk111);
        OnTK_111_Collected?.Invoke();
        floatingTextSpawner.Spawn(tk111, player.transform.position, type);
        break;
      case FallingObjectType.type_222tk:
        AddScore(tk222);
        OnTK_222_Collected?.Invoke();
        floatingTextSpawner.Spawn(tk222, player.transform.position, type);
				break;
      case FallingObjectType.type_555tk:
        AddScore(tk555);
        OnTK_555_Collected?.Invoke();
        floatingTextSpawner.Spawn(tk555, player.transform.position, type);
        break;
      case FallingObjectType.type_666tk:
        AddScore(tk666);
        OnTK_666_Collected?.Invoke();
        floatingTextSpawner.Spawn(tk666, player.transform.position, type);
        break;
      case FallingObjectType.type_1111tk:
        AddScore(tk999);
        OnTK_1111_Collected?.Invoke();
        floatingTextSpawner.Spawn(tk999, player.transform.position, type);
        break;
		}
  }   
  public void AddScore(int value)
  {
    Total += value;
    OnScoreChanged?.Invoke(Total);
  }
  public void ResetScore()
  {
    Total = 0;
    OnScoreChanged?.Invoke(Total);
  } 
}
