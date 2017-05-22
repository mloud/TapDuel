using System;
using Data;
using System.Collections.Generic;

public interface IGetLeaderboard
{
	void Do(Action<List<PlayerRecord>> lb);
}

