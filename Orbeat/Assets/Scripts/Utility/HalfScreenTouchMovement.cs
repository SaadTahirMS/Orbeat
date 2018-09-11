using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HalfScreenTouchMovement : MonoBehaviour
{
    public PlayerController player;
    private float screenCenterX;
    private bool gameStart = false;

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

        //// if there are any touches currently
        //if (Input.touchCount > 0)
        //{
        //    // get the first one
        //    Touch firstTouch = Input.GetTouch(0);

        //    // if it began this frame
        //    if (firstTouch.phase == TouchPhase.Began)
        //    {
        //        if (firstTouch.position.x > screenCenterX)
        //        {
        //            // if the touch position is to the right of center
        //            // move right
        //            Debug.Log("Move Right");
        //            player.MoveRight();
        //        }
        //        else if (firstTouch.position.x < screenCenterX)
        //        {
        //            // if the touch position is to the left of center
        //            // move left
        //            Debug.Log("Move Left");
        //            player.MoveLeft();
        //        }
        //    }
        //}
    }
}
