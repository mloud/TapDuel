using UnityEngine;
using TMPro;
using System.Collections;

public class InfoPanel : MonoBehaviour
{
	[SerializeField]
	TextMeshProUGUI text;
		
	[SerializeField]
	float delay;

	Animator anim;

	void Awake()
	{
		anim = GetComponent<Animator>();
	}

	public void Open(string message)
	{
		StartCoroutine(OpenInternal(message, delay));
	}

	IEnumerator OpenInternal(string message, float delay)
	{
		text.text = message;
		//anim.SetTrigger("open");

		yield return new WaitForSeconds(delay);

		anim.SetTrigger("close");
	}

	public void CloseFinished()
	{
		Destroy(gameObject);
	}
}
