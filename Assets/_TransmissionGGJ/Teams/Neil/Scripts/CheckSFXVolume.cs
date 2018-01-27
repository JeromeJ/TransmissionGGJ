using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckSFXVolume : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
        // remember volume level from last time
        this.GetComponent< AudioSource > ().volume = PlayerPrefs.GetFloat("SFXVolume");
    }
	

	public void UpdateVolume()
    {
        this.GetComponent< AudioSource > ().volume = PlayerPrefs.GetFloat("SFXVolume");
    }
}
