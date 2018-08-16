using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour {
    public Camera cam;
    Vector3 offset;
    private void Start()
    {
        offset = transform.position;
    }
    void Update () {

        transform.position = cam.transform.position + offset;
	}
}
