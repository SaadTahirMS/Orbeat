using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPartyController: Singleton<ThirdPartyController> {

	#region Variables And References

	public InAppPurchaseController iAppController;
	public PlayServicesController playServicesController;
	public AnalyticsController analyticsController;
	public AdmobController admobController;
	public LocalNotificationController localNotificationController;
	public SupportController supportController;

	#endregion Variables And References

	#region Constructor And Initialization

	protected ThirdPartyController () {}

	public void Initialize()
	{
		InitializeControllers ();
	}

	private void InitializeControllers()
	{
		iAppController = new InAppPurchaseController (ThirdPartyConstants.ConsumableIApps, ThirdPartyConstants.NonConsumableIApps, ThirdPartyConstants.KIAppPublicKey);
		playServicesController = new PlayServicesController ();
		analyticsController = new AnalyticsController ();
		admobController = new AdmobController (true, true, true);
		localNotificationController = new LocalNotificationController ();
		supportController = new SupportController ();
	}

	#endregion Constructor And Initialization
		
}
