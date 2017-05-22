using System.Collections.Generic;
using Data;
using UnityEngine;
using System.Linq;


public class BattlePlayer
{
	BattleRecord recs;
	bool running;
	Defs.Side side;
	GameManager gameManager;

	public BattlePlayer(BattleRecord recs, GameManager gameManager, Defs.Side side)
	{
		this.recs = recs;
		this.gameManager = gameManager;
		this.side = side;
	}

	public void Start()
	{
		running = true;
	}


	public void Update(float battleTime)
	{
		if (running) {
			for (int i = 0; i < recs.Events.Count; ++i) {
				var ev = recs.Events[i];
				if (battleTime > ev.Time) {
					PerformEvent(ev);
					recs.Events.RemoveAt(i);
					break;
				}
			}			
		}
	}

	void PerformEvent(BattleEvent ev)
	{
		if (ev.Type == BattleEventType.OkTap) {
			var movers = gameManager.GetMovers(side);
			var prefab = gameManager.GetActualMoverShape(side);

			var okMover = movers.Find(x=>x.type == prefab.type);
			gameManager.OnTouch(okMover.transform.position, side);
		}else if (ev.Type == BattleEventType.WrongTap) {
			var movers = gameManager.GetMovers(side);
			var prefab = gameManager.GetActualMoverShape(side);

			var wrongMover = movers.Find(x=>x.type != prefab.type);
			gameManager.OnTouch(wrongMover.transform.position, side);
		} else if (ev.Type == BattleEventType.Shuffle) {
			gameManager.SwapMovers(gameManager.GetOppositeSide(side));			
		} else if (ev.Type == BattleEventType.NumbersLost) {
			gameManager.LooseBonusRow(side);	
		} else if (ev.Type == BattleEventType.NumberTapped) {
			var numbers = GameObject.FindObjectsOfType<BonusNumber>().ToList();
			numbers.RemoveAll(x=>x.Side != side);
			if (numbers.Count > 0) {
				numbers[0].Collect();
			}
		} else if (ev.Type == BattleEventType.NumbersFired) {
			
		}
	}
}

