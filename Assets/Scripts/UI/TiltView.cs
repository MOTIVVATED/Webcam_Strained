using TMPro;
using UnityEngine;

public class TiltView : MonoBehaviour
{
	[SerializeField] private TMP_Text tiltText;

	private void Start()
	{
		StartCoroutine(WaitForInstance.Get(
				() => TiltManager.Instance,
				tm =>
				{
					tm.OnTiltIncreased += UpdateTilt;
					tm.OnTiltDecreased += UpdateTilt;
					UpdateTilt(tm.Tilt);
				}
		));
	}

	private void OnDestroy()
	{
		if (TiltManager.Instance != null)
		{
			TiltManager.Instance.OnTiltIncreased -= UpdateTilt;
			TiltManager.Instance.OnTiltDecreased -= UpdateTilt;
		}
	}

	private void UpdateTilt(int tilt) => tiltText.text = tilt + "%";
}