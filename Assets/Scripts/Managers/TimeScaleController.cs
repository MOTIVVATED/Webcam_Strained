using UnityEngine;

public class TimeScaleController : MonoBehaviour
{
	public static TimeScaleController Instance { get; private set; }

	private float _actualTimeScale	= 1f;
	private float _timeScaleFactor	= 1f;
	private bool _paused	= false;

	private float _timeScaleMin = 0.7f;
	private float _timeScaleMax = 2f;

	private void Awake()
	{
		if (Instance != null && Instance != this) { Destroy(gameObject); }
		Instance = this;
	}

	public void SetTimeScale( float value)
	{
		_actualTimeScale = Mathf.Max(0f, value);
		Apply();
	}

	public void SetPenalty(float value)
	{
		//_penalty = Mathf.Clamp01(value);
		_timeScaleFactor = Mathf.Max(0f, value);

		_actualTimeScale = Mathf.Max(_timeScaleMin, Time.timeScale);

		Debug.Log(_actualTimeScale + " * " + _timeScaleFactor + " = ...");

		Apply();

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
		Time.timeScale = Mathf.Max( 
			_timeScaleMin, _actualTimeScale * _timeScaleFactor );

	}
}
