using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Boot : MonoBehaviour
{

	void Start () 
	{
		StartCoroutine(Menu.Instance.SetIntroPanelActive(true));
		StartCoroutine(LoadData());
	}
	
	IEnumerator LoadData()
	{
		yield return StartCoroutine(Session.Instance.LoadGameData());
		SceneManager.LoadScene(1, LoadSceneMode.Additive);
		Menu.Instance.ShowInGameUi();
		yield return StartCoroutine(Menu.Instance.SetIntroPanelActive(false));

	}
}
