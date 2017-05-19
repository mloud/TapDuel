using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class App
{
	public static void Restart()
	{
		SceneManager.LoadScene(1);
		Menu.Instance.ShowInGameUi();
	}
}

