using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManagerNameAvata : MonoBehaviour
{ 
	public Text Name; 
	public Image Avata; 
	public Sprite[] sprAvata;

	public static ManagerNameAvata Instance {
		get;
		private set;
	}

	void Awake(){
		Instance = this;
	}

	void Start ()
	{
		Name.text = SaveDataInfo.SaveName; 
		Avata.sprite = sprAvata [SaveDataInfo.SaveSex]; 
	}

}
