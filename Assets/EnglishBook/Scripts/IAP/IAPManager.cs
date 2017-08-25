using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;
using System;

public class IAPManager : MonoBehaviour, IStoreListener
{
 
	private static IStoreController m_StoreController;
	private static IExtensionProvider m_StoreExtensionProvider;
 
	public static string MC_001_499_COIN = "magiccrayons.001";
	public static string MC_02_998_COIN = "magiccrayons.02";
	public static string MC_03_2999_COIN = "magiccrayons.03";
	public static string MC_04_6999_COIN = "magiccrayons.04";
	public static string MC_05_10K_COIN = "magiccrayons.05";
	public static string MC_06_20K_COIN = "magiccrayons.06";

	private const string GGKEY_IAP = "MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAiUElPyve2yTH1WNtwHD0Gct1zy9PLkYxKLdWxQ9q7pGJ7ihS7HBGkPTO4HdwdRxCjmz4W+524gfe8aW2Q3FsgsLaCCVg0AYVDwGnWsqIsMB5Scn6DV/9XGwTbevsbuXcppo3gK5dnzFr7wKJpjkkWyIZLRgiMo7wDGMKMxxoY0Gbri6/rge2ARdAK5gx554QaDr2oGJpVPi6gx3qoAK7P9TAf8GrYXigtGp4CqsDpBR7kLmVTSU6M3eRQrESGQtZtWziQG0VHLR8PjJ6TtaFHBfWN9xAMyXt9tO9yrXE45FK/6ykyx8T3qeC6SpuojgmPTrP0WqU8EQ90qEUrGfGpQIDAQAB";

	public static IAPManager Instance {
		get;
		private set;
	}

	void Awake ()
	{ 
		Instance = this;
		DontDestroyOnLoad (gameObject);
	}

	void Start ()
	{ 
		if (m_StoreController == null) { 
			InitializePurchasing ();
		}
	}

	public void InitializePurchasing ()
	{ 
		if (IsInitialized ()) { 
			return;
		}
 
		var builder = ConfigurationBuilder.Instance (StandardPurchasingModule.Instance ()); 
		builder.AddProduct (MC_001_499_COIN, ProductType.Consumable); 
		builder.AddProduct (MC_02_998_COIN, ProductType.Consumable);   
		builder.AddProduct (MC_03_2999_COIN, ProductType.Consumable); 
		builder.AddProduct (MC_04_6999_COIN, ProductType.Consumable);   
		builder.AddProduct (MC_05_10K_COIN, ProductType.Consumable); 
		builder.AddProduct (MC_06_20K_COIN, ProductType.Consumable);   

		#if UNITY_ANDROID
		builder.Configure<IGooglePlayConfiguration> ().SetPublicKey (GGKEY_IAP);
		#endif

		UnityPurchasing.Initialize (this, builder);
	}


	private bool IsInitialized ()
	{
		// Only say we are initialized if both the Purchasing references are set.
		return m_StoreController != null && m_StoreExtensionProvider != null;
	}


	public void Buy001_499COIN ()
	{ 
		BuyProductID (MC_001_499_COIN);
	}


	public void Buy02_998COIN ()
	{ 
		BuyProductID (MC_02_998_COIN);
	}

	public void Buy03_2999COIN ()
	{ 
		BuyProductID (MC_03_2999_COIN);
	}


	public void Buy04_6999KCOIN ()
	{ 
		BuyProductID (MC_04_6999_COIN);
	}

	public void Buy05_10KCOIN ()
	{ 
		BuyProductID (MC_05_10K_COIN);
	}


	public void Buy06_20KCOIN ()
	{ 
		BuyProductID (MC_06_20K_COIN);
	}

	private void BuyProductID (string productId)
	{
		// If Purchasing has been initialized ...
		if (IsInitialized ()) {
			// ... look up the Product reference with the general product identifier and the Purchasing 
			// system's products collection.
			Product product = m_StoreController.products.WithID (productId);

			// If the look up found a product for this device's store and that product is ready to be sold ... 
			if (product != null && product.availableToPurchase) {
				Debug.Log (string.Format ("Purchasing product asychronously: '{0}'", product.definition.id));
				// ... buy the product. Expect a response either through ProcessPurchase or OnPurchaseFailed 
				// asynchronously.
				m_StoreController.InitiatePurchase (product);
			}
				// Otherwise ...
				else {
				// ... report the product look-up failure situation  
				Debug.Log ("BuyProductID: FAIL. Not purchasing product, either is not found or is not available for purchase");
			}
		}
			// Otherwise ...
			else {
			// ... report the fact Purchasing has not succeeded initializing yet. Consider waiting longer or 
			// retrying initiailization.
			Debug.Log ("BuyProductID FAIL. Not initialized.");
		}
	}

	public void OnInitialized (IStoreController controller, IExtensionProvider extensions)
	{ 
		Debug.Log ("OnInitialized: PASS");
 
		m_StoreController = controller; 
		m_StoreExtensionProvider = extensions;
	}


	public void OnInitializeFailed (InitializationFailureReason error)
	{
		// Purchasing set-up has not succeeded. Check error for reason. Consider sharing this reason with the user.
		Debug.Log ("OnInitializeFailed InitializationFailureReason:" + error);
	}


	public PurchaseProcessingResult ProcessPurchase (PurchaseEventArgs args)
	{ 
		try {
			if (String.Equals (args.purchasedProduct.definition.id, MC_001_499_COIN, StringComparison.Ordinal)) { 
				Debug.Log (string.Format ("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id)); 
				SaveDataInfo.SaveGold += 499;
			} else if (String.Equals (args.purchasedProduct.definition.id, MC_02_998_COIN, StringComparison.Ordinal)) {
				Debug.Log (string.Format ("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id)); 
				SaveDataInfo.SaveGold += 998;
			} else if (String.Equals (args.purchasedProduct.definition.id, MC_03_2999_COIN, StringComparison.Ordinal)) {
				Debug.Log (string.Format ("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id)); 
				SaveDataInfo.SaveGold += 2999;
			} else if (String.Equals (args.purchasedProduct.definition.id, MC_04_6999_COIN, StringComparison.Ordinal)) {
				Debug.Log (string.Format ("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id)); 
				SaveDataInfo.SaveGold += 6999;
			} else if (String.Equals (args.purchasedProduct.definition.id, MC_05_10K_COIN, StringComparison.Ordinal)) {
				Debug.Log (string.Format ("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id)); 
				SaveDataInfo.SaveGold += 10000;
			} else if (String.Equals (args.purchasedProduct.definition.id, MC_06_20K_COIN, StringComparison.Ordinal)) {
				Debug.Log (string.Format ("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id)); 
				SaveDataInfo.SaveGold += 20000;
			} else {
				Debug.Log (string.Format ("ProcessPurchase: FAIL. Unrecognized product: '{0}'", args.purchasedProduct.definition.id));
			}
			EventManager.Instance.RaiseEventInTopic ("CHANGE_BALANCE");
		} catch (Exception ex) {
			Debug.Log (string.Format ("Exception: '{0} - {1}'", ex.Message, ex.StackTrace));
		} 
		return PurchaseProcessingResult.Complete;
	}


	public void OnPurchaseFailed (Product product, PurchaseFailureReason failureReason)
	{
		Debug.Log (string.Format ("OnPurchaseFailed: FAIL. Product: '{0}', PurchaseFailureReason: {1}", product.definition.storeSpecificId, failureReason));
	}
}