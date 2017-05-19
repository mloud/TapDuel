using UnityEngine;
using System;
using System.Collections;
using Vis;


public class Menu : MonoBehaviour
{
	[SerializeField]
	GameObject infoPopupPrefab;

	[SerializeField]
	GameObject questionPopupPrefab;

	[SerializeField]
	GameObject ingameUiPrefab;


	[SerializeField]
	GameObject introPanel;

	public static Menu Instance {
		get {return instance; }
	}
	static Menu instance;

	void Awake()
	{
		instance = this;
		DontDestroyOnLoad(gameObject);
	}

	public IEnumerator SetIntroPanelActive(bool active)
	{
		if (!active) {
			float alpha = 1.0f;
			while (alpha > 0){
				Vis.Utils.SetAlpha(alpha, introPanel);
				alpha = Mathf.Max(0, alpha - 0.05f);
				yield return 0;
			}
		}
		introPanel.SetActive(active);
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

	public void ShowInGameUi()
	{
		var ingameUi = Instantiate(ingameUiPrefab);	

		ingameUi.transform.SetParent(transform);
		ingameUi.transform.SetAsFirstSibling();
		ingameUi.transform.localPosition = Vector3.zero;
		Utils.Makefullscreen(ingameUi.transform);
		//var ui = ingameUi.GetComponent<InGameMenu>();
	}

	public void HideIngameUi()
	{
		var ingameUi = gameObject.GetComponentInChildren<InGameMenuOverlay>();
		Destroy(ingameUi.gameObject);
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
}


