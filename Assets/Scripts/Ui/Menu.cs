﻿using UnityEngine;
using System.Collections;
using Vis;
using UnityEngine.UI;

public partial class Menu : MonoBehaviour
{
	[SerializeField]
	GameObject loadingCircle;

	[SerializeField]
	GameObject introPanel;

	[SerializeField]
	InGameMenu ingameMenu;

	[SerializeField]
	BattleMenu battleMenu;


	[SerializeField]
	Transform popupContainer;

    GameManager GameManager { get {
            if (gameManager == null)
                gameManager = FindObjectOfType<GameManager>();
            return gameManager;
        }}

    GameManager gameManager;
   


	void Awake()
	{
		DontDestroyOnLoad(gameObject);
	}

	public void Init()
	{
		ingameMenu.Init();
	}

	public void ShowLoadingCircle(bool show)
	{
		loadingCircle.SetActive(show);
	}

    public IEnumerator FadeToBlack()
    {
        var fadeGo = new GameObject("fade");
		fadeGo.AddComponent<RectTransform>();
        fadeGo.transform.SetParent(transform);
        fadeGo.transform.SetAsLastSibling();
		fadeGo.transform.localScale = Vector3.one;
        fadeGo.transform.localPosition = Vector3.zero;
        (fadeGo.transform as RectTransform).sizeDelta = new Vector2(Screen.width, Screen.height);
        var image = fadeGo.AddComponent<Image>();

        float startTime = Time.time;
        const float duration = 0.2f;
        while (true) {
            float c = Mathf.Min(1, (Time.time - startTime) / duration);
            image.color = new Color(0, 0, 0, c);
            if (c >= 1) {
                break;
            }
            yield return 0;
        } 
	}

	public IEnumerator FadeToWhite()
	{
        var fadeGo = transform.Find("fade");
        var image = fadeGo.GetComponent<Image>();

		float startTime = Time.time;
		const float duration = 0.2f;
		while (true)
		{
			float c = 1 - Mathf.Min(1, (Time.time - startTime) / duration);
			image.color = new Color(0, 0, 0, c);
            if (c < Mathf.Epsilon) {
				break;
			}
			yield return 0;
		}
		Destroy(fadeGo.gameObject);
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
   
	public void ShowInGameMenu()
	{
		ingameMenu.Init();
		ingameMenu.gameObject.SetActive(true);
		battleMenu.gameObject.SetActive(false);
	}

	public void HideInGameMenu()
	{
		ingameMenu.gameObject.SetActive(false);
		battleMenu.gameObject.SetActive(true);
	}

    void OnCreateBattle()
    {
		App.Instance.Menu.OpenCreateMatchPopup(GameManager.StartMultiplayerMatch, null);
	}

    void OnJoinBattle()
    {
        App.Instance.Menu.ShowLoadingCircle(true);
        GameManager.ListBattles((succ, info, matches) => {
            if (succ) {
                App.Instance.Menu.OpenMatchesPopup(matches);
            } else {
                App.Instance.Menu.OpenInfoPopup("Error", "ListMatches " + info, null);
            }
            App.Instance.Menu.ShowLoadingCircle(false);
        });
    }
}


