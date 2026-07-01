using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeToggleButtonUI : MonoBehaviour
{
	public enum State { Locked, Buyable, OwnedOff, OwnedOn }

	[SerializeField] private TMP_Text labelText;
	[SerializeField] private TMP_Text costText;
	[SerializeField] private Image background;
	[SerializeField] private Button button;

	[Header("State Colors")]
	[SerializeField] private Color lockedColor = new Color(0.4f, 0.4f, 0.4f);
	[SerializeField] private Color buyableColor = new Color(0.9f, 0.8f, 0.2f);
	[SerializeField] private Color ownedOffColor = new Color(0.3f, 0.5f, 0.9f);
	[SerializeField] private Color ownedOnColor = new Color(0.25f, 0.75f, 0.35f);

	private Action onClick;

	public void Setup(string label, int cost, int requiredRank, State state, Action onClickCallback)
	{
		if (labelText != null) labelText.text = label;

		if (costText != null)
		{
			costText.text = state == State.Buyable ? $"{cost}"
				: state == State.Locked ? $"Rank {requiredRank}"
				: string.Empty;
		}

		onClick = onClickCallback;

		if (button != null)
		{
			button.interactable = state != State.Locked;
			button.onClick.RemoveAllListeners();
			button.onClick.AddListener(HandleClick);
		}

		if (background != null)
		{
			switch (state)
			{
				case State.Locked: background.color = lockedColor; break;
				case State.Buyable: background.color = buyableColor; break;
				case State.OwnedOff: background.color = ownedOffColor; break;
				case State.OwnedOn: background.color = ownedOnColor; break;
			}
		}
	}

	private void HandleClick()
	{
		onClick?.Invoke();
	}
}