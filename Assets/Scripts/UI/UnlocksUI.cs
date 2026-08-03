using UnityEngine;

public class UnlocksUI : MonoBehaviour
{
  [SerializeField] private GameObject MafuLock;
  [SerializeField] private GameObject ApexLock;
  [SerializeField] private GameObject PampyLock;
  [SerializeField] private GameObject EnterLock;
  [SerializeField] private GameObject SmellyLock;
  [SerializeField] private GameObject MadyLock;

  public static UnlocksUI Instance { get; private set; }

	void Start()
  {
    if (Instance != null && Instance != this) { Destroy(gameObject); return; } Instance = this;

    RefreshLocks();
	}

  private void OnEnable()
  {
    GameEvents.OnUpgradesChanged += HandleUpgradesChanged;
    RefreshLocks();
  }

  private void OnDisable()
  {
    GameEvents.OnUpgradesChanged -= HandleUpgradesChanged;
  }

  private void HandleUpgradesChanged()
  {
    RefreshLocks();
  }

  private void RefreshLocks()
  {
    if (PlayerProfileManager.Instance == null)
    {
      Debug.LogWarning("UnlocksUI: PlayerProfileManager instance not found.");
      return;
		}
    else
    {
      var profile = PlayerProfileManager.Instance.GetProfile();
      if (profile == null)
      {
        Debug.LogWarning("UnlocksUI: Player profile not found.");
        return;
      }
      MafuLock.SetActive(!profile.IsSceneUnlocked("MafuLegenda"));
      ApexLock.SetActive(!profile.IsSceneUnlocked("ApexFunk"));
      PampyLock.SetActive(!profile.IsSceneUnlocked("PampyBam"));
      EnterLock.SetActive(!profile.IsSceneUnlocked("EnterYou"));
      SmellyLock.SetActive(!profile.IsSceneUnlocked("SmellySam"));
      MadyLock.SetActive(!profile.IsSceneUnlocked("MadiMeows"));
		}
	}
}
