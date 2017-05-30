using UnityEngine;

public class Popup : MonoBehaviour
{
	Animator animator;

	void Awake()
	{
		animator = GetComponent<Animator>();
	}

	public void Open()
	{
	}

	public void Close()
	{
		if (animator != null) {
			animator.SetTrigger("close");
		} else {
			CloseFinished();
		}
	}

	public void CloseFinished()
	{
		Destroy(gameObject);
	}
}
