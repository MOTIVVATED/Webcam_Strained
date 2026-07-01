using System;
using UnityEngine;

public class PowerUpInventory : MonoBehaviour
{
	public static PowerUpInventory Instance { get; private set; }

	[SerializeField] private SaveForLaterUpgradeConfig config;

	public int Charges { get; private set; }

	public event Action<int> OnChargesChanged;
	public event Action OnPowerUpUsed;

	// -1 means unlimited. Defaults to 0 (tier 0 = power-up not owned yet) until ApplyEquippedTier runs.
	private int maxCharges = 0;

	private void Awake()
	{
		if (Instance != null && Instance != this) { Destroy(gameObject); return; }
		Instance = this;
	}

	private void Start()
	{
		ApplyEquippedTier();
	}

	private void ApplyEquippedTier()
	{
		int tier = 0;
		if (PlayerProfileManager.Instance != null)
			tier = PlayerProfileManager.Instance.GetProfile().saveForLaterUpgrade.equippedTier;

		if (config == null || config.tiers == null || tier < 0 || tier >= config.tiers.Length)
		{
			Debug.LogWarning("PowerUpInventory: Missing or invalid SaveForLaterUpgradeConfig, defaulting to 0 max charges.");
			maxCharges = 0;
			return;
		}

		maxCharges = config.tiers[tier].maxCharges;
	}

	private void OnEnable()
	{
		GameEvents.OnPowerUpCollected += HandleCollected;
	}

	private void OnDisable()
	{
		GameEvents.OnPowerUpCollected -= HandleCollected;
	}

	private void Update()
	{
		if (Charges <= 0) return;

		if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
			UsePowerUp();
	}

	private void HandleCollected()
	{
		if (maxCharges >= 0 && Charges >= maxCharges) return;

		Charges++;
		OnChargesChanged?.Invoke(Charges);
	}

	private void UsePowerUp()
	{
		Charges--;
		OnChargesChanged?.Invoke(Charges);

		// Snapshot first: Collect() destroys objects and removes them from
		// FallingObject.Active during the loop, so iterating the live list directly
		// would mutate it mid-iteration.
		var toCollect = new System.Collections.Generic.List<FallingObject>();
		foreach (var obj in FallingObject.Active)
		{
			if (IsGood(obj.ObjectType))
				toCollect.Add(obj);
		}

		int pointsCollected = 0;
		if (ScoreManager.Instance != null)
		{
			foreach (var obj in toCollect)
				pointsCollected += ScoreManager.Instance.GetPointValue(obj.ObjectType);
		}
		else
		{
			Debug.LogWarning("PowerUpInventory: No ScoreManager instance found, MultiSale total will show 0.");
		}

		foreach (var obj in toCollect)
			obj.Collect();

		if (MultiSalePopup.Instance != null)
			MultiSalePopup.Instance.Show(pointsCollected);
		else
			Debug.LogWarning("PowerUpInventory: No MultiSalePopup instance found in scene.");

		OnPowerUpUsed?.Invoke();
	}

	private bool IsGood(FallingObjectType type)
	{
		switch (type)
		{
			case FallingObjectType.tk1:
			case FallingObjectType.tk15:
			case FallingObjectType.tk25:
			case FallingObjectType.tk111:
			case FallingObjectType.tk222:
			case FallingObjectType.tk555:
			case FallingObjectType.tk666:
			case FallingObjectType.tk1111:
				return true;
			default:
				return false;
		}
	}
}