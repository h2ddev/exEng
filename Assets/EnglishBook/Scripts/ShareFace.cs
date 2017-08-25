using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShareFace : MonoBehaviour
{
	private string AppID = "1593340424049694";


	private string Link = "";

 
	private string Picture = "https://lh3.googleusercontent.com/2lWBvQXX2RsUKXTeH9YKlrR5Q-W-AIHAwpokjy_6zorrhbtvLTKip1OovdOx8sTVPlU";


	private string Name = "Magic Crayons - I can write Numbers and Letters";


	private string Caption = "I love to share this Magic Crayons App for teaching my kids to write in easy way/ easily.";


	private string Description = "A simple application for parents with young children learning to write and learn English. Just play, learn to write, and learn English.\n- Letter writing: Lowercase letters, capital letters to guide each stroke.\n- Literacy in English from A to Z.\n- Digital Writing: Simple, easy-to-remember line guide.\n- The phrase is based on the alphabet.\n- Interface smart, fun but not harmful to the eye.\n- Functional alert if baby play too long.\n- No in-app advertising.\n* Note to parents:\nIf you are looking for an educational application that helps your baby to play while learning Magic Crayons is worth it for you.\nYou and your baby will love it!";
 

	public void ShareOnFB ()
	{
		#if UNITY_ANDROID
		Link = "https://play.google.com/store/apps/details?id=iapi.bluewhale.learningenglish.write";
		#elif UNITY_IOS 
		Link = "https://itunes.apple.com/us/app/magic-crayons-i-can-write-numbers-and-letters/id1274208840?ls=1&mt=8";
		#endif

		Application.OpenURL ("https://www.facebook.com/dialog/feed?" + "app_id=" + AppID + "&link=" + Link + "&picture=" + Picture + "&name=" + ReplaceSpace (Name) + "&caption=" + ReplaceSpace (Caption) + "&description=" + ReplaceSpace (Description) + "&redirect_uri=https://facebook.com/");

	} 

	string ReplaceSpace (string val)
	{ 
		return val.Replace (" ", "%20");

	}

}
