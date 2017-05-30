using UnityEngine;
using System.Collections.Generic;
using Data;
using UnityEngine.Networking.Match;

public class MatchesPopup : MonoBehaviour
{
	[SerializeField]
	GameObject battlePrefab;

	[SerializeField]
	Transform container;


	[SerializeField]
	UnityEngine.UI.Button button;

	public void Init(List<MatchInfoSnapshot> matches)
	{
		button.onClick.AddListener(()=> {
			Destroy(gameObject);
		});

		Fill(matches);
	}

	void Fill(List<MatchInfoSnapshot> matches)
	{
		for (int i = 0; i < matches.Count; ++i) {
			var item = Instantiate(battlePrefab).GetComponent<MatchItem>();
			var rec = matches[i];
			item.Init(i + 1, rec, OnFight);
			item.transform.SetParent(container);
		}
	}

	void OnFight(MatchInfoSnapshot rec)
	{
		var gameManager = GameObject.FindObjectOfType<GameManager>();
		gameManager.JoinBattle(rec);
		Destroy(gameObject);
	}
}


