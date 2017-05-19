using UnityEngine;
using System;
using System.Runtime.InteropServices;

public class InputManager : MonoBehaviour 
{
	public Action<Vector3, Defs.Side> TouchAction;

	void Awake()
	{
		Input.multiTouchEnabled = true;
	}
	
	void Update () 
	{
		#if UNITY_EDITOR
		if (Input.GetMouseButtonDown(0)) {
			var side = GetSide(Input.mousePosition.x);
			var wPos = ScreenToWorldPoint(Input.mousePosition);
			TouchAction(wPos, side);
		}
		#else
		for (int i = 0; i < Input.touchCount; ++i) {
			if (Input.touches[i].phase == TouchPhase.Began) {
				var side = GetSide(Input.mousePosition.x);
				var wPos = ScreenToWorldPoint(Input.touches[i].position);
				TouchAction(wPos, side);
			}
		}
		#endif
	}

	Defs.Side GetSide(float screenXPos)
	{
		return Camera.main.ScreenToWorldPoint(new Vector3(screenXPos, 0, 0)).x < 0 ? Defs.Side.Left : Defs.Side.Right;
	}

	Vector3 ScreenToWorldPoint(Vector2 screenPos)
	{
		return Camera.main.ScreenToWorldPoint(screenPos);
	}
}
