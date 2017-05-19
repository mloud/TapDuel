using UnityEngine;

public class GameVisual : MonoBehaviour
{
	[SerializeField]
	LineController lineController;

	[SerializeField]
	Transform leftLine;

	[SerializeField]
	Transform rightLine;

	public void MoveLine(float x)
	{
		lineController.Move(x);
	}

	public float GetLeftWinLinePosition()
	{
		return leftLine.position.x;
	}

	public float GetRightWinLinePosition()
	{
		return rightLine.position.x;
	}

	public float GetLinePosition()
	{
		return lineController.GetPosition();
	}

	public void PlayLineAppear()
	{
		lineController.PlayAppear();
	}
}
