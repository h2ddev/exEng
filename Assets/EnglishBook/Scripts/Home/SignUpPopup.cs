using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SignUpPopup : MonoBehaviour
{
	 
	[SerializeField]
	private Text PlaceHoderUserName;
	[SerializeField]
	private Text UserName;
	[SerializeField]
	private Toggle[] TogglSex;

	void Start ()
	{
		InitData ();
	}

	private void InitData ()
	{  
		string name = SaveDataInfo.SaveName; 
		PlaceHoderUserName.text = name;
		int index = SaveDataInfo.SaveSex;
		for (int i = 0; i < 2; i++) {
			if (i == index) {
				TogglSex [index].isOn = true;
			} else
				TogglSex [index].isOn = false;
		}
	}

	public void ClickSignUp ()
	{
		if (UserName.text != "") {
			SaveDataInfo.SaveName = UserName.text;
		}
		if (TogglSex [0].isOn) {
			SaveDataInfo.SaveSex = 0;
		}
		if (TogglSex [1].isOn) {
			SaveDataInfo.SaveSex = 1;
		}


		ManagerNameAvata.Instance.Name.text = SaveDataInfo.SaveName; 
		ManagerNameAvata.Instance.Avata.sprite = ManagerNameAvata.Instance.sprAvata [SaveDataInfo.SaveSex]; 
	}
}
