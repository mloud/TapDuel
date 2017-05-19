using UnityEngine;
[ExecuteInEditMode, RequireComponent(typeof(Renderer))]
public class SortingOrder : MonoBehaviour {

	public int Order;

	void Awake()
	{
		var ren = GetComponent<Renderer>();
		ren.sortingOrder = Order;
	}

	#if UNITY_EDITOR
	void Update()
	{
		GetComponent<Renderer>().sortingOrder = Order;
	}
	#endif

}
