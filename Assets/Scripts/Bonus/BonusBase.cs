using System;
using UnityEngine;

public class BonusBase : MonoBehaviour
{
	[SerializeField]
	AudioClip clip;

	public Defs.Side Side { get; set; }

	public GameObject ExplosionPrefab;
	public Action<BonusBase> Action;
	public Action<BonusBase> OutOfScreenAction;
	public string Name;
	public bool Falling = true;
	public bool DestroyOnHit;
	public float Speed;
	CircleCollider2D touchCollider;

	void Awake()
	{
		touchCollider = GetComponent<CircleCollider2D>();
	}

	void Update()
	{
		if (Falling) {
			transform.position += Time.deltaTime * new Vector3(0, Speed, 0);
		}	
		if ((transform.position.y + 0.3f)< Camera.main.ViewportToWorldPoint(Vector3.zero).y) {
			Falling = false;
			OutOfScreenAction(this);
			Destroy(gameObject);
		}
	}

	public void Explode()
	{
		var ps = Instantiate(ExplosionPrefab);
		ps.transform.position = transform.position;
		SFXManager.Instance.Play(clip);

		if (DestroyOnHit)
			Destroy(gameObject);
	}

	public bool IsInside(Vector3 point)
	{
		return touchCollider.OverlapPoint(point);
	}

	public virtual void Collect()
	{}
}

