using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingViewController {

    LoadingReference loadingReference;

    public void OpenUI(){
        GameObject loadingRefObj = (GameObject)Object.Instantiate(Resources.Load("Path"));
        loadingReference = loadingRefObj.GetComponent<LoadingReference>();
    }


}
