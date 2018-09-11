using UnityEngine;
using UnityEngine.UI;

public class MainMenuController : Singleton<MainMenuController>, IController {

    public GameplayRefs gameplayRefs;
    GameplayContoller gameplayContoller;

    public GameObject playBtn;
    public GameObject restartBtn;
    public GameObject inputBtn1;
    public GameObject inputBtn2;
    public Text inputText;

    public void Open(){
        ActivatePlayBtn();
        SoundController.Instance.SetPitch(.5f,true);
        SoundController.Instance.SetVolume(1f);
        InitializeBeat();
    }

    private void InitializeBeat()
    {
        gameplayRefs.loudness.Initialize();
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


    public void InputMethod(string method)
    {
        gameplayRefs.inputController.InputMethod(method);
        inputText.text = method;
    }

    // this will make a call to GameplayController to start the gameplay
    public void PlayGame(){
        DeactivatePlayBtn();
        SoundController.Instance.SetPitch(1f,true);
        SoundController.Instance.SetVolume(1f);
        gameplayContoller = GameplayContoller.Instance;
        gameplayContoller.Open();
        inputBtn1.SetActive(false);
        inputBtn2.SetActive(false);
        inputText.gameObject.SetActive(false);
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
