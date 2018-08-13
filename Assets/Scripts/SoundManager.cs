using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {
	public static SoundManager instance;
	public AudioSource Click;
	
	private void Awake()
	{
		if(instance == null) instance = this;
		else if (instance != this) Destroy(gameObject);
		DontDestroyOnLoad(gameObject);
		
	} 
	public void PlaySingle()
	{		
		Click.Play();	
	}
		
	// Update is called once per frame
	
}
