using UnityEngine;
using System;
using Data;

public class BeatMeComponent : MonoBehaviour
{
	IGameManager gameManager;
	BattlePlayer automaticBattlePlayer;

	void Awake()
	{
		gameManager = Array.Find(GetComponents<MonoBehaviour>(),x => x is IGameManager) as IGameManager;
	}

	public void Set(BattleRecord rec)
	{
		automaticBattlePlayer = gameObject.AddComponent<BattlePlayer>();
		automaticBattlePlayer.Set(rec, gameManager, Defs.Side.Left);
	}
}

