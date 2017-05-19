using System;


public class ProgressManager
{
	public static ProgressManager Instance {
		get { 
			if (instance == null)  {
				instance = new ProgressManager();
			}
			return instance;
		}
	}

	static ProgressManager instance;

	ISaver saver;

	public ProgressManager()
	{
		saver = SaveManager.Instance.Impl;
	}


	public int GetLastFinishedMission()
	{
		if (saver.HasKey("LastFinishedMission")) {
			return saver.GetInt("LastFinishedMission");
		}
		return -1;
	}

	public void SaveLastFinishedMission(int missionId)
	{
		saver.Save("LastFinishedMission", missionId);
	}

	public void ResetProgress()
	{
		saver.Save("LastFinishedMission", -1);
	}
}

