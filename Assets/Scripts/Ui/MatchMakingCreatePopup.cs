using System;
using TMPro;
using UnityEngine;

public class MatchMakingCreatePopup : MonoBehaviour
{
	[SerializeField]
	TMP_InputField inputField;

	[SerializeField]
	UnityEngine.UI.Button createBtn;

	[SerializeField]
	UnityEngine.UI.Button backBtn;


	public void Init(Action<string> actionCreate, Action actionBack)
	{
		createBtn.onClick.AddListener(()=> {
			if (actionCreate != null)
				actionCreate(inputField.text);
			Destroy(gameObject);
		});
		backBtn.onClick.AddListener(()=> {
			if (actionBack != null)
				actionBack();
			Destroy(gameObject);
		});
	}
}


