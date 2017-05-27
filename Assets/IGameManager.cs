using System.Collections.Generic;
using UnityEngine;
using System;


public interface IGameManager
{
	Action RightTouchAction { get; set; }
	Action WrongTouchAction { get; set; }
	Action<string> BonusCollectedAction { get; set; }
	Action<float> NumberBonusFiredAction { get; set; }


	bool IsRunning { get; }
	float BattleTime { get; }

	List<InputManager> InputManagers { get; set; }

	void PerformRightTouch(Defs.Side side);
	void PerformWrongTouch(Defs.Side side);
	void PerformCollectBonus(Defs.Side side, string name);
	void PerformNumberBonusFired(Defs.Side side, float move);
	void CollectBonus(BonusBase bonus, Defs.Side side);
	void SwapMovers(Defs.Side side);
	void LooseBonusRow(Defs.Side side);
	void OnTouch(Vector3 pos, Defs.Side side);

	void NewGame();
	void NewBeatMeGameRecording();
	void NewBeatMePlay();
	void NewMultiGame();
	void NewAiGame();
}

