using Data;
using System;
using System.Collections.Generic;


public interface IGetBeatMe
{
	void Do (Action<List<BattleRecord>> recs);
}



