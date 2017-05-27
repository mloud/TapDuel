using UnityEngine;
using System;
using System.Linq;

public class MultiplayerComponent : MonoBehaviour
{
	IGameManager gameManager;
	bool checkForMultiPlayers = true;
	float timer;
	void Awake()
	{
		gameManager = Array.Find(GetComponents<MonoBehaviour>(),x => x is IGameManager) as IGameManager;
		gameManager.RightTouchAction += RightTouchAction;
		gameManager.WrongTouchAction += WrongTouchAction;
		gameManager.BonusCollectedAction += CollectBonus;
		gameManager.NumberBonusFiredAction += NumberBonusFired;
	}

	void RightTouchAction()
	{
		var player = GetMyPlayer();
		if (player != null)
			player.CmdRightClick();
	}

	void WrongTouchAction()
	{
		var player = GetMyPlayer();
		if (player != null)
			player.CmdWrongClick();
	}

	void CollectBonus(string name)
	{
		var player = GetMyPlayer();
		if (player != null)
			player.CmdBonusCollected(name);
	}

	void NumberBonusFired(float distance)
	{
		var player = GetMyPlayer();
		if (player != null)
			player.CmdNumberBonusFired(distance);
	}

	InputManager GetMyPlayer()
	{
		return gameManager.InputManagers.Find(x=>x.isLocalPlayer);
	}

	void Update()
	{
		UpdateCheckForMultiplayer();
	}

	void UpdateCheckForMultiplayer()
	{
		if (Time.time > timer && checkForMultiPlayers) {
			gameManager.InputManagers = FindObjectsOfType<InputManager>().ToList();
			if (gameManager.InputManagers.Count == 2 && gameManager.InputManagers.Any(x=>x.isLocalPlayer)) {
				gameManager.InputManagers[0].Side = gameManager.InputManagers[0].netId.GetHashCode() < gameManager.InputManagers[1].netId.GetHashCode() ? 
					Defs.Side.Left : Defs.Side.Right;

				gameManager.InputManagers[1].Side = gameManager.InputManagers[1].netId.GetHashCode() < gameManager.InputManagers[0].netId.GetHashCode() ? 
					Defs.Side.Left : Defs.Side.Right;

				checkForMultiPlayers = false;
				gameManager.NewMultiGame();
			}
			timer = Time.time + 1.0f;
		}
	}

}

