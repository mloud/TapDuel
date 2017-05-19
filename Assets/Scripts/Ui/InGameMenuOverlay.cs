using UnityEngine;
using Txt;
using TMPro;
using UnityEngine.SceneManagement;

public class InGameMenuOverlay : MonoBehaviour
{
	[SerializeField]
	UnityEngine.UI.Button resetBtn;

	[SerializeField]
	TextMeshProUGUI progressTxt;

	void Awake()
	{
		resetBtn.onClick.AddListener(OnReset);
		Refresh();
	}

	public void Refresh()
	{
		int lastFinishedMission = ProgressManager.Instance.GetLastFinishedMission();
		int totalMissionCount = Session.Instance.Data.Utils.GetMissionsCount();

		SetProgress(lastFinishedMission + 1, totalMissionCount);
	}

	void SetProgress(int actualMission, int totalMission)
	{
		progressTxt.text = string.Format("{0}/{1}", actualMission, totalMission);
	}

	void OnReset()
	{
		Menu.Instance.OpenQuestionPopup ("", TextManager.Instance.Get (TextConts.STR_RESET_THE_GAME), 
			// yes
			()=> {
				ProgressManager.Instance.ResetProgress();
				App.Restart();
			},
			//no
			() => {}
		);
	}
}

