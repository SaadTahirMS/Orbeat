﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyID : MonoBehaviour {

    //[SerializeField]
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
}
