using UnityEngine;

[RequireComponent(typeof(LaneStepMovement))]
[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(PlayerMovement))]
public class PlayerMovementModeController : MonoBehaviour
{
	[SerializeField] private MovementUpgradeConfig config;

	private LaneStepMovement laneStepMovement;
	private PlayerInput playerInput;
	private PlayerMovement playerMovement;

	private void Awake()
	{
		laneStepMovement = GetComponent<LaneStepMovement>();
		playerInput = GetComponent<PlayerInput>();
		playerMovement = GetComponent<PlayerMovement>();
	}

	private void Start()
	{
		ApplyEquippedTier();
	}

	private void ApplyEquippedTier()
	{
		int tier = 0;
		if (PlayerProfileManager.Instance != null)
			tier = PlayerProfileManager.Instance.GetProfile().movementUpgrade.equippedTier;

		if (config == null || config.tiers == null || tier < 0 || tier >= config.tiers.Length)
		{
			Debug.LogWarning("PlayerMovementModeController: Missing or invalid MovementUpgradeConfig, defaulting to lane step.");
			SetLaneStep();
			return;
		}

		var tierData = config.tiers[tier];
		if (tierData.useLegacyLaneStep)
		{
			SetLaneStep();
		}
		else
		{
			laneStepMovement.enabled = false;
			playerInput.enabled = true;
			playerMovement.enabled = true;
			playerMovement.SetSpeed(tierData.speed);
		}
	}

	private void SetLaneStep()
	{
		laneStepMovement.enabled = true;
		playerInput.enabled = false;
		playerMovement.enabled = false;
	}
}