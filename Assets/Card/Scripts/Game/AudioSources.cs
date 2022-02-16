using UnityEngine;
using System.Collections;
public class AudioSources : MonoBehaviour {
	public AudioClip waterBubbleSound;
	[HideInInspector]
	public AudioSource[] audioSources;
	public static AudioSources instance;
	void Awake ()
	{
		if (instance == null) {
			instance = this;
			DontDestroyOnLoad(gameObject);
		} else {
			Destroy (gameObject);
		}
		audioSources = GetComponents<AudioSource> ();
	}

	public void PlayWaterBubbleSound ()
	{
		audioSources [1].clip = waterBubbleSound;
		audioSources [1].Play ();
	}
}
