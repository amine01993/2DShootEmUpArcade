using System.Collections.Generic;
using UnityEngine;

public enum SoundType
{
	Sfx,
	Music
}

public class SoundManager
{
	private Dictionary<string, AudioClip> sfxDictionary;
	private Dictionary<string, AudioClip> musicDictionary;
	private AudioSource sfxSource;
	private AudioSource musicSource;

	public SoundManager(GameObject gameObject)
	{
		Debug.Log("Initializing SoundManager...");
		sfxDictionary = new Dictionary<string, AudioClip>();
		musicDictionary = new Dictionary<string, AudioClip>();

		sfxSource = gameObject.AddComponent<AudioSource>();
		sfxSource.volume = 1.0f;

		musicSource = gameObject.AddComponent<AudioSource>();
		musicSource.volume = 1f;
		musicSource.loop = true;

		AddSound("laserShoot", Resources.Load<AudioClip>("laserShoot"), SoundType.Sfx);
		AddSound("collect", Resources.Load<AudioClip>("collect"), SoundType.Sfx);
		AddSound("crash", Resources.Load<AudioClip>("crash"), SoundType.Sfx);
		AddSound("explosion", Resources.Load<AudioClip>("explosion"), SoundType.Sfx);
		AddSound("hitHurt", Resources.Load<AudioClip>("hitHurt"), SoundType.Sfx);
		AddSound("Main Music", Resources.Load<AudioClip>("Alone Against Enemy"), SoundType.Music);
	}

	// Add a sound to the dictionary.
	public void AddSound(string soundKey, AudioClip audioClip, SoundType soundType)
	{
		Dictionary<string, AudioClip> targetDictionary = GetDictionaryByType(soundType);

		if (targetDictionary == null) return;
		if (!targetDictionary.ContainsKey(soundKey))
		{
			targetDictionary.Add(soundKey, audioClip);
		}
		else
		{
			Debug.LogWarning("Sound key " + soundKey + " already exists in the " + soundType + " dictionary!");
		}
	}

	// Play a sound by key interface.
	public void PlaySound(string soundKey)
	{
		Play(soundKey, SoundType.Sfx);
	}

	// Play music by key interface.
	public void PlayMusic(string soundKey)
	{
		Play(soundKey, SoundType.Music);
	}

	public void StopMusic()
	{
		musicSource.Stop();
	}

	public void SetSoundVolume(float volume)
	{
		sfxSource.volume = volume;
	}

	public void SetMusicVolume(float volume)
	{
		musicSource.volume = volume;
	}

	public void SetMasterVolume(float volume)
	{
		AudioListener.volume = volume;
	}

	public void SetPanSetero(float volume)
	{
		sfxSource.panStereo = volume;
		musicSource.panStereo = volume;
	}

	private void Play(string soundKey, SoundType soundType)
	{
		switch (soundType)
		{
			case SoundType.Sfx:
				if (TryGetClip(soundKey, sfxDictionary, out AudioClip sfxClip))
				{
					sfxSource.PlayOneShot(sfxClip);
				}
				break;
			case SoundType.Music:
				if (TryGetClip(soundKey, musicDictionary, out AudioClip musicClip))
				{
					musicSource.Stop();
					musicSource.clip = musicClip;
					musicSource.Play();
				}
				break;
			default:
				Debug.LogError("Unknown sound type: " + soundType);
				break;
		}
	}

	private bool TryGetClip(string soundKey, Dictionary<string, AudioClip> dictionary, out AudioClip clip)
	{
		if (dictionary.TryGetValue(soundKey, out clip))
		{
			return true;
		}
		Debug.LogWarning("Sound key " + soundKey + " was not found!");
		return false;
	}

	private Dictionary<string, AudioClip> GetDictionaryByType(SoundType soundType)
	{
		switch (soundType)
		{
			case SoundType.Sfx:
				return sfxDictionary;
			case SoundType.Music:
				return musicDictionary;
			default:
				Debug.LogError("Unknown sound type: " + soundType);
				return null;
		}
	}
}
