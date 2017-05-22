using Firebase.Database;
using System.Collections.Generic;
using System;


public class UpdateUser : BaseNetAction, IUpdateUser
{
	public void Do()
	{
		var playerRef = FirebaseDatabase.DefaultInstance.GetReference("Players");
		var entryValues =  Session.Instance.User.Profile.ToDictionary();
		var childUpdates = new Dictionary<string, Object>();
		childUpdates[Session.Instance.User.Id()] = entryValues;
		playerRef.UpdateChildrenAsync(childUpdates);
	}
}

