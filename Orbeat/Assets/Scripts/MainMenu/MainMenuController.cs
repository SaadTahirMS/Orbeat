using UnityEngine;

public class MainMenuController : Singleton<MainMenuController>, IController {

    GameplayContoller gameplayContoller;

    public GameObject playBtn;
    public GameObject restartBtn;

    public void Open(){
        ActivatePlayBtn();
        SoundController.Instance.SetMainMenuMusic(true);
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
        SoundController.Instance.SetMainMenuMusic(false);
        gameplayContoller = GameplayContoller.Instance;
        gameplayContoller.Open();

    }

    //a call from GameplayController to restart the game
    public void RestartGame(){
        DeactivateRestartBtn();

        gameplayContoller.ChangeGameState(GameState.Restart);
    }

    void Update(){
        if(Input.GetKeyDown(KeyCode.Space)){
            if(playBtn.activeInHierarchy){
                PlayGame();
            }
            else if(restartBtn.activeInHierarchy){
                RestartGame();
            }
        }
    }

}
