using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RateUsController : BaseController{

	#region Variables

	private RateUsViewController rateUsViewController;

	#endregion Variables

	public RateUsController()
	{
		rateUsViewController = new RateUsViewController ();
	}

	public void Open(GameObject obj, object viewModel = null)
	{
		rateUsViewController.Open (obj);
	}

	public void Close()
	{
		rateUsViewController.Close ();
	}

	#region Rate Us Handling

	#endregion Rate Us Handling
}
