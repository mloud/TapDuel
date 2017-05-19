using System;
using Data;

public class DataUtils
{
	World world;

	public DataUtils(World world) 
	{
		this.world = world;
	}

	public AiSettings GetAiSettings(int levelIndex)
	{
		var aiName = world.Levels[levelIndex].AiName;
		return world.AiSettingsDict.Find(x=>x.Name == aiName);
	}

	public int GetMissionsCount()
	{
		return world.Levels.Count;
	}
}

