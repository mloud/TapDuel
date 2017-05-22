using UnityEngine;
using System.Collections.Generic;
using Data;

public class LBPopup : MonoBehaviour
{
	[SerializeField]
	GameObject lbPrefab;

	[SerializeField]
	Transform lbContainer;


	[SerializeField]
	UnityEngine.UI.Button button;

	public void Init(List<PlayerRecord> players)
	{
		button.onClick.AddListener(()=> {
			Destroy(gameObject);
		});

		FillLb(players);
	}

	void FillLb(List<PlayerRecord> players)
	{
		for (int i = 0; i < players.Count; ++i) {
			if (players[i].FinishedMission == -1)
				continue;
			
			var lb = Instantiate(lbPrefab).GetComponent<LBItem>();
			var playerId = string.IsNullOrEmpty(players[i].Name) ? players[i].Id : players[i].Name;
			lb.Init(i + 1, playerId, players[i].FinishedMission + 1);

			lb.transform.SetParent(lbContainer);
		}
	}
}


