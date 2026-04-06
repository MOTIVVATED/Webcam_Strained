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

		instance.Setup(FallingObjectType.bad);
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
			case FallingObjectType.bad:
				instance.Setup(amount, FallingObjectType.bad);
				break;
			case FallingObjectType.tk1:
				instance.Setup(amount, FallingObjectType.tk1);
				break;
			case FallingObjectType.tk15:
				instance.Setup(amount, FallingObjectType.tk15);
				break;
			case FallingObjectType.tk25:
				instance.Setup(amount, FallingObjectType.tk25);
				break;
			case FallingObjectType.tk111:
				instance.Setup(amount, FallingObjectType.tk111);
				break;
			case FallingObjectType.tk222:
				instance.Setup(amount, FallingObjectType.tk222);
				break;
			case FallingObjectType.tk555:
				instance.Setup(amount, FallingObjectType.tk555);
				break;
			case FallingObjectType.tk666:
				instance.Setup(amount, FallingObjectType.tk666);
				break;
			case FallingObjectType.tk1111:
				instance.Setup(amount, FallingObjectType.tk1111);
				break;
		}
	}
}
