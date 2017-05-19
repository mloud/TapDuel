using UnityEngine;

using System.Xml.Linq;


public class HPBar : MonoBehaviour 
{
	[SerializeField]
	Transform leftPlayer;

	[SerializeField]
	Transform rightPlayer;

	float initialLeftScale;
	float initialRightScale;

	Animator animator;

	void Awake()
	{
		initialLeftScale = leftPlayer.localScale.x;
		initialRightScale = rightPlayer.localScale.x;
		animator = GetComponent<Animator>();
	}

	public void Hide()
	{
		animator.enabled = false;
		var scale = rightPlayer.localScale;
		scale.x = 0.0f;
		rightPlayer.localScale = scale;

		scale = leftPlayer.localScale;
		scale.x = 0.0f;
		leftPlayer.localScale = scale;
	}

	public void PlayChargeAnim()
	{
		animator.enabled = true;
		animator.SetTrigger("charge");
	}

	public void DisableAnimator()
	{
		animator.enabled = false;
	}


	public void Set(float coef01)
	{
		float maxScale = initialLeftScale + initialRightScale;

		var scale = leftPlayer.localScale;
		scale.x = coef01 * maxScale;
		leftPlayer.localScale = scale;

		scale = rightPlayer.localScale;
		scale.x = maxScale - leftPlayer.localScale.x;
		rightPlayer.localScale = scale;
	}
}
