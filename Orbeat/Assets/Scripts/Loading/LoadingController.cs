using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LoadingController : Singleton<LoadingController> {

	public void Initialize(GameRefs refs){
		GameStateController.Instance.Initialize (refs);
    }


}
