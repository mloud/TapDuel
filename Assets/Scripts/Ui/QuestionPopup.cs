using System;
using TMPro;
using UnityEngine;

public class QuestionPopup : MonoBehaviour
{
	[SerializeField]
	TextMeshProUGUI title;

	[SerializeField]
	TextMeshProUGUI text;


	[SerializeField]
	UnityEngine.UI.Button yesBtn;

	[SerializeField]
	UnityEngine.UI.Button noBtn;


	public void Init(string title, string message, Action actionYes, Action actionNo)
	{
		this.text.text = message;
		this.title.text = title;
		yesBtn.onClick.AddListener(()=> {
			if (actionYes != null)
				actionYes();
			Destroy(gameObject);
		});
		noBtn.onClick.AddListener(()=> {
			if (actionNo != null)
				actionNo();
			Destroy(gameObject);
		});
	}
}


