using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SupportController {
	

	public void SendEmailToSupport()
	{
		#if UNITY_EDITOR
		string appName 	= UnityEditor.PlayerSettings.productName.Replace(" ", "%20");
		string version 	= "Version%3A%20"+ UnityEditor.PlayerSettings.bundleVersion;
		string platform = "Platform%3A%20"+ Application.platform;
		#else
		string appName 	= Application.productName.Replace(" ", "%20");
		string version 	= "Version%3A%20"+ Application.version;
		string platform = "Platform%3A%20"+ Application.platform;
		#endif

		string OS = "OS%3A%20"+Application.platform + "%20("+WWW.EscapeURL(SystemInfo.operatingSystem)+")"+ "%20("+WWW.EscapeURL(SystemInfo.deviceModel)+")";
		string DID = "DID%3A%20"+WWW.EscapeURL(SystemInfo.deviceUniqueIdentifier);
		string mailTo = ThirdPartyConstants.SupportMailToURL;
		mailTo = mailTo.Replace ("_AppName_", appName);
		mailTo = mailTo.Replace("_OS_", OS);
		mailTo = mailTo.Replace("_Version_", version);
		mailTo = mailTo.Replace("_Platform_", platform);
		mailTo = mailTo.Replace("_DID_", DID);
		Application.OpenURL(mailTo);
	}

	public void RateApplication()
	{
		Application.OpenURL (ThirdPartyConstants.AppRatingURL);
	}
}
