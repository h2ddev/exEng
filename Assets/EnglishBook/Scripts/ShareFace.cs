using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShareFace : MonoBehaviour
{
	private string AppID = "1593340424049694";


	private string Link = "https://itunes.apple.com/us/app/magic-crayons-i-can-write-numbers-and-letters/id1274208840?ls=1&mt=8";

 
	private string Picture = "https://kenhthongtinbds.com/wp-content/uploads/2017/08/icon-1024x1024.jpg";


	private string Name = "Magic Crayons - I can write Numbers and Letters";


	private string Caption = "I love to share this Magic Crayons App for teaching my kids to write in easy way/ easily.";


	private string Description = "Magic Crayons - I can write Numbers and Letters.\nA simple Application for parents to teach their kids how to write and learn English. Learning to write English by playing.\n- Writing Letters: to give full particular strokes of every lowercase and capitalize Letter.\n- Reading Letters in English from A to Z.\n- Writing Numbers: to give instruction of every stroke in simple and easy way.\n- Learning to write phrases based on Alphabet.\n- Friendly and smart App Interface and safe for eyes.\n- Warning Function: to keep your kids from playing too much.\n* Note for parents:\nIf you are in search of a learning by playing educational App for your kids, then Magic Crayons is designed for you.\nParents and kids will definitely love it!";

	public static ShareFace Instance {
		get;
		private set;
	}

	void Awake ()
	{  
		Instance = this; 
	}

	public void ShareOnFB ()
	{
		#if UNITY_ANDROID
		Link = "https://play.google.com/store/apps/details?id=iapi.bluewhale.learningenglish.write";
		#endif

		Application.OpenURL ("https://www.facebook.com/dialog/feed?" + "app_id=" + AppID + "&link=" + Link + "&picture=" + Picture + "&name=" + ReplaceSpace (Name) + "&caption=" + ReplaceSpace (Caption) + "&description=" + ReplaceSpace (Description) + "&redirect_uri=https://facebook.com/");

		if (SaveDataInfo.TimeShareFace <= 0) {
			SaveDataInfo.SaveGold += 24;	
			SaveDataInfo.TimeShareFace = 28800;
			EventManager.Instance.RaiseEventInTopic ("CHANGE_BALANCE");
		}  
	}

	string ReplaceSpace (string val)
	{ 
		return val.Replace (" ", "%20");

	}

}
