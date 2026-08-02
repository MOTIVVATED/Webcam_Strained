using UnityEngine;

[RequireComponent(typeof(Collider2D))]

public class Player : MonoBehaviour
{
	private void Start()
	{
		bool enlargeEquipped = PlayerProfileManager.Instance != null
				&& PlayerProfileManager.Instance.GetProfile().enlargeEquipped;

		if (enlargeEquipped)
		{
			Vector3 scale = transform.localScale;
			scale.x *= 1.7f;
			scale.y *= 1.7f;
			transform.localScale = scale;
		}
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.TryGetComponent(out FallingObject fallingObject))
		{
			fallingObject.Collect();
		}
	}
}