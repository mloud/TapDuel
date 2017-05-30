using UnityEngine;
using System.Collections.Generic;

public partial class GameManager
{
	float GetLeftSidePosition()
	{
		return visual.GetLeftWinLinePosition();
	}

	float GetRightSidePosition()
	{
		return visual.GetRightWinLinePosition();
	}

	public Mover GetActualMoverShape(Defs.Side side)
	{
		return side == Defs.Side.Left ? leftMoverPrefab : rightMoverPrefab;
	}

	BattleRecorder GetBattleRecorder(Defs.Side side)
	{
		return side == Defs.Side.Left ? leftBattleRecorder : rightBattleRecorder;
	}

	BonusGenerator GetBonusGenerator(Defs.Side side)
	{
		return side == Defs.Side.Left ? leftBonusGen : rightBonusGen;
	}

	public List<Mover> GetMovers(Defs.Side side) 
	{
		return side == Defs.Side.Left ? leftMovers : rightMovers;
	}

	Transform GetBonusContainer(Defs.Side side) 
	{
		return side == Defs.Side.Left ? leftBonusContainer : rightBonusContainer;
	}

	Transform GetShapeContainer(Defs.Side side) 
	{
		return side == Defs.Side.Left ? leftContainer : rightContainer;
	}

	float GetHPCoef()
	{
		float rightPos = visual.GetRightWinLinePosition();
		float leftPos = visual.GetLeftWinLinePosition();

		float max = Mathf.Abs(rightPos - leftPos);
		float curr = visual.GetLinePosition() - leftPos;
		return curr / max;
	}

}

