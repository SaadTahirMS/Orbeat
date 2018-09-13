using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InnerOrbitController : MonoBehaviour {

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(GameplayContoller.Instance.gameState != GameState.Quit){
            switch (collision.gameObject.tag)
            {
                case "Player":
                    GameplayContoller.Instance.PlayerHitHurdle();
                    break;
                case "Wall":
                    GameplayContoller.Instance.HurdleHitWall();
                    break;
            }
        }

    }

}
