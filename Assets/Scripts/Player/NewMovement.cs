using UnityEngine;

public class NewMovement : MonoBehaviour
{
	[SerializeField] int minX = -3;
	[SerializeField] int maxX = 3;
	[SerializeField] bool looping = false;

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.RightArrow) && !Input.GetKeyDown(KeyCode.LeftArrow))
		{
			MoveRight();
		}
		else if (Input.GetKeyDown(KeyCode.LeftArrow) && !Input.GetKeyDown(KeyCode.RightArrow))
		{
			MoveLeft();
		}
	}

	private void MoveRight()
	{
		Vector3 pos = transform.position;

		if (pos.x == maxX)
		{
			if (looping)
			{
				pos.x = minX;
				transform.position = pos;
				return;
			}
			else { return; }
		}

		pos.x = pos.x + 1;
		transform.position = pos;
	}

	private void MoveLeft()
	{
		Vector3 pos = transform.position;

		if (pos.x == minX)
		{
			if (looping)
			{
				pos.x = maxX;
				transform.position = pos;
				return;
			}
			else { return; }
		}

		pos.x = pos.x + -1;
		transform.position = pos;
	}
}
