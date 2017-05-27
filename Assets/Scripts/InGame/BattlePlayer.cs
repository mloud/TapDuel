using Data;
using UnityEngine;

public class BattlePlayer : MonoBehaviour
{
	BattleRecord recs;
	Defs.Side side;
	IGameManager gameManager;

	public void Set(BattleRecord recs, IGameManager gameManager, Defs.Side side)
	{
		this.recs = recs;
		this.gameManager = gameManager;
		this.side = side;
	}

	public void Set(IGameManager gameManager, Defs.Side side)
	{
		this.gameManager = gameManager;
		this.side = side;
	}


	void Update()
	{
		if (recs != null && gameManager.IsRunning) {
			for (int i = 0; i < recs.Events.Count; ++i) {
				var ev = recs.Events[i];
				if (gameManager.BattleTime > ev.Time) {
					PerformEvent(ev);
					recs.Events.RemoveAt(i);
					break;
				}
			}			
		}
	}

	public void PerformEvent(BattleEvent ev)
	{
		if (ev.Type == BattleEventType.OkTap) {
			gameManager.PerformRightTouch(side);
		}else if (ev.Type == BattleEventType.WrongTap) {
			gameManager.PerformWrongTouch(side);
		} else if (ev.Type == BattleEventType.Shuffle) {
			gameManager.SwapMovers(GameUtils.GetOppositeSide(side));			
		} else if (ev.Type == BattleEventType.NumbersLost) {
			gameManager.LooseBonusRow(side);	
		} else if (ev.Type == BattleEventType.NumberTapped) {
			
		} else if (ev.Type == BattleEventType.NumbersFired) {
			
		}
	}
}

