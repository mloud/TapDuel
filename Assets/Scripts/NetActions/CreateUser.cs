using Data;
using Firebase.Database;
using UnityEngine;

public class CreateUser : BaseNetAction, ICreateUser
{
	public void Do(string id, PlayerRecord rec)
	{
		var playerRef = FirebaseDatabase.DefaultInstance.GetReference("Players");
		string json = JsonUtility.ToJson(rec);
		playerRef.Child(id).SetRawJsonValueAsync(json);
	}
}

