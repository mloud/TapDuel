using Data;
using Firebase.Database;
using UnityEngine;


public class GetUser : BaseNetAction, IGetUser
{
	public void Do (string id, System.Action<PlayerRecord> finish)
	{
		var playerRef = FirebaseDatabase.DefaultInstance.GetReference("Players/" + id);
		playerRef.GetValueAsync().ContinueWith(task => {
			if (task.IsFaulted) {
				finish(null);
			}
			else if (task.IsCompleted) {
				DataSnapshot snapshot = task.Result;
				var rec = JsonUtility.FromJson<PlayerRecord>(snapshot.GetRawJsonValue());
				finish(rec);
			}
		});
	}
}

