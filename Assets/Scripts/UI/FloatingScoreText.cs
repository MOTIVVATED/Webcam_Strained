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
			case FallingObjectType.Bad:
			text.text = "BAN";
			text.color = Color.green;
			break;
		}
	}
	public void Setup(int amount, FallingObjectType type)
	{

		switch (type)
		{
			//case FallingObjectType.type_1tk:
			//    text.text = "+" + amount.ToString() + "tk";
			//    text.color = grey;
			//    break;

			case FallingObjectType.type_15tk:
				text.text = "+" + amount.ToString() + "tk";
				text.color = grey;
				break;
			case FallingObjectType.type_25tk:
				text.text = "+" + amount.ToString() + "tk";
				text.color = paleBlue;
				break;
			case FallingObjectType.type_111tk:;
				text.text = "+" + amount.ToString() + "tk";
				text.color = blue;
				break;
			case FallingObjectType.type_222tk:
				text.text = "+" + amount.ToString() + "tk";
				text.color = blue;
				break;
			case FallingObjectType.type_555tk:
				text.text = "+" + amount.ToString() + "tk";
				text.color = purple;
				break;
			case FallingObjectType.type_666tk:
				text.text = "+" + amount.ToString() + "tk";
				text.color = purple;
				break;
			case FallingObjectType.type_1111tk:
				text.text = "+" + amount.ToString() + "tk";
				text.color = darkPurple;
				break;
			case FallingObjectType.Bad:
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
