using System;
using TMPro;
using UnityEngine;

public class InputFieldPopup : MonoBehaviour
{
	[SerializeField]
	TextMeshProUGUI title;

	[SerializeField]
	TMP_InputField inputField;


	[SerializeField]
	UnityEngine.UI.Button okBtn;

	[SerializeField]
	UnityEngine.UI.Button cancelBtn;


	public void Init(string title, Action<string> actionOk, Action actionCancel)
	{
		this.title.text = title;
		okBtn.onClick.AddListener(()=> {
			if (actionOk != null)
				actionOk(inputField.text);
			Destroy(gameObject);
		});
		cancelBtn.onClick.AddListener(()=> {
			if (actionCancel != null)
				actionCancel();
			Destroy(gameObject);
		});
	}
}


