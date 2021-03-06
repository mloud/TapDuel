﻿using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using Data;

public class Boot : MonoBehaviour
{

	void Start () 
	{
		StartCoroutine(Menu.Instance.SetIntroPanelActive(true));
		StartCoroutine(StartBootSequence());
	}
	
	IEnumerator StartBootSequence()
	{
		#if UNITY_ANDROID && !UNITY_EDITOR
		yield return StartCoroutine(Session.Instance.AuthorizeUser());
		#endif
		//string userId = Session.Instance.User.Id();
		yield return StartCoroutine(Session.Instance.LoadGameData());

		bool profileFinished = false;
		Session.Instance.User.LoadProfile(()=>profileFinished = true);
		yield return new WaitUntil(()=>profileFinished);

		SceneManager.LoadScene(1, LoadSceneMode.Additive);
		yield return 0; // SceneManager.LoadScenehas delay 1 frame
		Menu.Instance.ShowInGameMenu();

		yield return StartCoroutine(Menu.Instance.SetIntroPanelActive(false));
	
	}
}
