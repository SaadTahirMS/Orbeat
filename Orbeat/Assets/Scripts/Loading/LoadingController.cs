using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LoadingController : Singleton<LoadingController> {

	public void Initialize(ViewRefs refs){
		GameStateController.Instance.Initialize (refs);
    }


}
