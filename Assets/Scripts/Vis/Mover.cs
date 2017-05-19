using UnityEngine;
using System.Collections;
using System.IO;
using System;

public class Mover : MonoBehaviour 
{
	Collider2D collider;
	SpriteRenderer ren;
	Animator anim;

	public enum Type
	{
		A,
		B,
		C
	}

	public enum Side 
	{
		Left,
		Right
	}

	public Side side;
	public Type type;

	bool useCustomColor;
	Color customColor;

	void Awake()
	{
		collider = GetComponent<Collider2D>();
		ren = GetComponent<SpriteRenderer>();
		anim = GetComponent<Animator>();
	}

	public bool IsInside(Vector3 point)
	{
		return collider.OverlapPoint(point);
	}

	public void SetColor(Color color)
	{
		useCustomColor = true;
		customColor = color;
	}

	public void PlayIn()
	{
		anim.SetTrigger("in");
	}

	public void PlayOut()
	{
		anim.SetTrigger("out");
	}

	public void PlayCorrectTap()
	{
		anim.SetTrigger("ok");
	}

	public void PlayWrongTap()
	{
		anim.SetTrigger("ko");
	}

	public void DisableAnimator()
	{
		anim.enabled = false;
	}

	public IEnumerator MoveTo(Vector3 pos)
	{
		const float duration = 0.0f;
		var dir = pos - transform.position;
		var startPos = transform.position;
		float t = 0;
		float startTime = Time.time;
		while(t < 1) {
			if (duration < 0.001)
				t = 1.0f;
			else
				t = Math.Min((Time.time - startTime) / duration, 1);
			transform.position = startPos + dir * t;
			yield return 0;
		}
	}

	void LateUpdate()
	{
		if (useCustomColor) {
			ren.color = customColor;
		}
	}
}
