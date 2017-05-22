using UnityEngine;
using System.Collections.Generic;
using Data;

public class BeatMePopup : MonoBehaviour
{
	[SerializeField]
	GameObject beatMePrefab;

	[SerializeField]
	Transform container;


	[SerializeField]
	UnityEngine.UI.Button button;

	public void Init(List<BattleRecord> battleRecords)
	{
		button.onClick.AddListener(()=> {
			Destroy(gameObject);
		});

		Fill(battleRecords);
	}

	void Fill(List<BattleRecord> battleRecords)
	{
		for (int i = 0; i < battleRecords.Count; ++i) {
			var item = Instantiate(beatMePrefab).GetComponent<BeatMeItem>();
			var rec = battleRecords[i];
			item.Init(i + 1, rec, OnFight);
			item.transform.SetParent(container);
		}
	}

	void OnFight(BattleRecord rec)
	{
		var gameManager = GameObject.FindObjectOfType<GameManager>();
		gameManager.BattleRec = rec;
		gameManager.NewBeatMePlay();
		Destroy(gameObject);
	}
}


