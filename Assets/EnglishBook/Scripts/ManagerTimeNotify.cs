using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerTimeNotify : MonoBehaviour
{ 
	public GameObject Notify;
	public static ManagerTimeNotify Instance {
		get;
		private set;
	} 
	void Awake(){
		Instance = this;
	}
}
