using UnityEngine;
using System.Linq;


[ExecuteInEditMode]
public class SortingLayer : MonoBehaviour {

	public string Layer;

	void Awake()
	{
		var rens = GetComponentsInChildren<Renderer>().ToList();
		rens.ForEach(x=>x.sortingLayerName = Layer);
	}

	#if UNITY_EDITOR
	void Update()
	{
		var rens = GetComponentsInChildren<Renderer>().ToList();
		rens.ForEach(x=>x.sortingLayerName = Layer);
	}
	#endif

}
