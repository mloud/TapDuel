using TMPro;
using UnityEngine;
using Data;
using System;

public class BeatMeItem : MonoBehaviour
{
	[SerializeField]
	TextMeshProUGUI pos;

	[SerializeField]
	TextMeshProUGUI name;

	[SerializeField]
	TextMeshProUGUI userName;

	[SerializeField]
	UnityEngine.UI.Button fightButton;

	public void Init(int pos, BattleRecord rec, Action<BattleRecord> onFight)
	{
		this.pos.text = pos.ToString();
		this.name.text = rec.Name;
		this.userName.text = rec.UserName;
		fightButton.onClick.AddListener(()=>onFight(rec));
	}
}


