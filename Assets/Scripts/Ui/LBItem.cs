using System;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class LBItem : MonoBehaviour
{
	[SerializeField]
	TextMeshProUGUI pos;

	[SerializeField]
	TextMeshProUGUI id;

	[SerializeField]
	TextMeshProUGUI level;

	public void Init(int pos, string name, int level)
	{
		this.pos.text = pos.ToString();
		this.id.text = name.ToString();
		this.level.text = level.ToString();
	}
}


