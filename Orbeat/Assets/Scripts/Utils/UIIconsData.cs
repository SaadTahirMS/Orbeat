using UnityEngine;
using System.Collections;

public class UIIconsData : MonoBehaviour 
{
	public Sprite[] opponentIcons;
	public Sprite[] playerIcons;

	public Sprite playerDefaultIcon;

	public Color playerFontColor;
	public Color playerBarColor;
	public Color opponentBarColor;
	public Color opponentFontColor;


	private static UIIconsData instance;
	public static UIIconsData Instance
	{
		get
		{
			if (instance == null)
				instance = GameObject.FindObjectOfType<UIIconsData> ();
			return instance;
		}
	}

	void Start()
	{
		instance = this;
	}
}
