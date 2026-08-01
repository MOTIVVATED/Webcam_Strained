using System.Collections.Generic;
using UnityEngine;

public class LeaderboardsPanelUI : MonoBehaviour
{
	[Header("Panel")]
	[SerializeField] private GameObject panelRoot;
	[SerializeField] private GameObject loadingText;
	[SerializeField] private GameObject notAvailableText;

	[Header("Rows")]
	[SerializeField] private Transform content;
	[SerializeField] private LeaderboardRowUI rowPrefab;
	[SerializeField] private LeaderboardRowUI ownEntryRow;

	[Header("Buttons")]
	[SerializeField] private GameObject closeButton;

	private bool _requestInProgress;

	// Content -> Viewport -> ScrollView
	private GameObject ScrollViewRoot => content.parent.parent.gameObject;

	private readonly List<LeaderboardRowUI> _spawnedRows = new List<LeaderboardRowUI>();

	private void OnEnable()
	{
		if (SteamLeaderboardManager.Instance != null)
		{
			SteamLeaderboardManager.Instance.OnTopEntriesReady += HandleTopEntriesReady;
			SteamLeaderboardManager.Instance.OnOwnEntryReady += HandleOwnEntryReady;
		}
	}

	private void OnDisable()
	{
		if (SteamLeaderboardManager.Instance != null)
		{
			SteamLeaderboardManager.Instance.OnTopEntriesReady -= HandleTopEntriesReady;
			SteamLeaderboardManager.Instance.OnOwnEntryReady -= HandleOwnEntryReady;
		}
	}

	public void Open()
	{
		panelRoot.SetActive(true);

		if (!SteamManager.Initialized)
		{
			notAvailableText.SetActive(true);
			loadingText.SetActive(false);
			ScrollViewRoot.SetActive(false);
			ownEntryRow.gameObject.SetActive(false);
			return;
		}

		loadingText.SetActive(true);
		notAvailableText.SetActive(false);
		ScrollViewRoot.SetActive(false);
		ownEntryRow.gameObject.SetActive(false);

		if (_requestInProgress)
			return;

		_requestInProgress = true;
		SteamLeaderboardManager.Instance.RequestTopAndOwnEntries();
	}

	public void Close()
	{
		panelRoot.SetActive(false);
	}

	private void HandleTopEntriesReady(List<LeaderboardEntryData> entries)
	{
		foreach (LeaderboardRowUI row in _spawnedRows)
		{
			if (row != null) Destroy(row.gameObject);
		}
		_spawnedRows.Clear();

		foreach (LeaderboardEntryData entry in entries)
		{
			LeaderboardRowUI row = Instantiate(rowPrefab, content);
			row.Setup(entry);
			_spawnedRows.Add(row);
		}

		loadingText.SetActive(false);
		ScrollViewRoot.SetActive(true);
		_requestInProgress = false;
	}

	private void HandleOwnEntryReady(LeaderboardEntryData entry)
	{
		ownEntryRow.gameObject.SetActive(true);
		ownEntryRow.Setup(entry);
	}
}
