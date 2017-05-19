using UnityEngine;
using TMPro;


public class Ui : MonoBehaviour
{
	public BottomPopup BottomPopup;
	public HPBar bar;

	[SerializeField]
	TextMeshPro timer;

	[SerializeField]
	TextMeshPro wonMessage;

	[SerializeField]
	SpriteRenderer background;

	[SerializeField]
	GameObject playButton;

	[SerializeField]
	GameObject playAiButton;


	public void SetTimer(float time)
	{
		timer.text = ((int)time).ToString();
	}

	public void ShowMessage(string message, bool showBg = false)
	{
		wonMessage.gameObject.SetActive(true);
		wonMessage.text = message;
		background.enabled = showBg;
	}

	public void HideMessage()
	{
		wonMessage.gameObject.SetActive(false);
		background.enabled = false;
	}

	public void SetTimerActive(bool active, bool useAnim = true)
	{
		if (useAnim) {
			if (active) {
				timer.gameObject.SetActive(true);
				timer.gameObject.GetComponent<Animator>().SetTrigger("out");
			} else {
				timer.gameObject.GetComponent<Animator>().SetTrigger("in");
			}
		} else {
			timer.gameObject.SetActive(active);
		}
	}

	public void SetPlayButtonActive(bool active)
	{
		playButton.gameObject.SetActive(active);
		playAiButton.gameObject.SetActive(active);
	}

}