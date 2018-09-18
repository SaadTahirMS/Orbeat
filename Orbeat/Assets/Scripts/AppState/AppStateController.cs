using UnityEngine;

public class AppStateController : MonoBehaviour {

	public ViewRefs viewRefs;

    private void Awake()
    {
		LoadingController.Instance.Initialize (viewRefs);
    }
}
