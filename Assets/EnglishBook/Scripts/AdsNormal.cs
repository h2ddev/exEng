using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdsNormal : MonoBehaviour {

	//---------- ONLY NECESSARY FOR ASSET PACKAGE INTEGRATION: ----------//

	#if UNITY_IOS
	private string gameId = "1602501";

	#elif UNITY_ANDROID
	private string gameId = "1602502";
	#endif

	void Awake ()
	{
		DontDestroyOnLoad (gameObject);
	}

	// Use this for initialization
	void Start ()
	{   
		if (Advertisement.isSupported) {
			Advertisement.Initialize (gameId, true);
		} 
	}

}
