using System;

public static class GameUtils
{
	public static Defs.Side GetOppositeSide(Defs.Side side)
	{
		return side == Defs.Side.Left ? Defs.Side.Right : Defs.Side.Left;
	}
}

