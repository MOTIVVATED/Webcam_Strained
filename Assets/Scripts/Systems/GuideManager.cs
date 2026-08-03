using System;
using UnityEngine;

public class GuideManager : MonoBehaviour
{
	public static GuideManager Instance { get; private set; }

	[SerializeField] private GuideCharacterUI guideUI;
	[SerializeField] private GuideDialogueSet modelSelectionDialogueSet;
	[SerializeField] private GuideDialogueSet levelingPanelDialogueSet;

	private void Awake()
	{
		if (Instance != null && Instance != this) { Destroy(gameObject); return; }

		Instance = this;

		transform.SetParent(null);

		DontDestroyOnLoad(gameObject);
	}

	public void ShowModelSelectionGuide(Action onComplete)
		=> guideUI.PlaySequence(modelSelectionDialogueSet.lines, onComplete);

	public void ShowLevelingGuide(Action onComplete)
		=> guideUI.PlaySequence(levelingPanelDialogueSet.lines, onComplete);
}
