using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyTargetController : MonoBehaviour {

    [SerializeField]
    private int id;

    public int Id
    {
        get
        {
            return id;
        }

        set
        {
            id = value;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Player":
                GameplayContoller.Instance.PlayerCollidedWithTarget(id);
                break;
        }
    }
}
