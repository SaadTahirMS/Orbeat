using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputController : MonoBehaviour
{
    public PlayerController player;
    private float screenCenterX;
    private bool gameStart = false;
    private string inputMethod = "Scroll";

    public void InputMethod(string method)
    {
        inputMethod = method;
        print(inputMethod);
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
            if (inputMethod == "Buttons")
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
            }
            else if (inputMethod == "Scroll")
            {
                if (Input.GetMouseButton(0))
                {
                    float x = Input.mousePosition.x - screenCenterX;
                    player.RotatePlayer(x / Screen.width); //value b/w -0.5 and 0.5
                }
            }
        }

    }
}
