using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Info : MonoBehaviour {

	public AudioSource cl;

	public void PlayEf(AudioClip clip)
	{
		cl.clip = clip;
		cl.Play();
	}
		public void PlayEf2(AudioClip clip)
	{
		cl.clip = clip;
		cl.Play();
	}	
	
}
