using UnityEngine;

public class TimeScaleController : MonoBehaviour
{
	public static TimeScaleController Instance { get; private set; }

	private float _progression	= 1f;
	private float _penalty			= 1f;
	private bool _paused				= false;

	//private float _timeScaleMin = 0.7f;
	//private float _timeScaleMax = 2f;

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
		//_penalty = Mathf.Clamp01(value);
				
		Debug.Log(Time.timeScale + " * " + _penalty + " = ...");

		//Apply();
		Time.timeScale *= value;

		Debug.Log("... = " + Time.timeScale);
	}

	public void SetPaused(bool paused)
	{
		_paused = paused;
		Apply();
	}

	public void SetFrozen()
	{
		Time.timeScale = 0f;
		Debug.Log("SetFrozen! Time.timeScale: " +  Time.timeScale);
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
		// Time.timeScale = Mathf.Clamp( _progression * _penalty, _timeScaleMin, _timeScaleMax);

	}
}
