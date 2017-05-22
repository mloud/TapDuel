using System;
using UnityEngine;
using Data;
using UnityEngine.AI;

namespace Usr
{
	public class FirebaseUser : IUser
	{
		public PlayerRecord Profile { get { return profile; } }

		Action<string> errorOutput;

		Firebase.Auth.FirebaseUser user;
		Firebase.Auth.FirebaseAuth auth;

		PlayerRecord profile;

		public FirebaseUser()
		{
			InitializeFirebase();
		}

		public bool IsAuthorized ()
		{
			return user != null ;
		}

		public void LoadProfile (Action finished)
		{
			Session.Instance.Actions.GetUser(Id(),(rec)=> {
				profile = rec;
				if (profile == null) {
					profile = new PlayerRecord() {
						Name = Session.Instance.User.Name(),
						Id = Id(),
						FinishedMission = -1
					};
					Session.Instance.Actions.CreateUser(Id(), profile);
				}
				finished();
			});
		}

		public string Id()
		{
			return user != null ? user.UserId : SystemInfo.deviceUniqueIdentifier;
		}

		public string Name()
		{
			return user != null ? user.DisplayName : "Unknown player";
		}

		public void SetName(string name, Action<bool> finished)
		{
			var profile = new Firebase.Auth.UserProfile();
			profile.DisplayName = name;
			user.UpdateUserProfileAsync(profile).ContinueWith(task => {
				if (task.IsCanceled) {
					if (errorOutput != null) {
						errorOutput("SignInAnonymouslyAsync was canceled.");
					}
					finished(false);
					return;
				}
				if (task.IsFaulted) {
					if (errorOutput != null) {
						errorOutput("SignInAnonymouslyAsync was canceled.");
					}
					finished(false);
					return;
				}
				finished(true);
				this.profile.Name = name;
				Session.Instance.Actions.UpdateUser();

				Debug.Log("User profile updated successfully.");
			});

		}

		public void Authorize (Action<bool> finished)
		{
			InitializeFirebase();
			if (user == null) {
				auth.SignInAnonymouslyAsync().ContinueWith(task => {
					if (task.IsCanceled) {
						if (errorOutput != null) {
							errorOutput("SignInAnonymouslyAsync was canceled.");
						}
						finished(false); 
						return;
					}
					if (task.IsFaulted) {
						if (errorOutput != null) {
							errorOutput("SignInAnonymouslyAsync encountered an error: " + task.Exception);
						}
						finished(false);
						return;
					}

					user = task.Result;
					finished(true);
					Debug.LogFormat("User signed in successfully: {0} ({1})",
						user.DisplayName, user.UserId);
				});
			}
			finished(true);
		}

		void InitializeFirebase() {
			auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
			auth.StateChanged += AuthStateChanged;
			AuthStateChanged(this, null);
		}

		void AuthStateChanged(object sender, System.EventArgs eventArgs) {
			if (auth.CurrentUser != user) {
				bool signedIn = user != auth.CurrentUser && auth.CurrentUser != null;
				if (!signedIn && user != null) {
					Debug.Log("Signed out " + user.UserId);
				}
				user = auth.CurrentUser;
				if (signedIn) {
					Debug.Log("Signed in " + user.UserId);
				}
			}
		}


		public void SetErrorOutput(Action<string> output)
		{
			errorOutput = output;
		}
	}
}

