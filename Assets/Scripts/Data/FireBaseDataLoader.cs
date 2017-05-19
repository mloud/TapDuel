using Firebase;
using Firebase.Unity.Editor;
using Firebase.Database;
using UnityEngine;
using System;


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

		public void SetErrorOutput(Action<string> output)
		{
			errorOuput = output;
		}
	}
}

