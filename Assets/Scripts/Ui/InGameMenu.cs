using System;
using UnityEngine;

public class InGameMenu : MonoBehaviour
{
	[SerializeField]
	UnityEngine.UI.Button duelButton;

	[SerializeField]
	UnityEngine.UI.Button soloButton;

	[SerializeField]
	UnityEngine.UI.Button pvpButton;

	[SerializeField]
	UnityEngine.UI.Button lbButton;

	[SerializeField]
	UnityEngine.UI.Button shareButton;

	[SerializeField]
	UnityEngine.UI.Button createPVPButton;

	[SerializeField]
	UnityEngine.UI.Button joinPVPButton;

	[SerializeField]
	UnityEngine.UI.Button resetButton;


	GameManager gameManager;

	public void Init()
	{
		gameManager = GameObject.FindObjectOfType<GameManager>();

		duelButton.onClick.RemoveAllListeners();
		soloButton.onClick.RemoveAllListeners();
		createPVPButton.onClick.RemoveAllListeners();
		joinPVPButton.onClick.RemoveAllListeners();
		lbButton.onClick.RemoveAllListeners();
		resetButton.onClick.RemoveAllListeners();
		shareButton.onClick.RemoveAllListeners();

		duelButton.onClick.AddListener(()=>GetGameManager().NewGame());
		soloButton.onClick.AddListener(()=>GetGameManager().NewAiGame());
		createPVPButton.onClick.AddListener(()=>GetGameManager().OnCreateMatch());
		joinPVPButton.onClick.AddListener(()=>GetGameManager().OnListBattles());
		resetButton.onClick.AddListener(()=>GetGameManager().OnReset());
		lbButton.onClick.AddListener(()=>GetGameManager().OnLbs());
		shareButton.onClick.AddListener(()=>GetGameManager().OnShare());

	}

	GameManager GetGameManager()
	{
		return GameObject.FindObjectOfType<GameManager>();
	}
}

