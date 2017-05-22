using Firebase;
using Firebase.Unity.Editor;
using Firebase.Database;
using UnityEngine;
using System;
using System.Collections.Generic;


namespace Data
{
	public class FireBaseDataLoader : IGameDataLoader
	{
		const string DbUrl = "https://tap-duel.firebaseio.com/";

		Action<string> errorOuput;

		public void Init()
		{
			FirebaseApp.DefaultInstance.SetEditorDatabaseUrl(DbUrl);
		}

		public void GetWorld(Action<World> result)
		{
			FirebaseDatabase.DefaultInstance
				.GetReference("World")
				.GetValueAsync().ContinueWith(task => {
					if (task.IsFaulted) {
						result(null);
						if (errorOuput != null) {
							errorOuput(task.Exception.ToString());
						}
					}
					else if (task.IsCompleted) {
						DataSnapshot snapshot = task.Result;
						var json = snapshot.GetRawJsonValue();

						if (json == null) {
							errorOuput("GetWorld JSON is null");
						}
						var battleConfig = JsonUtility.FromJson<World>(json);
						result(battleConfig);
					}
			});
		}

		public void GetPlayers(Action<Players> result)
		{
			FirebaseDatabase.DefaultInstance
				.GetReference("Players")
				.GetValueAsync().ContinueWith(task => {
					if (task.IsFaulted) {
						result(null);
						if (errorOuput != null) {
							errorOuput(task.Exception.ToString());
						}
					}
					else if (task.IsCompleted) {
						DataSnapshot snapshot = task.Result;
						var json = snapshot.GetRawJsonValue();

						if (json == null) {
							errorOuput("GetWorld JSON is null");
						}
						var players = JsonUtility.FromJson<Players>(json);
						players.PlayerList.Sort((a, b) => {
							return a.FinishedMission > b.FinishedMission ? -1 : 1;
						});
						result(players);
					}
				});
		}
//
//		public void AddPlayerRecord(PlayerRecord rec)
//		{
//
//			FirebaseDatabase.DefaultInstance
//				.GetReference("Players").SetRawJsonValueAsync()
//
//			DatabaseReference usersRef = ref.child("users");
//
//			Map<String, User> users = new HashMap<String, User>();
//			users.put("alanisawesome", new User("June 23, 1912", "Alan Turing"));
//			users.put("gracehop", new User("December 9, 1906", "Grace Hopper"));
//
//			usersRef.setValue(users);
//		}

		public void SetErrorOutput(Action<string> output)
		{
			errorOuput = output;
		}
	}
}

