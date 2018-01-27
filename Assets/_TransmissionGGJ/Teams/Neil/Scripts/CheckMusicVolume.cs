using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckMusicVolume : MonoBehaviour
{
    
	void Start ()
    {
        this.GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("MusicVolume");
	}
	
	
	public void UpdateVolume()
    {
        this.GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("MusicVolume");
    }
}
