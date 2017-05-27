using System;
using UnityEngine;
using System.Collections;
using Txt;

public class ExitChecker : MonoBehaviour
{
	[SerializeField]
	Ui ui;


	bool keyPressCheckRunning;
	#if UNITY_EDITOR
	const KeyCode exitCode = KeyCode.Space;
	#else
	const KeyCode exitCode = KeyCode.Escape;
	#endif

	IGameManager gameManager;

	void Awake()
	{
		gameManager = Array.Find(GetComponents<MonoBehaviour>(),x => x is IGameManager) as IGameManager;
	}


	IEnumerator CheckForKeyPress(KeyCode code, float time, Action action) 
	{	
		keyPressCheckRunning = true;
		float startTime = Time.time;
		yield return 0;
		while ( (Time.time - startTime) < time) {
			if (Input.GetKeyDown(code)) {
				action();
			}
			yield return 0;
		}
		keyPressCheckRunning = false;
	}

	void Update()
	{
		UpdateKeyPressChecks();
	}

	void UpdateKeyPressChecks ()
	{
		if (!keyPressCheckRunning && Input.GetKeyDown (exitCode)) {
			var text = TextManager.Instance.Get (gameManager.IsRunning ? TextConts.STR_PRESS_AGAIN_LEAVE : TextConts.STR_PRESS_AGAIN_EXIT);
			ui.BottomPopup.Open(text, 2.0f);
			StartCoroutine (CheckForKeyPress (exitCode, 2.0f, () =>  {
				if (gameManager.IsRunning) {
					App.Restart ();
				}
				else {
					Application.Quit ();
				}
			}));
		}
	}
}

