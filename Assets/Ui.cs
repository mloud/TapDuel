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
	TextMeshPro userText;

	[SerializeField]
	GameObject centerLine;

	[SerializeField]
	TextMeshPro leftYourSide;

	[SerializeField]
	TextMeshPro rightYourSide;


	void Awake()
	{
		Refresh();
	}


	public void SetToBattleGameMode()
	{
		centerLine.SetActive(true);
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
		//progressTxt.text = string.Format("{0}/{1}", actualMission, totalMission);
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
				timer.transform.parent.gameObject.SetActive(true);
				timer.transform.parent.gameObject.GetComponent<Animator>().SetTrigger("out");
			} else {
				timer.transform.parent.gameObject.GetComponent<Animator>().SetTrigger("in");
			}
		} else {
			timer.transform.parent.gameObject.SetActive(active);
		}
	}

	public void ShowYourSide(Defs.Side side, bool visible)
	{
		if (side == Defs.Side.Left) {
			rightYourSide.gameObject.SetActive(false);
			leftYourSide.gameObject.SetActive(visible);
		} else {
			leftYourSide.gameObject.SetActive(false);
			rightYourSide.gameObject.SetActive(visible);
		}
	}

	void SetPlayButtonActive(bool active)
	{
		beatMeButton.SetActive(active);
	}
}