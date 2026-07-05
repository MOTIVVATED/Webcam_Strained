using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TimeScaleController : MonoBehaviour
{
	public static TimeScaleController Instance { get; private set; }

	private float _actualTimeScale = 1f;
	private bool _paused = false;

	private float _timeScaleFactor = 0.88f;
	private float _timeScaleIncrement = 0;

	[SerializeField] private float _timeScaleMin = 1f;
	[SerializeField] private float _timeScaleMax = 3f;

	[Header("timeScale Values for models")]
	[SerializeField] private float MafuLegenda = 0.01f;
	[SerializeField] private float ApexFunk = 0.015f;
	[SerializeField] private float PampyBam = 0.02f;
	[SerializeField] private float EnterYou = 0.025f;
	[SerializeField] private float SmellySam = 0.03f;



	private string _sceneName;

	private void Awake()
	{
		if (Instance != null && Instance != this) { Destroy(gameObject); }
		Instance = this;
	}

	public void SetTimeScale()
	{
		_sceneName = SceneManager.GetActiveScene().name;

		switch (_sceneName)
		{
			case "MafuLegenda": _timeScaleIncrement = MafuLegenda; break;
			case "ApexFunk": _timeScaleIncrement = ApexFunk; break;
			case "PampyBam": _timeScaleIncrement = PampyBam; break;
			case "EnterYou": _timeScaleIncrement = EnterYou; break;
			case "SmellySam": _timeScaleIncrement = SmellySam; break;

			default: _timeScaleIncrement = 0; break;
		}


		if ( !_paused && _actualTimeScale < _timeScaleMax)
		{
			_actualTimeScale += _timeScaleIncrement;
		}
		Apply();
	}

	public void ReduceTimeScale()
	{
		if (!_paused && _actualTimeScale > _timeScaleMin)
		{
			_actualTimeScale *= _timeScaleFactor;
		}
		Apply();
	}

	//One thing to be aware of: both TiltManager and ReducerManager
	//now call SetPenalty independently
	//They're setting the same value, so whichever calls last wins.
	//If we want them to stack properly — tilt penalty and reducer penalty
	//both active at the same time —
	//TimeScaleController needs two separate penalty slots.

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
		Time.timeScale = _actualTimeScale;
	}
}
