using System.Collections;
using TMPro;
using UnityEngine;

public class MultiSalePopup : MonoBehaviour
{
	public static MultiSalePopup Instance { get; private set; }

	[SerializeField] private TMP_Text popupText;

	[Header("Timing")]
	[SerializeField] private float punchInDuration = 0.15f;
	[SerializeField] private float holdDuration = 0.6f;
	[SerializeField] private float fadeOutDuration = 0.25f;

	[Header("Scale")]
	[SerializeField] private float overshootScale = 1.2f;
	[SerializeField] private float settleScale = 1f;

	[Header("Flash Colors")]
	[SerializeField]
	private Color[] flashColors = new Color[]
	{
		new Color(1f, 0.9f, 0.1f),
		new Color(1f, 0.2f, 0.4f),
		new Color(0.2f, 0.9f, 1f)
	};
	[SerializeField] private float colorCycleSpeed = 12f;

	private Coroutine activeRoutine;

	private void Awake()
	{
		if (Instance != null && Instance != this) { Destroy(gameObject); return; }
		Instance = this;

		if (popupText != null)
			popupText.gameObject.SetActive(false);
	}

	public void Show(int points)
	{
		if (popupText == null)
		{
			Debug.LogWarning("MultiSalePopup: popupText not assigned.");
			return;
		}

		if (activeRoutine != null)
			StopCoroutine(activeRoutine);

		popupText.text = $"MultiSale: {points}";
		activeRoutine = StartCoroutine(PlayAnimation());
	}

	private IEnumerator PlayAnimation()
	{
		popupText.gameObject.SetActive(true);
		Transform t = popupText.transform;

		// Punch in: scale 0 -> overshoot
		float elapsed = 0f;
		while (elapsed < punchInDuration)
		{
			elapsed += Time.unscaledDeltaTime;
			float progress = Mathf.Clamp01(elapsed / punchInDuration);
			float scale = Mathf.Lerp(0f, overshootScale, progress);
			t.localScale = Vector3.one * scale;
			SetFlashColor(elapsed);
			yield return null;
		}

		// Settle to normal scale quickly
		t.localScale = Vector3.one * settleScale;

		// Hold with color flashing, full opacity
		elapsed = 0f;
		SetAlpha(1f);
		while (elapsed < holdDuration)
		{
			elapsed += Time.unscaledDeltaTime;
			SetFlashColor(elapsed);
			yield return null;
		}

		// Fade out
		elapsed = 0f;
		while (elapsed < fadeOutDuration)
		{
			elapsed += Time.unscaledDeltaTime;
			float progress = Mathf.Clamp01(elapsed / fadeOutDuration);
			SetAlpha(1f - progress);
			SetFlashColor(elapsed);
			yield return null;
		}

		popupText.gameObject.SetActive(false);
		activeRoutine = null;
	}

	private void SetFlashColor(float elapsed)
	{
		if (flashColors == null || flashColors.Length == 0) return;

		float cyclePos = elapsed * colorCycleSpeed;
		int index = Mathf.FloorToInt(cyclePos) % flashColors.Length;
		int nextIndex = (index + 1) % flashColors.Length;
		float blend = cyclePos - Mathf.Floor(cyclePos);

		Color c = Color.Lerp(flashColors[index], flashColors[nextIndex], blend);
		c.a = popupText.color.a;
		popupText.color = c;
	}

	private void SetAlpha(float alpha)
	{
		Color c = popupText.color;
		c.a = alpha;
		popupText.color = c;
	}
}