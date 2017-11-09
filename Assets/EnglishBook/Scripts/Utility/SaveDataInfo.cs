using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveDataInfo
{
	private const string nameKey = "name";
	private const string sexKey = "sex";
	private const string goldKey = "gold";
	private const string timeshareFacedKey = "timeshareface";
	private const string timeshowAdsKey = "timeshowAds";

	public static string SaveName {
		get {
			if (PlayerPrefs.HasKey (nameKey))
				return PlayerPrefs.GetString (nameKey);
			else
				return "Name";
		}
		set {
			PlayerPrefs.SetString (nameKey, value);
		}
	}

	public static int SaveSex {
		get {
			if (PlayerPrefs.HasKey (sexKey))
				return PlayerPrefs.GetInt (sexKey);
			else
				return 0;
		}
		set {
			PlayerPrefs.SetInt (sexKey, value);
		}
	}

	public static int SaveGold {
		get {
			if (PlayerPrefs.HasKey (goldKey))
				return PlayerPrefs.GetInt (goldKey);
			else
				return 200;
		}
		set {
			PlayerPrefs.SetInt (goldKey, value);
		}
	}

	public static int TimeShareFace {
		get {
			if (PlayerPrefs.HasKey (timeshareFacedKey))
				return PlayerPrefs.GetInt (timeshareFacedKey);
			else
				return 0;
		}
		set {
			PlayerPrefs.SetInt (timeshareFacedKey, value);
		}
	}
	 
	public static int TimeShowAds {
		get {
			if (PlayerPrefs.HasKey (timeshowAdsKey))
				return PlayerPrefs.GetInt (timeshowAdsKey);
			else
				return 1800;
		}
		set {
			PlayerPrefs.SetInt (timeshowAdsKey, value);
		}
	}
}
