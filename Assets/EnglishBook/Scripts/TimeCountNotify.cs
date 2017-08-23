using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeCountNotify : MonoBehaviour
{ 
	public float time = 1800f;

	public static TimeCountNotify Instance {
		get;
		private set;
	}

	void Awake ()
	{

		DontDestroyOnLoad (gameObject);
		StartCoroutine (CountdownTime ()); 
	}

	public IEnumerator CountdownTime ()
	{
		while (true) {
			yield return null;
			time -= Time.deltaTime;  
			if (time <= 0) {
				ManagerTimeNotify.Instance.Notify.SetActive (true); 
				yield return new WaitForSeconds (10f);
				ManagerTimeNotify.Instance.Notify.SetActive (false);
				time = 1800f;
			}
		}
	}
}
