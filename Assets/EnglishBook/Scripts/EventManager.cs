using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EventManager : MonoBehaviour
{
	public static readonly string CHANGE_BALANCE = "CHANGE_BALANCE";

  
	public static EventManager Instance {
		get;
		private set;
	}

	private Dictionary<string, List<Action>> Topics = new Dictionary<string, List<Action>> ();

	void Awake ()
	{
		if (Instance != null) {
			Destroy (gameObject);
			return;
		}
		DontDestroyOnLoad (this.gameObject);
		Instance = this;
		CreateDefaultTopic ();
	}

	#region API

	public void CreateTopic (string TopicName)
	{
		if (Topics.ContainsKey (TopicName))
			return;

		Topics.Add (TopicName, new List<Action> ());
	}

	public void SubscribeTopic (string TopicName, Action Callback)
	{
		if (!Topics.ContainsKey (TopicName)) {
			Topics [TopicName] = new List<Action> ();
		}

		var subscribers = Topics [TopicName];
		if (subscribers == null) {
			subscribers = new List<Action> ();
			Topics [TopicName] = subscribers;
		}

		subscribers.Add (Callback);
	}


	public void UnSubscribeTopic (string TopicName, Action subscriber)
	{
		if (!Topics.ContainsKey (TopicName))
			return;

		var subscribers = Topics [TopicName];
		if (subscribers == null) {
			subscribers = new List<Action> ();
			Topics [TopicName] = subscribers;
			return;
		}

		if (Topics [TopicName].Remove (subscriber)) {
			Debug.Log ("REMOVE SUBSCRIBER SUCCESS : " + subscriber.Method.Name);
		} else {
			Debug.Log ("REMOVE SUBSCRIBER FAIL : " + subscriber.Method.Name);
		}
	}

	public void RaiseEventInTopic (string TopicName)
	{
		if (!Topics.ContainsKey (TopicName))
			return;

		var subscribers = Topics [TopicName];
		if (subscribers == null) {
			subscribers = new List<Action> ();
			Topics [TopicName] = subscribers;
			return;
		}

		int subscribersCount = subscribers.Count;

		List<Action> RemovableSubscribers = new List<Action> ();

		for (int i = 0; i < subscribersCount; i++) {
			var subscriber = subscribers [i];
			try {
				if (subscriber != null)
					subscriber ();
			} catch (Exception e) {
				Debug.Log (String.Format ("Exception when call subscriber for topic {0} : {1} - {2}", TopicName, e.Message, e.StackTrace));
				RemovableSubscribers.Add (subscriber);
			}
		}

		int RemovableSubscriberCount = RemovableSubscribers.Count;
		for (int i = 0; i < RemovableSubscriberCount; i++) {
			UnSubscribeTopic (TopicName, RemovableSubscribers [i]);
		}
	}

	#endregion

	#region Processor

	private void CreateDefaultTopic ()
	{  
		CreateTopic (CHANGE_BALANCE);
	}

	#endregion
} 