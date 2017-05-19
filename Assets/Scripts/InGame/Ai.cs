using System;
using System.Runtime.Remoting.Contexts;
using System.Collections;
using UnityEngine;

public class Ai : MonoBehaviour
{
	AiContext context;

	public Defs.Side Side { get { return context.Side; }}

	public void Set(AiContext context)
	{
		this.context = context;
	}

	void Update()
	{}

	public void OnShapeChanged(Defs.Side side)
	{
		if (side == context.Side) {
			float delay = UnityEngine.Random.Range(context.Settings.MinReactionTime, context.Settings.MaxReactionTime);
			StartCoroutine(ReactToChangeWithDelay(delay));
		}
	}

	IEnumerator ReactToChangeWithDelay(float delay)
	{
		yield return new WaitForSeconds(delay);
		var aiMovers = context.GetMovers(context.Side);
		var rnd01= UnityEngine.Random.Range(0.0f, 1.0f);

		Mover touchedMover = null;
		if (rnd01 < context.Settings.ErrorRate) {
			touchedMover = aiMovers.Find(x=>x.type != context.GetMoverShape(context.Side).type);
		} else {
			touchedMover = aiMovers.Find(x=>x.type == context.GetMoverShape(context.Side).type);
		}
		context.TouchAction(touchedMover.transform, context.Side);
		//context.TouchAction(touchedMover.transform.position, context.Side);
	}

}

