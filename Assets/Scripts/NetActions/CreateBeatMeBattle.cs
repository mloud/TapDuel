using Firebase.Database;
using Data;
using UnityEngine;

public class CreateBeatMeBattle : BaseNetAction, ICreateBeatMeBattle
{
	public void Do(BattleRecord rec)
	{
		var beatMeRef = FirebaseDatabase.DefaultInstance.GetReference("BeatMe");
		string json = JsonUtility.ToJson(rec);
		beatMeRef.Child(rec.Id).SetRawJsonValueAsync(json);
	}
}