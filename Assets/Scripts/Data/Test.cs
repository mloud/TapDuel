#if UNITY_EDITOR
using System;
using Data;
using System.Collections.Generic;
using UnityEditor;


public static class Test
{
	[MenuItem("Tools/Generate World Json")]
	public static void Run ()
	{
		var w = new World();
		w.AiSettings = new AiSettings();
		w.AiSettingsDict = new List<AiSettings>();
		w.AiSettingsDict.Add(new AiSettings());
		w.AiSettingsDict.Add(new AiSettings());

		w.Levels = new List<Level>();
		w.Levels.Add(new Level{AiName = "0"});
		w.Levels.Add(new Level{AiName = "1"});

		w.BattleConfig = new BattleConfig();
		w.BattleConfig.NumberCoef = new List<float>{1, 2,3};

		var str = UnityEngine.JsonUtility.ToJson(w, true);
		UnityEngine.Debug.Log(str);
	}
}
#endif

