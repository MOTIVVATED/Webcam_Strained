using System;
using UnityEngine;

public class FallingObject : MonoBehaviour
{
  [SerializeField] private FallingObjectType objectType;
  [SerializeField] private SpriteRenderer spriteRenderer;

  [SerializeField] private float fallSpeed = 3f;
  [SerializeField] private float destroyY = -6f;

  public FallingObjectType ObjectType => objectType;

  public event Action<FallingObjectType> OnCollected;
    
  public event Action<FallingObjectType> OnMissed;

  public event Action<FallingObjectType, Vector3> OnSmashed;

  private void Awake()
  {
    if (spriteRenderer == null)
    spriteRenderer = GetComponent<SpriteRenderer>();
  }

  private void Reset()
  {
    spriteRenderer = GetComponent<SpriteRenderer>();
  }
  public void SetSprite(Sprite sprite)
  {
    if (spriteRenderer != null && sprite != null)
    spriteRenderer.sprite = sprite;
  }

  void Update()
  {
    Fall();
    CheckDestroy();
  }

  private void Fall()
  { 
    transform.position += Vector3.down * fallSpeed * Time.deltaTime;
  }

  private void CheckDestroy()
  {
    if (transform.position.y <= destroyY)
    {
      OnMissed?.Invoke(objectType);
      Destroy(gameObject);
    }
  }
  public void Collect()
  {
    OnCollected?.Invoke(objectType);
    Destroy(gameObject);
  }
	public void Smash()
	{
		Vector3 pos = transform.position;
		OnSmashed?.Invoke(objectType, pos);
		Destroy(gameObject);
	}
}
