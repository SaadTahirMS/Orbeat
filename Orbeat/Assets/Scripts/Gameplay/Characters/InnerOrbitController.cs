using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InnerOrbitController : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D collision)
    {
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
