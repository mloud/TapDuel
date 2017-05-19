using System;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class InfoPopup : MonoBehaviour
{
	[SerializeField]
	TextMeshProUGUI title;

	[SerializeField]
	TextMeshProUGUI text;


	[SerializeField]
	UnityEngine.UI.Button button;

	public void Init(string title, string message, Action action)
	{
		this.text.text = message;
		this.title.text = title;
		button.onClick.AddListener(()=> {
			if (action != null)
				action();
			Destroy(gameObject);
		});
	}
}


