using UnityEngine;
using TMPro;

public class BonusNumber : BonusBase
{
	[SerializeField]
	TextMeshPro text;

	public void SetNumber(int number)
	{
		text.text = number.ToString();
	}
	public override void Collect()
	{
		Action(this);
		Explode();
		Falling = false;
	}
}

