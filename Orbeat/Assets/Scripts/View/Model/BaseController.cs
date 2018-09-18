using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface BaseController {

	void Open (GameObject obj = null, object viewModel = null);
	void Close ();
}
	