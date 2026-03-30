using UnityEngine;

public class FloatingTextSpawner : MonoBehaviour
{
	[SerializeField] private FloatingScoreText prefab;
	[SerializeField] private Canvas canvas;
	[SerializeField] private Camera mainCamera;

	public static FloatingTextSpawner Instance { get; private set; }

	private void Awake()
	{
		if (Instance != null && Instance != this)
		{
		Destroy(this.gameObject);
		return;
		}
		Instance = this;
	}
	public void Spawn(Vector3 worldPos)
	{
		if (mainCamera == null) mainCamera = Camera.main;

		Vector3 screenPos = mainCamera.WorldToScreenPoint(worldPos);

		var instance = Instantiate(prefab, canvas.transform);
		instance.transform.position = screenPos;

		instance.Setup(FallingObjectType.Bad);
	}
	public void Spawn(int amount, Vector3 worldPos, FallingObjectType type)
	{
		if (mainCamera == null) mainCamera = Camera.main;

		Vector3 screenPos = mainCamera.WorldToScreenPoint(worldPos);

		var instance = Instantiate(prefab, canvas.transform);
		instance.transform.position = screenPos;

		switch (type)
		{
			case FallingObjectType.reduser:
				instance.Setup(FallingObjectType.reduser);
				break;
			case FallingObjectType.webcam:
				instance.Setup(amount, FallingObjectType.webcam);
				break;
			case FallingObjectType.Bad:
				instance.Setup(amount, FallingObjectType.Bad);
				break;
			case FallingObjectType.type_1tk:
				instance.Setup(amount, FallingObjectType.type_1tk);
				break;
			case FallingObjectType.type_15tk:
				instance.Setup(amount, FallingObjectType.type_15tk);
				break;
			case FallingObjectType.type_25tk:
				instance.Setup(amount, FallingObjectType.type_25tk);
				break;
			case FallingObjectType.type_111tk:
				instance.Setup(amount, FallingObjectType.type_111tk);
				break;
			case FallingObjectType.type_222tk:
				instance.Setup(amount, FallingObjectType.type_222tk);
				break;
			case FallingObjectType.type_555tk:
				instance.Setup(amount, FallingObjectType.type_555tk);
				break;
			case FallingObjectType.type_666tk:
				instance.Setup(amount, FallingObjectType.type_666tk);
				break;
			case FallingObjectType.type_1111tk:
				instance.Setup(amount, FallingObjectType.type_1111tk);
				break;
		}
	}
}
