using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Audio;
public class AudioManager : MonoBehaviour {

	public Sound[] sounds;
	public static AudioManager instance;
	// Use this for initialization
	void Awake () {
		if (instance == null)
			instance = this;
		else {
			Destroy (gameObject);
			return;
		}
		DontDestroyOnLoad (gameObject);

		foreach (Sound i in sounds) {
			i.source = gameObject.AddComponent<AudioSource> (); //goes through the sounds and adds component
			i.source.clip = i.clip;
			i.source.volume = i.volume;
			i.source.pitch = i.pitch;
			i.source.loop = i.loop;
		
		}
	}
	void Start()
	{
		PlaySound ("bgMusic");


	}
	
	public void PlaySound(string name)
	{
		Sound s = Array.Find (sounds, Sound => Sound.name == name);
		if (s == null) {
			Debug.Log ("Cannot find sound name");
			return;
		}
			
		s.source.Play ();

	}
	public void StopSound(string name)
	{
		Sound s = Array.Find (sounds, Sound => Sound.name == name);
		if (s == null) {
			Debug.Log ("Cannot find sound name");
			return;
		}
		s.source.Stop ();
	}


}
