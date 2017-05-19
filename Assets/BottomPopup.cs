using UnityEngine;
using TMPro;

public class BottomPopup : MonoBehaviour 
{
	[SerializeField]
	TextMeshPro text;

	Animator animator;

	void Awake()
	{
		animator = GetComponent<Animator>();
	}


	public void Open(string message, float autoHideTime = -1)
	{
		animator.SetTrigger("open");
		text.text = message;

		if (autoHideTime > 0) {
			Invoke("Close", autoHideTime);
		}
	}

	public void Close()
	{
		animator.SetTrigger("close");
	}
}
