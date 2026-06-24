using UnityEngine;

public class TiltAppearanceController : MonoBehaviour
{
	[SerializeField] private GameObject naked;
	[SerializeField] private GameObject underwear;
	[SerializeField] private GameObject dressed;

	[Header("Thresholds (upper bound of each band, in percent)")]
	[SerializeField] private int nakedMax = 33;
	[SerializeField] private int underwearMax = 66;

	private SpriteRenderer dressedRenderer;
	private SpriteRenderer underwearRenderer;
	private SpriteRenderer nakedRenderer;

	private Animator dressedAnimator;
	private Animator underwearAnimator;
	private Animator nakedAnimator;

	private void Start()
	{
		TiltManager.Instance.OnTiltIncreased += HandleTiltChanged;
		TiltManager.Instance.OnTiltDecreased += HandleTiltChanged;

		nakedRenderer = naked.GetComponent<SpriteRenderer>();
		underwearRenderer = underwear.GetComponent<SpriteRenderer>();
		dressedRenderer = dressed.GetComponent<SpriteRenderer>();

		nakedAnimator = naked.GetComponent<Animator>();
		underwearAnimator = underwear.GetComponent<Animator>();
		dressedAnimator = dressed.GetComponent<Animator>();

		// All three animators run continuously and stay in sync until the game ends.
		nakedAnimator.enabled = true;
		underwearAnimator.enabled = true;
		dressedAnimator.enabled = true;

		// Naked is the base layer and is always visible.
		nakedRenderer.enabled = true;

		UpdateAppearance(TiltManager.Instance.Tilt);
	}

	private void OnDisable()
	{
		TiltManager.Instance.OnTiltIncreased -= HandleTiltChanged;
		TiltManager.Instance.OnTiltDecreased -= HandleTiltChanged;
	}

	private void HandleTiltChanged(int tilt)
	{
		UpdateAppearance(tilt);
	}

	private void UpdateAppearance(int tilt)
	{
		bool showUnderwear = tilt >= nakedMax;
		bool showDressed = tilt >= underwearMax;

		underwearRenderer.enabled = showUnderwear;
		dressedRenderer.enabled = showDressed;
		// nakedRenderer stays enabled permanently — set once in Start.
	}
}