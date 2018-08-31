using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewPlayerController : MonoBehaviour {

    public Transform myParent;
    public Vector3 initialPosition = new Vector3(175f, 0f, 0f);
    //Shooting
    public float shotSpeed;
    bool shotFlag = false;
    //public Rotate rotate;

    //private void Start()
    //{
    //    rotate = GetComponent<Rotate>();
    //}

    private void Update()
    {
        if (shotFlag)
        {
            CollisionWithBoundary();
            transform.Translate(Vector3.right * Time.deltaTime * shotSpeed);
        }

        if (Input.GetMouseButtonDown(0) || Input.GetKey(KeyCode.Space))
        {
            InitiateShot();
        }
    }

    void InitiateShot()
    {
        transform.SetParent(myParent.parent);
        transform.SetAsLastSibling();
        //rotate.StopRotate();
        shotFlag = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Target":
                shotFlag = false;
                ResetParent();
                ResetTransform();
                //rotate.DoRotate();
                break;
        }
    }

    private void ResetParent()
    {
        transform.SetParent(myParent);
        transform.SetSiblingIndex(1);
    }

    private void ResetTransform()
    {
        transform.localPosition = initialPosition;
        transform.localRotation = Quaternion.Euler(Vector3.zero);

    }

    private void CollisionWithBoundary()
    {
        Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);
        if (pos.x < 0 || pos.x > 1 || pos.y < 0 || pos.y > 1)
        {
            //gameObject.SetActive(false);
            ResetParent();
            ResetTransform();
            shotFlag = false;
            //rotate.DoRotate();
            //gameObject.SetActive(true);
        }
    }


}
