using UnityEngine;
using System.Collections;
using System;

public class LineController : MonoBehaviour 
{
	Animator anim;

	void Awake()
	{
		anim = GetComponent<Animator>();
	}

	public void PlayAppear()
	{
		anim.SetTrigger("play");
	}

	public void SetPosition(float x)
	{
		transform.position = new Vector3(x, 0, 0);	
	}

	public float GetPosition()
	{
		return transform.position.x;
	}

	public void Move(float distance)
	{
		StartCoroutine(MoveCoroutine(distance));
	}

	IEnumerator MoveCoroutine(float distance)
	{
		float dir = Mathf.Sign(distance);
		distance = Mathf.Abs(distance);
		while (distance > 0) {
			float step = Time.deltaTime * distance * 10;
			step = Math.Min(step,distance);
			distance -= step;
			MoveInstantly(dir * step);
			yield return 0;
		}
	}

	void MoveInstantly(float distance)
	{
		SetPosition(GetPosition() + distance);
	}
}
