using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Scaler : MonoBehaviour {

    public List<Transform> orbits;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            DoScale();
        }
        if (orbits[0].localScale.x <= 0.5f)
        {
            orbits[0].localScale = Vector3.one;
        }
        print("Orbit: " + 0 + " " + orbits[0].localScale);
    }

    private void DoScale(){
        
        for (int i = 0; i < orbits.Count; i++)
        {
            orbits[i].DOScale(0, 10f);

        }
    }

}
