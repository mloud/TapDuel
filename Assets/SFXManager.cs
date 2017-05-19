using UnityEngine;

public class SFXManager : MonoBehaviour
{
	public static SFXManager Instance {
		get { 
			if (instance == null) {
				instance = new GameObject("TextManager").AddComponent<SFXManager>();
			}
			return instance;
		}
	}

	static SFXManager instance;

	AudioSource source;

	void Awake()
	{
		source = gameObject.AddComponent<AudioSource>();
	}

	public void Play(AudioClip clip)
	{
		source.PlayOneShot(clip);	
	}
}
