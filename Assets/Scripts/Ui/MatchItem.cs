using TMPro;
using UnityEngine;
using Data;
using System;
using UnityEngine.Networking.Match;

public class MatchItem : MonoBehaviour
{
	[SerializeField]
	TextMeshProUGUI pos;

	[SerializeField]
	TextMeshProUGUI name;

	[SerializeField]
	UnityEngine.UI.Button fightButton;

	public void Init(int pos, 
		MatchInfoSnapshot match, Action<MatchInfoSnapshot> onFight)
	{
		this.pos.text = pos.ToString();
		this.name.text = match.name;
		fightButton.onClick.AddListener(()=>onFight(match));
	}
}


