using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using Inv;

public class App
{
	static IInviter inviter = new FireBaseInviter();

	public static void Restart()
	{
		SceneManager.LoadScene(1);
		Menu.Instance.ShowInGameMenu();
	}

	public static void InviteToGame()
	{
		inviter.SendTryGameInvitation();
	}
}

