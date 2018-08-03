using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateController : Singleton<GameStateController>,IController {

    MainMenuController mainMenuController;
    public void Open(){
        print("GameState Controller Opened");
        mainMenuController = MainMenuController.Instance;
        mainMenuController.Open();
    }
}
