using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelingPanelUI : MonoBehaviour
{
	[Header("Panel")]
	[SerializeField] private GameObject panelRoot;
	[SerializeField] private TMP_Text moneyText;
	[SerializeField] private TMP_Text rankText;
	[SerializeField] private GameObject infoButton;

	[Header("Branch Columns")]
	[SerializeField] private Transform saveForLaterColumnContent;
	[SerializeField] private Transform movementColumnContent;
	[SerializeField] private Transform laneColumnContent;
	[SerializeField] private UpgradeTierButtonUI tierButtonPrefab;

	[Header("Rank Progression Column")]
	[SerializeField] private Transform rankColumnContent;
	[SerializeField] private RankProgressionRowUI rankRowPrefab;

	[Header("Independent Upgrades")]
	[SerializeField] private UpgradeToggleButtonUI banButton;
	[SerializeField] private UpgradeToggleButtonUI enlargeButton;

	[Header("Configs")]
	[SerializeField] private SaveForLaterUpgradeConfig saveForLaterConfig;
	[SerializeField] private MovementUpgradeConfig movementConfig;
	[SerializeField] private LaneUpgradeConfig laneConfig;
	[SerializeField] private BanEnlargeUpgradeConfig banEnlargeConfig;

	private const int MaxRank = 6;

	private readonly List<UpgradeTierButtonUI> spawnedSaveForLaterButtons = new List<UpgradeTierButtonUI>();
	private readonly List<UpgradeTierButtonUI> spawnedMovementButtons = new List<UpgradeTierButtonUI>();
	private readonly List<UpgradeTierButtonUI> spawnedLaneButtons = new List<UpgradeTierButtonUI>();
	private readonly List<RankProgressionRowUI> rankRows = new List<RankProgressionRowUI>();

	private void Start()
	{
		BuildRankColumn();

		Refresh();

	}

	private void OnEnable()
	{
		GameEvents.OnUpgradesChanged += Refresh;
	}

	private void OnDisable()
	{
		GameEvents.OnUpgradesChanged -= Refresh;
	}

	public void Open()
	{
		panelRoot.SetActive(true);

		if (rankRows.Count == 0)
			BuildRankColumn();

		Refresh();
	}

	public void Close()
	{
		panelRoot.SetActive(false);
	}

	public void OnInfoButtonClicked()
	{
		infoButton.SetActive(false);

		if (GuideManager.Instance == null)
		{
			OnGuideFinished();
			return;
		}

		GuideManager.Instance.ShowLevelingGuide(OnGuideFinished);
	}

	private void OnGuideFinished()
	{
		infoButton.SetActive(true);
	}

	private void Refresh()
	{
		if (PlayerProfileManager.Instance == null)
		{
			Debug.LogWarning("LevelingPanelUI: No PlayerProfileManager instance found.");
			return;
		}

		var profile = PlayerProfileManager.Instance.GetProfile();

		if (moneyText != null) moneyText.text = $"Cash: {profile.money}$";
		if (rankText != null) rankText.text = $"Rank {profile.GetRankNumber()}";

		RefreshSaveForLaterColumn(profile);
		RefreshMovementColumn(profile);
		RefreshLaneColumn(profile);
		RefreshBanButton(profile);
		RefreshEnlargeButton(profile);
		RefreshRankHighlight(profile);
	}

	// --- Save For Later column ---

	private void RefreshSaveForLaterColumn(PlayerProfile profile)
	{
		ClearButtons(spawnedSaveForLaterButtons);
		if (saveForLaterConfig == null || saveForLaterConfig.tiers == null) return;

		var progress = profile.saveForLaterUpgrade;
		int visibleCount = GetVisibleTierCount(progress, saveForLaterConfig.tiers.Length);

		for (int i = 0; i < visibleCount; i++)
		{
			var tier = saveForLaterConfig.tiers[i];
			var state = GetTierState(profile, progress, i);
			int tierIndex = i;

			var button = Instantiate(tierButtonPrefab, saveForLaterColumnContent);
			button.Setup(tier.label, tier.cost, tierIndex + 1, state,
				() => HandleTierClick(UpgradeBranch.SaveForLater, tierIndex, tier.cost));
			spawnedSaveForLaterButtons.Add(button);
		}
	}

	// --- Movement column ---

	private void RefreshMovementColumn(PlayerProfile profile)
	{
		ClearButtons(spawnedMovementButtons);
		if (movementConfig == null || movementConfig.tiers == null) return;

		var progress = profile.movementUpgrade;
		int visibleCount = GetVisibleTierCount(progress, movementConfig.tiers.Length);

		for (int i = 0; i < visibleCount; i++)
		{
			var tier = movementConfig.tiers[i];
			var state = GetTierState(profile, progress, i);
			int tierIndex = i;

			var button = Instantiate(tierButtonPrefab, movementColumnContent);
			button.Setup(tier.label, tier.cost, tierIndex + 1, state,
				() => HandleTierClick(UpgradeBranch.Movement, tierIndex, tier.cost));
			spawnedMovementButtons.Add(button);
		}
	}

	// --- Lane column ---

	private void RefreshLaneColumn(PlayerProfile profile)
	{
		ClearButtons(spawnedLaneButtons);
		if (laneConfig == null || laneConfig.tiers == null) return;

		var progress = profile.laneUpgrade;
		int visibleCount = GetVisibleTierCount(progress, laneConfig.tiers.Length);

		for (int i = 0; i < visibleCount; i++)
		{
			var tier = laneConfig.tiers[i];
			var state = GetTierState(profile, progress, i);
			int tierIndex = i;

			var button = Instantiate(tierButtonPrefab, laneColumnContent);
			button.Setup(tier.label, tier.cost, tierIndex + 1, state,
				() => HandleTierClick(UpgradeBranch.Lanes, tierIndex, tier.cost));
			spawnedLaneButtons.Add(button);
		}
	}

	// --- Shared tier helpers ---

	// All tiers are always visible, regardless of ownership or rank.
	private int GetVisibleTierCount(UpgradeBranchProgress progress, int totalTiers)
	{
		return totalTiers;
	}

	private UpgradeTierButtonUI.State GetTierState(PlayerProfile profile, UpgradeBranchProgress progress, int tierIndex)
	{
		if (progress.ownedTiers[tierIndex])
			return progress.equippedTier == tierIndex ? UpgradeTierButtonUI.State.Equipped : UpgradeTierButtonUI.State.Owned;

		int requiredRank = tierIndex + 1;
		return profile.GetRankNumber() >= requiredRank ? UpgradeTierButtonUI.State.Buyable : UpgradeTierButtonUI.State.Locked;
	}

	private void HandleTierClick(UpgradeBranch branch, int tierIndex, int cost)
	{
		var manager = PlayerProfileManager.Instance;
		if (manager == null) return;

		var progress = manager.GetProfile().GetBranchProgress(branch);
		if (progress == null) return;

		if (progress.ownedTiers[tierIndex])
		{
			manager.EquipTier(branch, tierIndex);
			return;
		}

		if (!manager.CanPurchaseTier(branch, tierIndex, cost))
		{
			Debug.Log($"LevelingPanelUI: Cannot purchase tier {tierIndex} on {branch} (rank or currency insufficient).");
			return;
		}

		manager.PurchaseTier(branch, tierIndex, cost);
		manager.EquipTier(branch, tierIndex); // Equip immediately after buying.
	}

	private void ClearButtons(List<UpgradeTierButtonUI> buttons)
	{
		foreach (var button in buttons)
		{
			if (button != null) Destroy(button.gameObject);
		}
		buttons.Clear();
	}

	// --- Ban / Enlarge ---

	private void RefreshBanButton(PlayerProfile profile)
	{
		if (banButton == null || banEnlargeConfig == null) return;

		var state = GetToggleState(profile.banOwned, profile.banEquipped, profile.GetRankNumber(), banEnlargeConfig.banRank);
		banButton.Setup("Ban", banEnlargeConfig.banCost, banEnlargeConfig.banRank, state, HandleBanClick);
	}

	private void RefreshEnlargeButton(PlayerProfile profile)
	{
		if (enlargeButton == null || banEnlargeConfig == null) return;

		var state = GetToggleState(profile.enlargeOwned, profile.enlargeEquipped, profile.GetRankNumber(), banEnlargeConfig.enlargeRank);
		enlargeButton.Setup("Enlarge", banEnlargeConfig.enlargeCost, banEnlargeConfig.enlargeRank, state, HandleEnlargeClick);
	}

	private UpgradeToggleButtonUI.State GetToggleState(bool owned, bool equipped, int currentRank, int requiredRank)
	{
		if (!owned)
			return currentRank >= requiredRank ? UpgradeToggleButtonUI.State.Buyable : UpgradeToggleButtonUI.State.Locked;

		return equipped ? UpgradeToggleButtonUI.State.OwnedOn : UpgradeToggleButtonUI.State.OwnedOff;
	}

	private void HandleBanClick()
	{
		var manager = PlayerProfileManager.Instance;
		if (manager == null || banEnlargeConfig == null) return;

		var profile = manager.GetProfile();

		if (!profile.banOwned)
		{
			if (!manager.CanPurchaseBan(banEnlargeConfig.banCost, banEnlargeConfig.banRank)) return;
			manager.PurchaseBan(banEnlargeConfig.banCost);
			manager.SetBanEquipped(true);
			return;
		}

		manager.SetBanEquipped(!profile.banEquipped);
	}

	private void HandleEnlargeClick()
	{
		var manager = PlayerProfileManager.Instance;
		if (manager == null || banEnlargeConfig == null) return;

		var profile = manager.GetProfile();

		if (!profile.enlargeOwned)
		{
			if (!manager.CanPurchaseEnlarge(banEnlargeConfig.enlargeCost, banEnlargeConfig.enlargeRank)) return;
			manager.PurchaseEnlarge(banEnlargeConfig.enlargeCost);
			manager.SetEnlargeEquipped(true);
			return;
		}

		manager.SetEnlargeEquipped(!profile.enlargeEquipped);
	}

	// --- Rank progression column (built once, highlighted on refresh) ---

	private void BuildRankColumn()
	{
		if (rankColumnContent == null || rankRowPrefab == null) return;

		foreach (var row in rankRows)
		{
			if (row != null) Destroy(row.gameObject);
		}
		rankRows.Clear();

		for (int rank = 1; rank <= MaxRank; rank++)
		{
			string summary = BuildRankSummary(rank);
			var row = Instantiate(rankRowPrefab, rankColumnContent);
			row.Setup(rank, summary);
			rankRows.Add(row);
		}
	}

	private string BuildRankSummary(int rank)
	{
		var parts = new List<string>();
		int tierIndex = rank - 1;

		if (rank == 1)
		{
			parts.Add("Starting loadout");
		}
		else
		{
			if (saveForLaterConfig != null && tierIndex < saveForLaterConfig.tiers.Length)
				parts.Add($"Save For Later {saveForLaterConfig.tiers[tierIndex].label}");
			if (movementConfig != null && tierIndex < movementConfig.tiers.Length)
				parts.Add($"Movement {movementConfig.tiers[tierIndex].label}");
			if (laneConfig != null && tierIndex < laneConfig.tiers.Length)
				parts.Add($"{laneConfig.tiers[tierIndex].label} Lanes");
		}

		if (banEnlargeConfig != null && rank == banEnlargeConfig.banRank)
			parts.Add("Ban");
		if (banEnlargeConfig != null && rank == banEnlargeConfig.enlargeRank)
			parts.Add("Enlarge");

		return string.Join(", ", parts);
	}

	private void RefreshRankHighlight(PlayerProfile profile)
	{
		int currentRank = profile.GetRankNumber();
		for (int i = 0; i < rankRows.Count; i++)
		{
			rankRows[i].SetCurrent(i + 1 == currentRank);
		}
	}
}
