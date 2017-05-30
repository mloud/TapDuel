using UnityEngine;
using System;
using System.Collections;
using Vis;
using System.Collections.Generic;
using Data;
using UnityEngine.Networking.Match;


public class Menu : MonoBehaviour
{
	[SerializeField]
	GameObject infoPopupPrefab;

	[SerializeField]
	GameObject infoPanelPrefab;

	[SerializeField]
	GameObject questionPopupPrefab;

	[SerializeField]
	GameObject inputNamePrefab;

	[SerializeField]
	GameObject lbPopupPrefab;

	[SerializeField]
	GameObject beatMePopupPrefab;

	[SerializeField]
	GameObject matchesPopupPrefab;

	[SerializeField]
	GameObject createMatchPopupPrefab;

	[SerializeField]
	GameObject loadingCircle;

	[SerializeField]
	GameObject introPanel;

	[SerializeField]
	InGameMenu ingameMenu;

	public static Menu Instance {
		get {return instance; }
	}
	static Menu instance;

	void Awake()
	{
		instance = this;
		DontDestroyOnLoad(gameObject);
	}

	public void ShowLoadingCircle(bool show)
	{
		loadingCircle.SetActive(show);
	}

	public void CloseAllPopups()
	{
		for (int i = 3; i < transform.childCount; ++i) {
			GameObject.Destroy(transform.GetChild(i).gameObject);
		}
		ShowLoadingCircle(false);
	}
	public IEnumerator SetIntroPanelActive(bool active)
	{
		if (!active) {
			float alpha = 1.0f;
			while (alpha > 0){
				Utils.SetAlpha(alpha, introPanel);
				alpha = Mathf.Max(0, alpha - 0.05f);
				yield return 0;
			}
		}
		introPanel.SetActive(active);
	}

	public void OpenInfoPanel(string title)
	{
		var popupGo = Instantiate(infoPanelPrefab);	
		popupGo.transform.SetParent(transform);
		popupGo.transform.localPosition = Vector3.zero;
		(popupGo.transform as RectTransform).offsetMax = Vector2.zero;
		(popupGo.transform as RectTransform).offsetMin = Vector2.zero;
		var panel = popupGo.GetComponent<InfoPanel>();
		popupGo.transform.SetAsLastSibling();
		panel.Open(title);
	}


	public void OpenInfoPopup(string title,string text, Action action)
	{
		var popupGo = Instantiate(infoPopupPrefab);	

		popupGo.transform.SetParent(transform);
		popupGo.transform.SetAsLastSibling();
		popupGo.transform.localPosition = Vector3.zero;
		var popup = popupGo.GetComponent<InfoPopup>();
		popup.Init(title, text, action);
	}

	public void OpenInputTextPopup(string title, Action<string> action)
	{
		var popupGo = Instantiate(inputNamePrefab);	

		popupGo.transform.SetParent(transform);
		popupGo.transform.SetAsLastSibling();
		popupGo.transform.localPosition = Vector3.zero;
		var popup = popupGo.GetComponent<InputFieldPopup>();
		popup.Init(title, action,null);
	}

	public void OpenCreateMatchPopup(Action<string> action, Action back)
	{
		var popupGo = Instantiate(createMatchPopupPrefab);	

		popupGo.transform.SetParent(transform);
		popupGo.transform.SetAsLastSibling();
		popupGo.transform.localPosition = Vector3.zero;
		var popup = popupGo.GetComponent<MatchMakingCreatePopup>();
		popup.Init(action,back);
	}

	public void ShowInGameMenu()
	{
		ingameMenu.Init();
		ingameMenu.gameObject.SetActive(true);
	}

	public void HideInGameMenu()
	{
		ingameMenu.gameObject.SetActive(false);
	}

	public void OpenQuestionPopup(string title,string text, Action actionYes, Action actionNo)
	{
		var popupGo = Instantiate(questionPopupPrefab);	

		popupGo.transform.SetParent(transform);
		popupGo.transform.SetAsLastSibling();
		popupGo.transform.localPosition = Vector3.zero;
		var popup = popupGo.GetComponent<QuestionPopup>();
		popup.Init(title, text, actionYes, actionNo);
	}

	public void OpenLBPopup(List<PlayerRecord> lb)
	{
		var popupGo = Instantiate(lbPopupPrefab);	

		popupGo.transform.SetParent(transform);
		popupGo.transform.SetAsLastSibling();
		popupGo.transform.localPosition = Vector3.zero;
		var popup = popupGo.GetComponent<LBPopup>();
		popup.Init(lb);
	}

	public void OpenBeatMePopup(List<BattleRecord> recs)
	{
		var popupGo = Instantiate(beatMePopupPrefab);	

		popupGo.transform.SetParent(transform);
		popupGo.transform.SetAsLastSibling();
		popupGo.transform.localPosition = Vector3.zero;
		var popup = popupGo.GetComponent<BeatMePopup>();
		popup.Init(recs);
	}

	public void OpenMatchesPopup(List<MatchInfoSnapshot> recs)
	{
		var popupGo = Instantiate(matchesPopupPrefab);	

		popupGo.transform.SetParent(transform);
		popupGo.transform.SetAsLastSibling();
		popupGo.transform.localPosition = Vector3.zero;
		var popup = popupGo.GetComponent<MatchesPopup>();
		popup.Init(recs);
	}


	void OnShareButton()
	{
		App.InviteToGame();
	}

}


