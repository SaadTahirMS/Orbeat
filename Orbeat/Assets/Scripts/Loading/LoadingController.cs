using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LoadingController : Singleton<LoadingController> {

	public void Initialize(GameRefs refs){
        //StartCoroutine(LoadingScreenDelay(refs));
		GameStateController.Instance.Initialize (refs);
    }


    //IEnumerator LoadingScreenDelay(GameRefs refs){
    //    refs.loadingScreen.SetActive(true);
    //    yield return new WaitForSeconds(3);
    //    refs.loadingScreen.SetActive(false);

    //}



}
