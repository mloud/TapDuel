using System;
using System.Collections.Generic;
using Data;


public interface INetActions
{
	void CreateUser (string id, PlayerRecord rec);
	void UpdateUser ();
	void GetLeaderboards(Action<List<PlayerRecord>> lb);
	void GetBeatMe(Action<List<BattleRecord>> recs);
	void GetUser(string id, Action<PlayerRecord> result);
	void CreateBeatMe(BattleRecord rec);
}


