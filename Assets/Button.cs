using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using Vis;

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
		if (!Utils.IsPointerOverUI())
			clickEvent.Invoke();
	}

	void Action()
	{
		
	}

}
