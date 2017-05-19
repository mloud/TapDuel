using System;
using System.Collections.Generic;
using UnityEngine;


namespace Data
{
	[Serializable]
	public class World
	{
		public BattleConfig BattleConfig;
		public AiSettings AiSettings;
		public List<AiSettings> AiSettingsDict;
		public List<Level> Levels;
	}

	[Serializable]
	public class Level
	{
		public string AiName;
	}

	[Serializable]
	public class BattleConfig
	{
		public float BattleDuration = 60;
		public float ShuffleInterval = 5;
		public float NumberInterval = 3;
		public float PositiveMove = 0.5f;
		public float NegativeMove = 0.5f;
		public float BonusFallingSpeed = -10.0f;
		public List<float> NumberCoef;
	}

	[Serializable]
	public class AiSettings 
	{
		public string Name;
		public float MinReactionTime = 0.5f;
		public float MaxReactionTime = 1.0f;
		public float ErrorRate = 0.2f;
	}
}

