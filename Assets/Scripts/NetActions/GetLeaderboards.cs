using System;
using Firebase.Database;
using UnityEngine;
using System.Collections.Generic;
using Data;

public class GetLeaderboards : BaseNetAction, IGetLeaderboard
{
	public void Do (Action<List<PlayerRecord>> lb)
	{
		var playerRef = FirebaseDatabase.DefaultInstance.GetReference("Players");

		playerRef.GetValueAsync().ContinueWith(task => {
			if (task.IsFaulted) {
				lb(null);
			}
			else if (task.IsCompleted) {
				DataSnapshot snapshot = task.Result;

				var lbList = new List<PlayerRecord>();
				foreach(var ch in snapshot.Children) {
					var rec = JsonUtility.FromJson<PlayerRecord>(ch.GetRawJsonValue());
					lbList.Add(rec);
				}
				lbList.Sort((x, y) => x.FinishedMission > y.FinishedMission ? -1 : 1);
				lb(lbList);
			}
		});
	}
}

