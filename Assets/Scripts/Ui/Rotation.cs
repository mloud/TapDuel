using System;
using UnityEngine;

public class Rotation : MonoBehaviour
{
	[SerializeField]
	float Speed;

	void Update()
	{
		var rot = transform.localEulerAngles;
		rot.z += Speed;
		transform.localEulerAngles = rot;
	}
}


