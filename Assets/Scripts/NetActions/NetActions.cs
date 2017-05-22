using System;
using Data;
using System.Collections.Generic;


public class NetActions : INetActions
{
	public void CreateUser (string id, Data.PlayerRecord rec)
	{
		new CreateUser().Do(id, rec);
	}

	public void UpdateUser ()
	{
		new UpdateUser().Do();
	}

	public void GetLeaderboards(Action<List<PlayerRecord>> lb)
	{
		new GetLeaderboards().Do(lb);
	}

	public void GetBeatMe(Action<List<BattleRecord>> recs)
	{
		new GetBeatMe().Do(recs);
	}

	public void GetUser(string id, Action<PlayerRecord> result)
	{
		new GetUser().Do(id, result);
	}

	public void CreateBeatMe(BattleRecord rec)
	{
		new CreateBeatMeBattle().Do(rec);
	}
}


