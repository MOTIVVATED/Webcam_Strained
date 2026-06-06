using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
	public float Horizontal { get; private set; }

	private PlayerControls controls;

	private void Awake()
	{
		controls = new PlayerControls();
	}

	private void OnEnable()
	{
		controls.Player.Enable();
	}

	private void OnDisable()
	{
		controls.Player.Disable();
	}

	private void Update()
	{
		Horizontal = controls.Player.Move.ReadValue<Vector2>().x;
	}
}