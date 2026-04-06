using TMPro;
using UnityEngine;

public class FloatingScoreText : MonoBehaviour
{
	[SerializeField] private TMP_Text text;
	[SerializeField] private float floatUpSpeed;
	[SerializeField] private float lifetime;
	[SerializeField] private float fadeDuration;

	Color32 grey =      new Color32(147, 147, 147, 255);
	Color32 paleBlue =  new Color32(102, 153, 170, 255);
	Color32 blue =      new Color32(30, 92, 239, 255);
	Color32 purple =    new Color32(190, 106, 255, 255);
	Color32 darkPurple = new Color32(128, 75, 170, 255);

	private float timer;
	private Color startColor;

	public void Setup(FallingObjectType type)
	{
		switch (type)
		{
			case FallingObjectType.bad:
				text.text = "BAN";
				text.color = Color.green;
				break;
			case FallingObjectType.reduser:
				text.text = "SLOW";
				text.color = Color.green;
				break;
			case FallingObjectType.webcam:
				text.text = "BAN";
				text.color = Color.green;
				break;
		}
	}
	public void Setup(int amount, FallingObjectType type)
	{

		switch (type)
		{
			case FallingObjectType.tk15:
				text.text = "+" + amount.ToString() + "tk";
				text.color = grey;
				break;
			case FallingObjectType.tk25:
				text.text = "+" + amount.ToString() + "tk";
				text.color = paleBlue;
				break;
			case FallingObjectType.tk111:;
				text.text = "+" + amount.ToString() + "tk";
				text.color = blue;
				break;
			case FallingObjectType.tk222:
				text.text = "+" + amount.ToString() + "tk";
				text.color = blue;
				break;
			case FallingObjectType.tk555:
				text.text = "+" + amount.ToString() + "tk";
				text.color = purple;
				break;
			case FallingObjectType.tk666:
				text.text = "+" + amount.ToString() + "tk";
				text.color = purple;
				break;
			case FallingObjectType.tk1111:
				text.text = "+" + amount.ToString() + "tk";
				text.color = darkPurple;
				break;
			case FallingObjectType.bad:
				text.text = "+" + amount.ToString() + "tilt";
				text.color = Color.red;
				break;
			case FallingObjectType.webcam:
				text.text = "+" + amount.ToString() + "tilt";
				text.color = Color.red;
				break;
		}
	}
	private void Update()
	{
		transform.position += Vector3.up * floatUpSpeed * Time.deltaTime;

		timer += Time.deltaTime;

		float fadeStart = lifetime - fadeDuration;
		if ( timer >= fadeStart)
		{
			float t = Mathf.InverseLerp(lifetime, fadeStart, timer);

			var c = startColor;

			c.a = t;
			text.color = c;
		}
		if (timer >= lifetime)
		{
			Destroy(gameObject);
		}
	}
}
