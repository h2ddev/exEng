using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAPModel : MonoBehaviour
{
 	
	public void Buy001_499Coin ()
	{
		IAPManager.Instance.Buy001_499COIN ();
	}

	public void  Buy02_998Coin ()
	{
		IAPManager.Instance.Buy02_998COIN ();
	}

	public void Buy03_2999Coin ()
	{
		IAPManager.Instance.Buy03_2999COIN (); 
	}

	public void Buy04_6999Coin ()
	{
		IAPManager.Instance.Buy04_6999KCOIN (); 
	}

	public void Buy05_10KCoin ()
	{
		IAPManager.Instance.Buy05_10KCOIN (); 
	}

	public void Buy06_20KCoin ()
	{
		IAPManager.Instance.Buy06_20KCOIN (); 
	}
}
