using UnityEngine;
using System.Threading.Tasks;
using System;

namespace Inv
{
	public class FireBaseInviter : IInviter
	{
		public FireBaseInviter()
		{
			StartReceiving();
		}

		public void SendTryGameInvitation ()
		{
			Debug.Log("Sending an invitation...");

			var invite = new Firebase.Invites.Invite() {
				TitleText = "Invite to Tap Duel",
				MessageText = "Try Tap Duel game! It's awesome.",
				CallToActionText = "Download it for FREE",
				DeepLinkUrl = new Uri("https://play.google.com/apps/testing/com.mloud.tapper")
			};
			Firebase.Invites.FirebaseInvites
				.SendInviteAsync(invite).ContinueWith(OnTryGameInvitation);
		}

		void StartReceiving() 
		{
			Firebase.Invites.FirebaseInvites.InviteReceived += OnInviteReceived;
			Firebase.Invites.FirebaseInvites.InviteNotReceived += OnInviteNotReceived;
			Firebase.Invites.FirebaseInvites.ErrorReceived += OnErrorReceived;
		}

		void OnInviteReceived(object sender,
			Firebase.Invites.InviteReceivedEventArgs e) {
			if (e.InvitationId != "") {
				Debug.Log("Invite received: Invitation ID: " + e.InvitationId);
				//Firebase.Invites.FirebaseInvites.ConvertInvitationAsync(
				//	e.InvitationId).ContinueWith();
			}
			if (e.DeepLink.ToString() != "") {
				Debug.Log("Invite received: Deep Link: " + e.DeepLink);
			}
		}

		void OnInviteNotReceived(object sender, System.EventArgs e) 
		{
			Debug.Log("No Invite or Deep Link received on start up");
		}

		void OnErrorReceived(object sender, Firebase.Invites.InviteErrorReceivedEventArgs e) 
		{
			Debug.LogError("Error occurred received the invite: " + e.ErrorMessage);
		}


		void OnTryGameInvitation(Task<Firebase.Invites.SendInviteResult> sendTask) 
		{
			if (sendTask.IsCanceled) {
				Debug.Log("Invitation canceled.");
			} else if (sendTask.IsFaulted) {
				Debug.Log("Invitation encountered an error:");
				Debug.Log(sendTask.Exception.ToString());
			} else if (sendTask.IsCompleted) {
				Debug.Log("SendInvite: invites sent successfully.");


				//int inviteCount = (new List(sendTask.Result.InvitationIds)).Count;
				//Debug.Log("SendInvite: " + inviteCount + " invites sent successfully.");
				//foreach (string id in sendTask.Result.InvitationIds) {
			//		Debug.Log("SendInvite: Invite code: " + id);
			//	}
			}
		}

	}
}

