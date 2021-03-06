﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Facebook.Unity;

public class FaceTrack : MonoBehaviour
{

	// Use this for initialization
	void Awake ()
	{ 
		if (FB.IsInitialized) {
			FB.ActivateApp ();
		} else {
			//Handle FB.Init
			FB.Init (() => {
				FB.ActivateApp ();
			});
		}
	}
	// Unity will call OnApplicationPause(false) when an app is resumed
	// from the background
	void OnApplicationPause (bool pauseStatus)
	{
		// Check the pauseStatus to see if we are in the foreground
		// or background
		if (!pauseStatus) {
			//app resume
			if (FB.IsInitialized) {
				FB.ActivateApp ();
			} else {
				//Handle FB.Init
				FB.Init (() => {
					FB.ActivateApp ();
				});
			}
		}
	}
}
