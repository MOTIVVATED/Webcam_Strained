using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GuideCharacterUI : MonoBehaviour
{
	[SerializeField] private Image characterImage;
	[SerializeField] private Animator characterAnimator; // bool parameter "IsTalking"
	[SerializeField] private TMP_Text lineText;
	[SerializeField] private Button nextButton;
	[SerializeField] private CanvasGroup canvasGroup;
	[SerializeField] private GameObject panelRoot;
	[SerializeField] private float fadeDuration = 0.25f;

	private static readonly int IsTalkingHash = Animator.StringToHash("IsTalking");

	public event Action OnSequenceCompleted;

	private List<string> currentLines;
	private int currentIndex;
	private Coroutine fadeRoutine;

	private void Awake()
	{
		if (nextButton != null)
			nextButton.onClick.AddListener(HandleNextClicked);
	}

	private void OnDestroy()
	{
		if (nextButton != null)
			nextButton.onClick.RemoveListener(HandleNextClicked);
	}

	public void PlaySequence(List<string> lines)
	{
		if (lines == null || lines.Count == 0)
		{
			OnSequenceCompleted?.Invoke();
			return;
		}

		currentLines = lines;
		currentIndex = 0;
		StartSequence();
	}

	public void PlaySingleLine(string line)
	{
		PlaySequence(new List<string> { line });
	}

	private void StartSequence()
	{
		panelRoot.SetActive(true);
		ShowCurrentLine();
		SetTalking(true);

		if (fadeRoutine != null) StopCoroutine(fadeRoutine);
		fadeRoutine = StartCoroutine(Fade(0f, 1f));
	}

	private void ShowCurrentLine()
	{
		if (lineText != null)
			lineText.text = currentLines[currentIndex];
	}

	private void HandleNextClicked()
	{
		currentIndex++;

		if (currentIndex >= currentLines.Count)
		{
			EndSequence();
			return;
		}

		ShowCurrentLine();
	}

	private void EndSequence()
	{
		SetTalking(false);

		if (fadeRoutine != null) StopCoroutine(fadeRoutine);
		fadeRoutine = StartCoroutine(Fade(1f, 0f, () =>
		{
			panelRoot.SetActive(false);
			OnSequenceCompleted?.Invoke();
		}));
	}

	private void SetTalking(bool talking)
	{
		if (characterAnimator != null)
			characterAnimator.SetBool(IsTalkingHash, talking);
	}

	private IEnumerator Fade(float from, float to, Action onComplete = null)
	{
		if (canvasGroup == null)
		{
			onComplete?.Invoke();
			yield break;
		}

		canvasGroup.alpha = from;
		float elapsed = 0f;

		while (elapsed < fadeDuration)
		{
			elapsed += Time.unscaledDeltaTime;
			canvasGroup.alpha = Mathf.Lerp(from, to, elapsed / fadeDuration);
			yield return null;
		}

		canvasGroup.alpha = to;
		onComplete?.Invoke();
	}
}
