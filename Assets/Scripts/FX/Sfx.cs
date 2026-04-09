using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Sfx : MonoBehaviour
{
	private AudioSource sfx;

	private void Start()
	{
		sfx = GetComponent<AudioSource>();
	}

	void Update()
	{
		if (!sfx.isPlaying)
		{
			Destroy(gameObject);
		}
	}
}
