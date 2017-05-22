using System.Collections.Generic;
using Data;


public class BattleRecorder
{
	public List<BattleEvent> Events;

	public BattleRecorder()
	{
		Events = new List<BattleEvent>();
	}

	public BattleRecorder(List<BattleEvent> events)
	{
		Events = events;
	}

	public void AddEvent(BattleEventType type, float time)
	{
		Events.Add(new BattleEvent {
			Time = time,
			Type = type
		});
	}

	public override string ToString ()
	{
		string str = "";
		for(int i = 0; i < Events.Count; ++i) {
			str += string.Format("{0}-{1}", Events[i].Time,Events[i].Type);
		}
		return str.GetHashCode().ToString();
	}

	public void Log()
	{
		for(int i = 0; i < Events.Count; ++i) {
			UnityEngine.Debug.Log(string.Format("{0}-{1}", Events[i].Time,Events[i].Type));
		}
	}
}

