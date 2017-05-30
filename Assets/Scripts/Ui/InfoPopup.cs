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
	TextMeshProUGUI buttonText;


	[SerializeField]
	UnityEngine.UI.Button button;

	public void Init(string title, string message, Action action, string buttonName = null)
	{
		this.text.text = message;
		this.title.text = title;
		if (!string.IsNullOrEmpty(buttonName)) {
			buttonText.text = buttonName;
		}

		button.onClick.AddListener(()=> {
			if (action != null)
				action();
			Destroy(gameObject);
		});
	}
}


