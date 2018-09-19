using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GamePauseController : BaseController {

	#region Variables

	private GamePauseViewController gamePauseViewController;

	private float timeMultiplier = 20;

	private bool pauseGame;

	#endregion Variables

	#region Life Cycle Methods

	public GamePauseController()
	{
		gamePauseViewController = new GamePauseViewController ();
	}

	public void Open(GameObject obj, object viewModel = null)
	{
		gamePauseViewController.Open (obj);
		pauseGame = true;
		Time.timeScale = 0.0000001f;
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
		pauseGame = false;
		GameStateController.Instance.StartCoroutine (BringTimeScaleToNormal ());
	}

	private IEnumerator BringTimeScaleToNormal()
	{
		float timeScale = 0.00001f;
		while (timeScale < 1) {
			yield return null;
			if (!pauseGame) {
				timeScale += Time.deltaTime * timeMultiplier;
				Time.timeScale = timeScale;
				timeScale = Time.timeScale;
			} else
				break;
		}

		if(pauseGame)
			Time.timeScale = 0.00001f;
		else
		Time.timeScale = 1;
	}

	#endregion Game State
}
