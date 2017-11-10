 
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Advertisements;

 
public class UIEvents : MonoBehaviour
{
	public Text lbMoney;
	public PopupOpener BtnMoreCoin;


	#if UNITY_IOS
	private string gameId = "1602501";




#elif UNITY_ANDROID
	private string gameId = "1602502";
	#endif

	  
	private string placementId = "rewardedVideo";
	 
	void Start ()
	{
		UpdateMoney ();

		if (Advertisement.isSupported) {
			Advertisement.Initialize (gameId, true);
		} 
		EventManager.Instance.SubscribeTopic ("CHANGE_BALANCE", UpdateMoney);  
	}

	void UpdateMoney ()
	{
		if (lbMoney != null)
			lbMoney.text = SaveDataInfo.SaveGold.ToString ();
	}

	public void AlbumShapeEvent (TableShape tableShape)
	{ 
		if (tableShape == null) {
			return;
		}

		TableShape.selectedShape = tableShape;

		if (tableShape.isLocked) {
			//DataManager.SaveShapeLockedStatus (TableShape.selectedShape.ID, false,GameObject.Find (ShapesManager.shapesManagerReference).GetComponent<ShapesManager> ());
			ShowAd ();
			return;
		}

		if (SaveDataInfo.SaveGold > 0) {
			SaveDataInfo.SaveGold--;  
			EventManager.Instance.RaiseEventInTopic ("CHANGE_BALANCE");
			LoadGameScene ();
		} else { 
			if (BtnMoreCoin != null)
				BtnMoreCoin.OpenPopup ();
		}
	}


	private void ShowAd ()
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
			if (SaveDataInfo.SaveGold > 0) {
				SaveDataInfo.SaveGold--;  
				EventManager.Instance.RaiseEventInTopic ("CHANGE_BALANCE"); 
				LoadGameScene ();
			} else { 
				if (BtnMoreCoin != null)
					BtnMoreCoin.OpenPopup ();
			}
		} else if (result == ShowResult.Skipped) {
			Debug.LogWarning ("Video was skipped - Do NOT reward the player");

		} else if (result == ShowResult.Failed) {
			Debug.LogError ("Video failed to show");
		}
	}


	public void PointerButtonEvent (Pointer pointer)
	{
		if (pointer == null) {
			return;
		}
		if (pointer.group != null) {
			ScrollSlider scrollSlider = GameObject.FindObjectOfType (typeof(ScrollSlider)) as ScrollSlider;
			if (scrollSlider != null) {
				scrollSlider.DisableCurrentPointer ();
				FindObjectOfType<ScrollSlider> ().currentGroupIndex = pointer.group.Index;
				scrollSlider.GoToCurrentGroup ();
			}
		}
	}

	public void LoadMainScene ()
	{
		StartCoroutine (SceneLoader.LoadSceneAsync ("Home"));

//		SceneManager.LoadScene ("Home");
	}

	public void LoadGameScene ()
	{ 
		StartCoroutine (SceneLoader.LoadSceneAsync ("Game"));
		//		SceneManager.LoadScene ("Game"); 
	}

	public void LoadAlbumScene ()
	{
		if (!string.IsNullOrEmpty (ShapesManager.shapesManagerReference))
			StartCoroutine (SceneLoader.LoadSceneAsync (GameObject.Find (ShapesManager.shapesManagerReference).GetComponent<ShapesManager> ().sceneName));
	}

	public void LoadLowercaseAlbumScene ()
	{
		ShapesManager.shapesManagerReference = "LShapesManager";
		StartCoroutine (SceneLoader.LoadSceneAsync ("LowercaseAlbum"));

//		SceneManager.LoadScene ("LowercaseAlbum");
	}

	public void LoadUppercaseAlbumScene ()
	{
		ShapesManager.shapesManagerReference = "UShapesManager";
		StartCoroutine (SceneLoader.LoadSceneAsync ("UppercaseAlbum"));
//		SceneManager.LoadScene ("UppercaseAlbum");
	}

	public void LoadNumbersAlbumScene ()
	{
		ShapesManager.shapesManagerReference = "NShapesManager";
		StartCoroutine (SceneLoader.LoadSceneAsync ("NumbersAlbum"));
//		SceneManager.LoadScene ("NumbersAlbum");
	}

	public void LoadSentenceAlbumScene ()
	{
		ShapesManager.shapesManagerReference = "SShapesManager";
		StartCoroutine (SceneLoader.LoadSceneAsync ("SentenceAlbum"));
//		SceneManager.LoadScene ("SentenceAlbum");
	}

	public void NextClickEvent ()
	{
		try {
			GameObject.FindObjectOfType<GameManager> ().NextShape ();
		} catch (System.Exception ex) {

		}
	}

	public void PreviousClickEvent ()
	{
		try {
			GameObject.FindObjectOfType<GameManager> ().PreviousShape ();
		} catch (System.Exception ex) {
			
		}
	}

	public void SpeechClickEvent ()
	{
		Shape shape = GameObject.FindObjectOfType<Shape> ();
		if (shape == null) {
			return;
		}
		shape.Spell ();
	}

	public void ResetShape ()
	{
		GameManager gameManager = GameObject.FindObjectOfType<GameManager> ();
		if (gameManager != null) {
			if (!gameManager.shape.completed) {
				gameManager.DisableGameManager ();
				GameObject.Find ("ResetConfirmDialog").GetComponent<Dialog> ().Show ();
			} else {
				gameManager.ResetShape ();
			}
		}
	}

	public void PencilClickEvent (Pencil pencil)
	{
		if (pencil == null) {
			return;
		}
		GameManager gameManager = GameObject.FindObjectOfType<GameManager> ();
		if (gameManager == null) {
			return;
		}
		if (gameManager.currentPencil != null) {
			gameManager.currentPencil.DisableSelection ();
			gameManager.currentPencil = pencil;
		}
		gameManager.SetShapeOrderColor ();
		pencil.EnableSelection ();
	}

	public void ResetConfirmDialogEvent (GameObject value)
	{
		if (value == null) {
			return;
		}
		
		GameManager gameManager = GameObject.FindObjectOfType<GameManager> ();
		
		if (value.name.Equals ("YesButton")) {
			Debug.Log ("Reset Confirm Dialog : Yes button clicked");
			SaveDataInfo.SaveGold--; 
			EventManager.Instance.RaiseEventInTopic ("CHANGE_BALANCE");
			if (gameManager != null) {
				gameManager.ResetShape ();
			}
			
		} else if (value.name.Equals ("NoButton")) {
			Debug.Log ("Reset Confirm Dialog : No button clicked");
		}

		value.GetComponentInParent<Dialog> ().Hide ();

		if (gameManager != null) {
			gameManager.EnableGameManager ();
		}
	}


	public void ResetGame ()
	{
		DataManager.ResetGame ();
	}

	public void LeaveApp ()
	{
		//Application.Quit ();
		StartCoroutine (SceneLoader.LoadSceneAsync ("Home"));
//		SceneManager.LoadScene ("Home");
	}
}
