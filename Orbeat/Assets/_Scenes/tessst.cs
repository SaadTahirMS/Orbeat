using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class tessst : MonoBehaviour {

    public float time;
	// Use this for initialization
	void Start () 
    {
        this.transform.DOScale(Vector3.one, time).SetEase(Ease.Linear).Play();
		
	}
}
