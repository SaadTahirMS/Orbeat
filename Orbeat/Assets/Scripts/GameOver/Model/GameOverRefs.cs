using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverRefs : MonoBehaviour {

	public Text highScore;
	public Text headerText;
	public Text currentScoreText;
	public Text reviveText;
	public Text warningText;
	public Text restartText;

	public GameObject reviveObj;
	public GameObject restartObj;
	public GameObject homeObj;
	public GameObject highScoreBannerObj;

	public Image reviveFiller;

	[Header ("Leader Board")]

	public Image[] playerIcons;
	public Image[] stripeImages;
	public Text[] playerName;
	public Text[] score;
	public GameObject[] playerObj;

}
