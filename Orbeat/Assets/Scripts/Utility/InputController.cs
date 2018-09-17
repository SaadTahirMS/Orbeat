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
        print(screenCenterX);
    }

    public void GameStart(bool flag){
        gameStart = flag;
    }

    private void Update()
    {
        if (gameStart)
        {
            //For Keyboard
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                player.MoveLeft();
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                player.MoveRight();
            }

            switch (inputMethod)
            {
                case "Buttons":
                    Constants.difficultyLevel = 50;
                    if (Input.GetMouseButton(0))
                    {
                        if (Input.mousePosition.x > screenCenterX)
                        {
                            player.MoveRight();
                        }
                        else if (Input.mousePosition.x < screenCenterX)
                        {
                            player.MoveLeft();
                        }
                    }

                    break;
                case "Scroll V1":
                    Constants.difficultyLevel = 50;
                    if (Input.GetMouseButton(0))
                    {
                        float x = Input.mousePosition.x - screenCenterX;
                        player.RotatePlayerV1(x / Screen.width); //value b/w -0.5 and 0.5
                    }

                    break;
                case "Scroll V2":
                    Constants.difficultyLevel = 50;
                    if (Input.touchCount > 0)
                    {
                        Touch touch = Input.GetTouch(0);
                        // Handle finger movements based on touch phase.
                        if (touch.phase == TouchPhase.Moved)
                        {
                            player.RotatePlayerV2(touch.deltaPosition.x); //value b/w -0.5 and 0.5
                                                                          //playerMovementRefs.ropeTransform.Rotate (Vector3.forward * Time.deltaTime * deltaPostionX * movementFactor);
                        }
                        else if (touch.phase == TouchPhase.Ended)
                        {
                            player.RotatePlayerV2(0);
                        }
                    }

                    break;

                case "Tap Switch":
                    Constants.difficultyLevel = 100;
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
}
