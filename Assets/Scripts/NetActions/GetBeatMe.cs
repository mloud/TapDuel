using System;
using Firebase.Database;
using UnityEngine;
using System.Collections.Generic;
using Data;

public class GetBeatMe : BaseNetAction, IGetBeatMe
{
	public void Do (Action<List<BattleRecord>> recs)
	{
		var playerRef = FirebaseDatabase.DefaultInstance.GetReference("BeatMe");

		playerRef.GetValueAsync().ContinueWith(task => {
			if (task.IsFaulted) {
				recs(null);
			}
			else if (task.IsCompleted) {
				DataSnapshot snapshot = task.Result;

				var list = new List<BattleRecord>();
				foreach(var ch in snapshot.Children) {
					var rec = JsonUtility.FromJson<BattleRecord>(ch.GetRawJsonValue());
					list.Add(rec);
				}
			
				recs(list);
			}
		});
	}
}

