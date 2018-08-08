using UnityEngine;

public class MainMenuController : Singleton<MainMenuController>, IController {

    GameplayContoller gameplayContoller;

    public GameObject playBtn;
    public GameObject restartBtn;

    public void Open(){
        ActivatePlayBtn();
    }

    void ActivatePlayBtn(){
        playBtn.SetActive(true);
    }

    void DeactivatePlayBtn(){
        playBtn.SetActive(false);
    }

    public void ActivateRestartBtn()
    {
        restartBtn.SetActive(true);
    }

    void DeactivateRestartBtn()
    {
        restartBtn.SetActive(false);
    }

    // this will make a call to GameplayController to start the gameplay
    public void PlayGame(){
        DeactivatePlayBtn();
        gameplayContoller = GameplayContoller.Instance;
        gameplayContoller.Open();

    }

    //a call from GameplayController to restart the game
    public void RestartGame(){
        DeactivateRestartBtn();
        gameplayContoller.StartGame();
    }


}
