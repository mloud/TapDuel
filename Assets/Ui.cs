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

	[SerializeField]
	GameObject resetButton;

	[SerializeField]
	GameObject connectButton;

	[SerializeField]
	GameObject lbButton;

	[SerializeField]
	GameObject beatMeListButton;

	[SerializeField]
	GameObject beatMeButton;


	[SerializeField]
	TextMeshPro progressTxt;

	[SerializeField]
	TextMeshPro gameTitle;

	[SerializeField]
	TextMeshPro userText;

	[SerializeField]
	GameObject centerLine;

	void Awake()
	{
		Refresh();
	}


	public void SetToBattleGameMode()
	{
		SetPlayButtonActive(false);
		resetButton.SetActive(false);
		connectButton.SetActive(false);
		lbButton.SetActive(false);
		beatMeListButton.SetActive(false);
		centerLine.SetActive(true);
		gameTitle.gameObject.SetActive(false);
	}

	public void Refresh()
	{
		int lastFinishedMission = ProgressManager.Instance.GetLastFinishedMission();
		int totalMissionCount = Session.Instance.Data.Utils.GetMissionsCount();
		SetProgress(lastFinishedMission + 1, totalMissionCount);
	
		SetUserDetail ();
	}

	void SetProgress(int actualMission, int totalMission)
	{
		progressTxt.text = string.Format("{0}/{1}", actualMission, totalMission);
	}


	public void SetTimer(float time)
	{
		timer.text = ((int)time).ToString();
	}

	void SetUserDetail ()
	{
		var userName = Session.Instance.User.Name ();
		var userId = Session.Instance.User.Id ();
		userText.text = !string.IsNullOrEmpty (userName) ? userName : userId;
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

	void SetPlayButtonActive(bool active)
	{
		playButton.SetActive(active);
		playAiButton.SetActive(active);
		beatMeButton.SetActive(active);
	}
}