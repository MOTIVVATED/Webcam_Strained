using UnityEngine;

public class DistractionObject : FallingObject
{
  [SerializeField] private Sprite distractionImage;

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (!other.CompareTag("Player"))
			return;

		DistractionOverlay.Instance.Show(distractionImage);
		//OnSmashed?.Invoke(this);
		Destroy(gameObject);
	}
}
