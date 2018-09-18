using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GamePauseController : BaseController {

	#region Variables

	private GamePauseViewController gamePauseViewController;

	#endregion Variables

	#region Life Cycle Methods

	public GamePauseController()
	{
		gamePauseViewController = new GamePauseViewController ();
	}

	public void Open(GameObject obj, object viewModel = null)
	{
		gamePauseViewController.Open (obj);
		Time.timeScale = 0.00001f;
	}

	public void Close()
	{
		ResumeGame ();
		gamePauseViewController.Close ();
	}

	#endregion Life Cycle Methods

	#region Game State

	private void ResumeGame()
	{
		GameStateController.Instance.StartCoroutine (BringTimeScaleToNormal ());
	}

	private IEnumerator BringTimeScaleToNormal()
	{
		float timeScale = 0.00001f;
		while (timeScale < 1) {
			yield return null;
			timeScale += Time.deltaTime;
			Time.timeScale = timeScale;
		}
		Time.timeScale = 1;
	}

	#endregion Game State
}
