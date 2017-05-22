using System;
using UnityEngine;

[ExecuteInEditMode]
public class Anchor : MonoBehaviour
{

	[SerializeField]
	Vector2 relativeCoord;

	void Update()
	{
		#if UNITY_EDITOR
		if (!Application.isPlaying) {
			relativeCoord.x = transform.position.x / (Camera.main.aspect * 2 * Camera.main.orthographicSize);
			relativeCoord.y = transform.position.y / (2 * Camera.main.orthographicSize);
		}
		#endif
	}

	void Awake()
	{
		var pos = new Vector3(
			relativeCoord.x * (Camera.main.aspect * 2 * Camera.main.orthographicSize),
			relativeCoord.y * (2 * Camera.main.orthographicSize),
			0
		);
		transform.position = pos;
	}
}

