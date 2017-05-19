using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class Button : MonoBehaviour
{
	[SerializeField]
	UnityEvent clickEvent;

	Animator anim;

	void Awake()
	{
		anim = GetComponent<Animator>();
	}

	void OnMouseUp () 
	{
		anim.SetTrigger("bump");
	}

	void Action()
	{
		clickEvent.Invoke();
	}
}
