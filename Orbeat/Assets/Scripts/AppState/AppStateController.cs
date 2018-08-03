using UnityEngine;

public class AppStateController : MonoBehaviour {

    LoadingController loadingController;
    private void Start()
    {
        loadingController = LoadingController.Instance;
        loadingController.Open();
    }
}
