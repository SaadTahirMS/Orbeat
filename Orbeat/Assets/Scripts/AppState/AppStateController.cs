using UnityEngine;

public class AppStateController : MonoBehaviour {

	public GameRefs gameRefs;

    private void Awake()
    {
		LoadingController.Instance.Initialize (gameRefs);
    }
}
