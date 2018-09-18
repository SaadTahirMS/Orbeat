using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using GameAnalyticsSDK;

public class AnalyticsController {

	#region Unity Analytics

	public void SendUnityAnalyticsToServer(string eventToSend)
	{
		Analytics.CustomEvent (eventToSend);
	}

	public void SendUnityAnalyticsToServer(string eventToSend, Dictionary<string, object> data)
	{
		Analytics.CustomEvent (eventToSend, data);
	}

	#endregion Unity Analytics

	#region Google Analytics

	public void SendGACustomEvent(string eventName)
	{
		GameAnalytics.NewDesignEvent (eventName);
	}

	public void SendGACustomEvent(string eventName, float eventValue)
	{
		GameAnalytics.NewDesignEvent (eventName, eventValue);
	}

	public void SendGASourceEvent(string currencyName, float amount, string itemType, string itemID = null)
	{
		GameAnalytics.NewResourceEvent (GAResourceFlowType.Source, currencyName, amount, itemType, itemID);
	}

	public void SendGASinkEvent(string currencyName, float amount, string itemType, string itemID = null)
	{
		GameAnalytics.NewResourceEvent (GAResourceFlowType.Sink, currencyName, amount, itemType, itemID);
	}

	public static void UpdateCustomDimensions (string dimension1, string dimension2, string dimension3)
	{
		GameAnalytics.SetCustomDimension01 (dimension1);
		GameAnalytics.SetCustomDimension01 (dimension2);
		GameAnalytics.SetCustomDimension01 (dimension3);
	}
		
	#endregion Google Analytics
}
