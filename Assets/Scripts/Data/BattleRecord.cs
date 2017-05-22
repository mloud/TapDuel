using System.Collections.Generic;
using UnityEngine;
using System;

namespace Data
{
	[Serializable]
	public class BattleRecord 
	{
		public string Id;
		public string UserId;
		public string UserName;
		public string Name;
		public List<BattleEvent> Events;
	}
}


