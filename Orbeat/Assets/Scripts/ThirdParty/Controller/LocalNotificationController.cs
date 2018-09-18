using UnityEngine;
using System.Collections;
using System;
using Area730.Notifications;

public class LocalNotificationController
{		
	#region Initialization

	public LocalNotificationController()
	{
		Initialize ();
	}

	private void Initialize()
	{
		CancelNotifications ();
		RegisterNotifications ();
	}

	#endregion Initialization

	#region Local Notification Schedule/Cancel

	public void CreateLocalNotification (int id, string title, string message, double seconds, bool isRepeatable = false)
	{
		#if UNITY_ANDROID
		CreateAndroidNotification(id, title, message, seconds, isRepeatable);
		#elif UNITY_IPHONE
		CreateiOSLocalNotification(title, message, seconds);
		#endif
	}
		
	public void CancelNotifications()
	{
		#if UNITY_ANDROID
		AndroidNotifications.cancelAll();
		AndroidNotifications.clearAll();
		#elif UNITY_IPHONE
		ResetAppBadgeIconIOS();
		UnityEngine.iOS.NotificationServices.ClearLocalNotifications();
		UnityEngine.iOS.NotificationServices.CancelAllLocalNotifications();
		#endif
	}

	#endregion Local Notification Adder

	#region private methods

	private void RegisterNotifications()
	{
		#if UNITY_ANDROID
		#elif UNITY_IPHONE
		RegisterNotificationiOS();
		#endif
	}

	#if UNITY_ANDROID

	private void CreateAndroidNotification(int id, string title, string msg, double seconds, bool isRepeatable)
	{
		NotificationBuilder builder = new NotificationBuilder(id,title, msg);
		builder.setTicker (msg)
			.setDefaults (NotificationBuilder.DEFAULT_VIBRATE)
			.setDefaults (NotificationBuilder.DEFAULT_SOUND)
			.setAlertOnlyOnce (true)
			.setDelay ((long)(seconds * 1000))
			.setRepeating (isRepeatable)
			.setAutoCancel (true)
			.setColor ("#FFFFFF00")
			.setSmallIcon(ThirdPartyConstants.SmallIconName)
			.setLargeIcon(ThirdPartyConstants.LargeIconName);
		AndroidNotifications.scheduleNotification(builder.build());
	} 

	#elif UNITY_IPHONE

	private void RegisterNotificationiOS()
	{
		UnityEngine.iOS.NotificationServices.RegisterForNotifications(UnityEngine.iOS.NotificationType.Alert| UnityEngine.iOS.NotificationType.Badge |  UnityEngine.iOS.NotificationType.Sound);
	}

	private void ResetAppBadgeIconIOS() 
	{
		UnityEngine.iOS.LocalNotification setCountNotif = new UnityEngine.iOS.LocalNotification(); 
		setCountNotif.applicationIconBadgeNumber = -1;
		setCountNotif.hasAction = false;
		UnityEngine.iOS.NotificationServices.PresentLocalNotificationNow(setCountNotif); 
	}

	private void CreateiOSLocalNotification(string title, string msg, double seconds)
	{
		UnityEngine.iOS.LocalNotification notif = new UnityEngine.iOS.LocalNotification();
		notif.fireDate = DateTime.Now.AddMinutes(seconds);
		notif.alertAction = title;
		notif.alertBody = msg;
		notif.applicationIconBadgeNumber = 1;
		notif.soundName = UnityEngine.iOS.LocalNotification.defaultSoundName;
		UnityEngine.iOS.NotificationServices.ScheduleLocalNotification(notif);
	}

	#endif

	#endregion
}
