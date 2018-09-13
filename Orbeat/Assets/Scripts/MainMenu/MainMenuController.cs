using UnityEngine;
using UnityEngine.UI;

public class MainMenuController : Singleton<MainMenuController>, IController {

    public GameplayRefs gameplayRefs;
    GameplayContoller gameplayContoller;

    public GameObject playBtn;
    public GameObject restartBtn;
    public GameObject settings;

    //Game Settings
    public Text inputMethod;
    public InputField minHurdleFillAmount;
    public InputField maxHurdleFillAmount;
    public InputField cameraOffset;
    public InputField hurdlesDistance;
    public InputField scaleSpeed;
    public InputField playerRotationSpeed;
    public InputField playerScrollRotationSpeed;
    public InputField minRotateSpeed;
    public InputField maxRotateSpeed;
    public InputField rotationOffset;
    public Text playerCollision;
    public Text canRotateOrbits;

    public void Open(){
        ActivatePlayBtn();
        SoundController.Instance.SetPitch(0.9f,true);
        SoundController.Instance.SetVolume(0.75f);
        InitializeBeat();
        settings.SetActive(true);
        DefaultSettings();
    }

    private void DefaultSettings(){
        InputMethod(gameplayRefs.inputMethod);
        minHurdleFillAmount.text = gameplayRefs.minHurdleFillAmount.ToString();
        maxHurdleFillAmount.text = gameplayRefs.maxHurdleFillAmount.ToString();
        cameraOffset.text = gameplayRefs.cameraOffset.ToString();
        hurdlesDistance.text = gameplayRefs.hurdlesDistance.ToString();
        scaleSpeed.text = gameplayRefs.scaleSpeed.ToString();
        playerRotationSpeed.text = gameplayRefs.playerRotationSpeed.ToString();
        playerScrollRotationSpeed.text = gameplayRefs.playerScrollRotationSpeed.ToString();
        minRotateSpeed.text = gameplayRefs.minRotateSpeed.ToString();
        maxRotateSpeed.text = gameplayRefs.maxRotateSpeed.ToString();
        rotationOffset.text = gameplayRefs.rotationOffset.ToString();
        playerCollision.text = gameplayRefs.playerCollision.ToString();
        canRotateOrbits.text = gameplayRefs.canRotateOrbits.ToString();
    }

    public void GameSettings(){
        gameplayRefs.minHurdleFillAmount = float.Parse(minHurdleFillAmount.text);
        gameplayRefs.maxHurdleFillAmount = float.Parse(maxHurdleFillAmount.text);
        gameplayRefs.cameraOffset = float.Parse(cameraOffset.text);
        gameplayRefs.hurdlesDistance = float.Parse(hurdlesDistance.text);
        gameplayRefs.scaleSpeed = float.Parse(scaleSpeed.text);
        gameplayRefs.playerRotationSpeed = float.Parse(playerRotationSpeed.text);
        gameplayRefs.playerScrollRotationSpeed = float.Parse(playerScrollRotationSpeed.text);
        gameplayRefs.minRotateSpeed = float.Parse(minRotateSpeed.text);
        gameplayRefs.maxRotateSpeed = float.Parse(maxRotateSpeed.text);
        gameplayRefs.rotationOffset = float.Parse(rotationOffset.text);
    }

    public void PlayerCollisions(bool flag){
        gameplayRefs.playerCollision = flag;
        playerCollision.text = gameplayRefs.playerCollision.ToString();
    }

    public void CanRotate(bool flag)
    {
        gameplayRefs.canRotateOrbits = flag;
        canRotateOrbits.text = gameplayRefs.canRotateOrbits.ToString();
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
        gameplayRefs.inputMethod = method;
        inputMethod.text = method;
    }

    // this will make a call to GameplayController to start the gameplay
    public void PlayGame(){
        DeactivatePlayBtn();
        SoundController.Instance.SetPitch(1f,true);
        SoundController.Instance.SetVolume(1f);
        gameplayContoller = GameplayContoller.Instance;
        gameplayContoller.Open();
        settings.SetActive(false);
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
