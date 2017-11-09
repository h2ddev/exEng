using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;

public class AdsManager : MonoBehaviour
{
	//---------- ONLY NECESSARY FOR ASSET PACKAGE INTEGRATION: ----------//

	#if UNITY_IOS
	private string gameId = "1602501";


#elif UNITY_ANDROID
	private string gameId = "1602502";
	#endif


	private string placementId = "rewardedVideo";

	public static AdsManager Instance;

	void Awake ()
	{
		DontDestroyOnLoad (gameObject);
		Instance = this;
	}

	// Use this for initialization
	void Start ()
	{    
		if (Advertisement.isSupported) {
			Advertisement.Initialize (gameId, true);
		} 
		StartCoroutine (CountdownTime ()); 
	}

   IEnumerator CountdownTime ()
	{ 
		while (true) {
			yield return null;
			//Debug.Log ("SaveDataInfo.TimeShowAds " + SaveDataInfo.TimeShowAds);
			if (SaveDataInfo.TimeShowAds > 0) {
				SaveDataInfo.TimeShowAds--;  
			}
		}
	}

	public void ShowAd ()
	{
		if (Advertisement.IsReady ()) {
			ShowOptions options = new ShowOptions ();
			options.resultCallback = HandleShowResult;

			Advertisement.Show (placementId, options);
		}
	}

	void HandleShowResult (ShowResult result)
	{
		if (result == ShowResult.Finished) {
			SaveDataInfo.SaveGold += 24;
			EventManager.Instance.RaiseEventInTopic ("CHANGE_BALANCE");
		} else if (result == ShowResult.Skipped) {
			Debug.LogWarning ("Video was skipped - Do NOT reward the player");

		} else if (result == ShowResult.Failed) {
			Debug.LogError ("Video failed to show");
		}
	}


	// Update is called once per frame
	public void ShowNormalAds ()
	{ 
		if (Advertisement.IsReady ()) {
			Advertisement.Show ();
		}

	}


}
