 
using UnityEngine;
using System.Collections;
 
[DisallowMultipleComponent]
public class Logo : MonoBehaviour
{
	/// <summary>
	/// The sleep time.
	/// </summary>
	public float sleepTime = 5;

	/// <summary>
	/// The name of the scene to load.
	/// </summary>
	public string nextSceneName;

	// Use this for initialization
	void Start ()
	{
		Invoke ("LoadScene", sleepTime);
	}

	private void LoadScene ()
	{
		if (string.IsNullOrEmpty (nextSceneName)) {
			return;
		}
		Application.LoadLevel (nextSceneName);
	}
}
