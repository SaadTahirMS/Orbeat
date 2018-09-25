using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputController : MonoBehaviour
{
    public PlayerController player;
    private float screenCenterX;
    private bool gameStart = false;
    private string inputMethod;
    private bool tap = false;

    public void InputMethod(string method)
    {
        inputMethod = method;
    }

    private void Start()
    {
        // save the horizontal center of the screen
        screenCenterX = Screen.width * 0.5f;
        //print(screenCenterX);
        InputMethod("Buttons");
    }

    public void GameStart(bool flag){
        gameStart = flag;
    }

    float delay = 0.5f;
    private void Update()
    {
        if (gameStart)
        {
            //For Keyboard
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                if (Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    StartCoroutine(DelayDown());
                }
                player.MoveLeft(delay);
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                if (Input.GetKeyDown(KeyCode.RightArrow))
                {
                    StartCoroutine(DelayDown());
                }
                player.MoveRight(delay);
            }
            if(Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow)){
                StopAllCoroutines();
                delay = 0.5f;
            }

            switch (inputMethod)
            {
                case "Buttons":
                    if (Input.GetMouseButton(0))
                    {
                        if(Input.GetMouseButtonDown(0)){
                            StartCoroutine(DelayDown());
                        }
                        if (Input.mousePosition.x > screenCenterX)
                        {
                            player.MoveRight(delay);
                        }
                        else if (Input.mousePosition.x < screenCenterX)
                        {
                            player.MoveLeft(delay);
                        }
                    }
                    if(Input.GetMouseButtonUp(0)){
                        StopAllCoroutines();
                        delay = 0.5f;
                    }
                    break;
                case "Scroll V1":
                    //Constants.difficultyLevel = 50;
                    if (Input.GetMouseButton(0))
                    {
                        float x = Input.mousePosition.x - screenCenterX;
                        player.RotatePlayerV1(x / Screen.width); //value b/w -0.5 and 0.5
                    }

                    break;
                case "Scroll V2":
                    //Constants.difficultyLevel = 50;
                    if (Input.touchCount > 0)
                    {
                        Touch touch = Input.GetTouch(0);
                        // Handle finger movements based on touch phase.
                        if (touch.phase == TouchPhase.Moved)
                        {
                            player.RotatePlayerV2(touch.deltaPosition.x); //value b/w -0.5 and 0.5
                            Time.timeScale = 1f;
                        }
                        else if (touch.phase == TouchPhase.Ended)
                        {
                            player.RotatePlayerV2(0);
                            Time.timeScale = 0.1f;
                        }
                    }

                    break;

                case "Tap Switch":
                    //Constants.difficultyLevel = 100;
                    if (Input.GetMouseButtonDown(0))
                        tap = !tap;

                    if (tap)
                        player.MoveTapLeft();
                    else
                        player.MoveTapRight();
                    break;
            }
        }

    }

    IEnumerator DelayDown(){
        yield return new WaitForSeconds(0.1f);
        delay = 1f;
    }
}
