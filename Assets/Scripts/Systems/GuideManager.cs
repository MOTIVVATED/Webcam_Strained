using System;
using System.Collections.Generic;
using UnityEngine;

public class GuideManager : MonoBehaviour
{
	public static GuideManager Instance { get; private set; }

	[SerializeField] private GuideCharacterUI guideUI;
	[SerializeField] private GuideDialogueSet gameIntroSet;
	[SerializeField] private GuideDialogueSet gameGreetingsSet;
	[SerializeField] private GuideDialogueSet modelSelectionIntroSet;
	[SerializeField] private GuideDialogueSet levelingPanelIntroSet;
	[SerializeField] private GuideRankDialogueSet rankDialogueSet;

	private readonly Queue<Action> queue = new Queue<Action>();
	private bool isBusy;
	private Action pendingCompletion;

	private void Awake()
	{
		if (Instance != null && Instance != this) { Destroy(gameObject); return; }

		Instance = this;

		transform.SetParent(null);

		DontDestroyOnLoad(gameObject);
	}

	private void OnEnable()
	{
		if (guideUI != null)
			guideUI.OnSequenceCompleted += HandleSequenceCompleted;
	}

	private void OnDisable()
	{
		if (guideUI != null)
			guideUI.OnSequenceCompleted -= HandleSequenceCompleted;
	}

	public void ShowGameLaunchGuide()
	{
		Enqueue(() =>
		{
			if (guideUI == null) { FinishNoOp(); return; }

			var manager = PlayerProfileManager.Instance;
			if (manager == null) { FinishNoOp(); return; }

			var profile = manager.GetProfile();

			if (!profile.hasSeenGameIntroGuide)
			{
				pendingCompletion = () => manager.SetGameIntroGuideSeen();
				guideUI.PlaySequence(gameIntroSet != null ? gameIntroSet.lines : null);
				return;
			}

			if (gameGreetingsSet != null && gameGreetingsSet.lines != null && gameGreetingsSet.lines.Count > 0)
			{
				string line = gameGreetingsSet.lines[UnityEngine.Random.Range(0, gameGreetingsSet.lines.Count)];
				guideUI.PlaySingleLine(line);
				return;
			}

			FinishNoOp();
		});
	}

	public void OnModelSelectionPanelOpened()
	{
		Enqueue(() =>
		{
			if (guideUI == null) { FinishNoOp(); return; }

			var manager = PlayerProfileManager.Instance;
			if (manager == null) { FinishNoOp(); return; }

			var profile = manager.GetProfile();

			if (profile.hasSeenModelSelectionGuide) { FinishNoOp(); return; }

			pendingCompletion = () => manager.SetModelSelectionGuideSeen();
			guideUI.PlaySequence(modelSelectionIntroSet != null ? modelSelectionIntroSet.lines : null);
		});
	}

	public void OnLevelingPanelOpened(int currentRank)
	{
		Enqueue(() =>
		{
			if (guideUI == null) { FinishNoOp(); return; }

			var manager = PlayerProfileManager.Instance;
			if (manager == null) { FinishNoOp(); return; }

			var profile = manager.GetProfile();

			if (!profile.hasSeenLevelingPanelGuide)
			{
				pendingCompletion = () => manager.SetLevelingPanelGuideSeen();
				guideUI.PlaySequence(levelingPanelIntroSet != null ? levelingPanelIntroSet.lines : null);
				return;
			}

			if (currentRank > profile.highestRankGuideShown)
			{
				var entry = rankDialogueSet != null && rankDialogueSet.entries != null
					? rankDialogueSet.entries.Find(e => e.rankIndex == currentRank)
					: null;

				if (entry != null && entry.lines != null && entry.lines.Count > 0)
				{
					pendingCompletion = () => manager.SetHighestRankGuideShown(currentRank);
					guideUI.PlaySequence(entry.lines);
					return;
				}
			}

			FinishNoOp();
		});
	}

	private void Enqueue(Action showAction)
	{
		queue.Enqueue(showAction);
		ProcessQueue();
	}

	private void ProcessQueue()
	{
		if (isBusy) return;
		if (queue.Count == 0) return;

		isBusy = true;
		var action = queue.Dequeue();
		action.Invoke();
	}

	private void FinishNoOp()
	{
		isBusy = false;
		ProcessQueue();
	}

	private void HandleSequenceCompleted()
	{
		var completion = pendingCompletion;
		pendingCompletion = null;
		completion?.Invoke();

		isBusy = false;
		ProcessQueue();
	}
}
