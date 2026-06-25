using UnityEngine;

public class AppearanceController : MonoBehaviour
{
	[SerializeField] private GameObject naked;
	[SerializeField] private GameObject underwear;
	[SerializeField] private GameObject dressed;

	[Header("Thresholds")]
	[SerializeField] private int underwearMin = 11;
	[SerializeField] private int dressedMin = 69;

	private SpriteRenderer dressedRenderer;
	private SpriteRenderer underwearRenderer;
	private SpriteRenderer nakedRenderer;

	private Animator dressedAnimator;
	private Animator underwearAnimator;
	private Animator nakedAnimator;

	private static int ValuableObjectsCollected = 0;
	[SerializeField] private int ValuableObjectsForDress = 4;
	[SerializeField] private int ValuableObjectsForUnderwear = 8;


	private void Start()
	{
		TiltManager.Instance.OnTiltIncreased += HandleTiltChanged;
		TiltManager.Instance.OnTiltDecreased += HandleTiltChanged;

		GameEvents.OnObjectCollected += FirstValuableObjectCollected;

		nakedRenderer = naked.GetComponent<SpriteRenderer>();
		underwearRenderer = underwear.GetComponent<SpriteRenderer>();
		dressedRenderer = dressed.GetComponent<SpriteRenderer>();

		nakedAnimator = naked.GetComponent<Animator>();
		underwearAnimator = underwear.GetComponent<Animator>();
		dressedAnimator = dressed.GetComponent<Animator>();

		nakedAnimator.enabled = true;
		underwearAnimator.enabled = true;
		dressedAnimator.enabled = true;

		nakedRenderer.enabled = true;
		underwearAnimator.enabled = true;
		dressedAnimator.enabled = true;

		ValuableObjectsCollected = 0;

		UpdateAppearance(TiltManager.Instance.Tilt);
	}

	private void OnDisable()
	{
		TiltManager.Instance.OnTiltIncreased -= HandleTiltChanged;
		TiltManager.Instance.OnTiltDecreased -= HandleTiltChanged;

		GameEvents.OnObjectCollected -= FirstValuableObjectCollected;
	}

	private void FirstValuableObjectCollected(FallingObjectType type, Vector3 pos)
	{
		switch (type)
		{
			case FallingObjectType.tk111:
			case FallingObjectType.tk222:
			case FallingObjectType.tk555:
			case FallingObjectType.tk666:
			case FallingObjectType.tk1111:
				ValuableObjectsCollected++;
				break;
		}
	}

	private void HandleTiltChanged(int tilt)
	{
		UpdateAppearance(tilt);
	}

	private void UpdateAppearance(int tilt)
	{
		if (ValuableObjectsCollected < ValuableObjectsForDress) { return; }
		
		bool showDressed = tilt >= dressedMin;
		dressedRenderer.enabled = showDressed;
		
		if (ValuableObjectsCollected < ValuableObjectsForUnderwear) { return; }
		bool showUnderwear = tilt >= underwearMin;
		underwearRenderer.enabled = showUnderwear;

	}
}