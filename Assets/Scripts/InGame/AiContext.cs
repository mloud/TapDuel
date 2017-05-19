using System.Collections.Generic;
using System;
using UnityEngine;
using Data;

public class AiContext
{
	public AiContext(
		Func<Defs.Side, List<Mover>> getMovers, 
		Func<Defs.Side,Mover> getMoverShape, 
		Defs.Side side,  
		Action<Transform, Defs.Side> touchAction,
		AiSettings settings
	) 
	{
		GetMovers = getMovers;
		GetMoverShape = getMoverShape;
		TouchAction = touchAction;
		Side = side;
		Settings = settings;
	}
	public Func<Defs.Side, Mover> GetMoverShape;
	public Func<Defs.Side, List<Mover>> GetMovers;
	public Action<Transform, Defs.Side> TouchAction;
	public Defs.Side Side;
	public AiSettings Settings;
}

