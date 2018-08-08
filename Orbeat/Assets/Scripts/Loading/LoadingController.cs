using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LoadingController : Singleton<LoadingController>,IController {

    GameStateController gameStateController;

    public void Open(){
        gameStateController = GameStateController.Instance;
        gameStateController.Open();
    }


}
