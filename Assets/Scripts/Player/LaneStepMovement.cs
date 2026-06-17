using UnityEngine;

public class LaneStepMovement : MonoBehaviour
{
	[SerializeField] int minX = -3;
	[SerializeField] int maxX = 3;
	[SerializeField] bool looping = false;

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.RightArrow))
		{
			MoveRight();
		}
		else if (Input.GetKeyDown(KeyCode.LeftArrow))
		{
			MoveLeft();
		}
	}

	private void MoveRight()
	{
		Vector3 pos = transform.position;

		if (pos.x >= maxX)
		{
			pos.x = looping? minX : maxX;
			transform.position = pos;
			return;
		}

		pos.x += 1;
		transform.position = pos;
	}

	private void MoveLeft()
	{
		Vector3 pos = transform.position;

		if (pos.x <= minX)
		{
			pos.x = looping ? maxX : minX;
			transform.position = pos;
			return;
		}

		pos.x -= 1;
		transform.position = pos;
	}
}
