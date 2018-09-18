using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplicationController : MonoBehaviour {

	private void OnApplicationQuit()
	{
		AppInBackGroundState ();
	}

	private void OnApplicationPause(bool isPaused)
	{
		if (isPaused)
			AppInBackGroundState ();
		else{
			//ResumeApplication
		}
	}

	private void AppInBackGroundState()
	{
		//PauseApp
	}

	#if UNITY_ANDROID

	private void Update()
	{
		if (Input.GetKeyDown (KeyCode.Escape)) {
			EventManager.DoFireBackButtonEvent ();
		}
	}

	#endif
}
