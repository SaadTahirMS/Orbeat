using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;


public class InAppPurchaseController: IStoreListener
{		
	#region Variabels

	private static IStoreController m_StoreController;
	private static IExtensionProvider m_StoreExtensionProvider;

	#endregion Variabels

	#region Initialization


	public InAppPurchaseController(string[] consumableIApps, string[] nonConsumableIApps, string storePublicKey)
	{
		if (m_StoreController == null)
		{
			InitializePurchasing(consumableIApps, nonConsumableIApps, storePublicKey);
		}
	}

	private void InitializePurchasing(string[] consumableIApps, string[] nonConsumableIApps, string storePublicKey)
	{
		if (IsInitialized ())
			return;

		var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());


		if (consumableIApps != null) {
			for (int i = 0; i < consumableIApps.Length; i++) {
				builder.AddProduct (consumableIApps [i], ProductType.Consumable);
				ThirdPartyEventManager.DoFireLogDataEvent (consumableIApps [i]);
			}
		}

		if (nonConsumableIApps != null) {
			for (int i = 0; i < nonConsumableIApps.Length; i++) {
				builder.AddProduct (nonConsumableIApps [i], ProductType.NonConsumable);
				ThirdPartyEventManager.DoFireLogDataEvent (nonConsumableIApps [i]);
			}
		}

		#if UNITY_ANDROID
		builder.Configure<IGooglePlayConfiguration>().SetPublicKey(storePublicKey);
		#endif

		UnityPurchasing.Initialize(this, builder);
	}


	private bool IsInitialized()
	{
		return m_StoreController != null && m_StoreExtensionProvider != null;
	}

	public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
	{
		ThirdPartyEventManager.DoFireLogDataEvent ("In-app initialized");

		m_StoreController = controller;
		m_StoreExtensionProvider = extensions;
	}


	public void OnInitializeFailed(InitializationFailureReason error)
	{
		ThirdPartyEventManager.DoFireLogDataEvent ("In-app initialized Failed " + error);
	}

	#endregion Initialization


	#region Purchasing Methods 

	/**
	 * Call this function to purchase as item
	 * Register OnPurchaseSuccessfull And OnPurchaseFail Events To Get A CallBack
	 */

	public void PurchaseItem(string productId)
	{
		if (IsInitialized())
		{
			Product product = m_StoreController.products.WithID(productId);

			if (product != null && product.availableToPurchase)
			{
				m_StoreController.InitiatePurchase(product);
			}
			else
			{
				ThirdPartyEventManager.DoFireLogDataEvent ("In-Apps Not Found");
			}
		}
		else
		{
			ThirdPartyEventManager.DoFireLogDataEvent ("In-Apps Not Initialized");
		}
	}

	public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args) 
	{
		ThirdPartyEventManager.DoFirePurchaseSuccessFullEvent (args.purchasedProduct.definition.id);
		return PurchaseProcessingResult.Complete;
	}

	public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
	{
		ThirdPartyEventManager.DoFirePurchaseFailEvent (product.definition.storeSpecificId, failureReason);
	}

	public void RestorePurchases()
	{
		if (!IsInitialized())
		{
			Debug.Log("RestorePurchases FAIL. Not initialized.");
			return;
		}

		if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.OSXPlayer)
		{
			Debug.Log("RestorePurchases started ...");

			var apple = m_StoreExtensionProvider.GetExtension<IAppleExtensions>();
			apple.RestoreTransactions((result) => {
			});
		}
		else if (Application.platform == RuntimePlatform.Android)
		{
			foreach (var product in m_StoreController.products.all)
			{
				if (product.hasReceipt) 
				{
					ThirdPartyEventManager.DoFirePurchaseSuccessFullEvent (product.definition.id);
				}
			}
		}		
		else
		{
			Debug.Log("RestorePurchases FAIL. Not supported on this platform. Current = " + Application.platform);
		}
	}

	#endregion Purchasing Methods 

	#region Pricing

	public string GetInAppItemPrice(string id)
	{
		return m_StoreController.products.WithID (id).metadata.localizedPrice.ToString ();
	}

	#endregion Pricing

}




