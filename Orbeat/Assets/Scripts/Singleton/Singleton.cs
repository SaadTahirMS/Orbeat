using UnityEngine;
// <summary>
// Be aware this will not prevent a non singleton constructor
//   such as `T myT = new T();`
// To prevent that, add `protected T () {}` to your singleton class.
// 
// As a note, this is made as MonoBehaviour because we need Coroutines.
// </summary>
public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance;
    
    private static object _lock = new object();
    
    
    public static T Instance
    {
        get
        {   
            lock(_lock)
            {
                if (_instance == null)
                {
                    // check if there is at least one instance in the scene 
                    _instance = (T) FindObjectOfType(typeof(T));
                    // if there is one, also check if there are multiple types which
                    // is not allowed
                    if ( _instance != null && FindObjectsOfType(typeof(T)).Length > 1 )
                    {
                        return null;
                    }
                    
                    if (_instance == null)
                    {
                        GameObject singleton = new GameObject();
                        singleton.hideFlags = HideFlags.HideInHierarchy;
                        _instance = singleton.AddComponent<T>();
                        singleton.name = "(singleton) "+ typeof(T).ToString();
                    } else {
                    }
                }
                
                return _instance;
            }
        }
    }
}