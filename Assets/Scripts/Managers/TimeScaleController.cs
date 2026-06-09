using UnityEngine;

public class TimeScaleController : MonoBehaviour
{
	public static TimeScaleController Instance { get; private set; }

	private float _progression	= 1f;
	private float _penalty			= 1f;
	private bool _paused				= false;

	private void Awake()
	{
		if (Instance != null && Instance != this) { Destroy(gameObject); }
		Instance = this;
	}

	public void SetProgression( float value)
	{
		_progression = Mathf.Max(0f, value);
		Apply();
	}

	public void SetPenalty(float value)
	{
		_penalty = Mathf.Clamp01(value);
		Apply();
	}

	public void SetPaused(bool paused)
	{
		_paused = paused;
		Apply();
	}

	public void SetFrozen()
	{
		Time.timeScale = 0f;
	}
	public void Unfreeze()
	{
		Time.timeScale = 1f;
	}

	private void Apply()
	{
		if(_paused)
		{
			Time.timeScale = 0f;
			return;
		}
		Time.timeScale = _progression * _penalty;
	}
}
