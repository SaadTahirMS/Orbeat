using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateController : Singleton<GameStateController>,IController {

    MainMenuController mainMenuController;
    public void Open(){
        mainMenuController = MainMenuController.Instance;
        mainMenuController.Open();
    }
}
