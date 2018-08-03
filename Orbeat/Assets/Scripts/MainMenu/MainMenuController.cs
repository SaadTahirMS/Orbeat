using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuController : Singleton<MainMenuController>, IController {

    GameplayContoller gameplayContoller;

    public GameObject playBtn;

    public void Open(){
        print("MainMenu Controller Opened");
        ActivatePlayBtn();
    }

    void ActivatePlayBtn(){
        playBtn.SetActive(true);
    }

    void DeactivatePlayBtn(){
        playBtn.SetActive(false);
    }

    // this will make a call to GameplayController to start the gameplay
    public void PlayGame(){
        DeactivatePlayBtn();
        gameplayContoller = GameplayContoller.Instance;
        gameplayContoller.Open();
    }


}
