using UnityEngine;

public abstract class BaseGameSingleton<T>  : MonoBehaviour where T : MonoBehaviour
{
    protected static T instance = null;

    public static T Instance
    {
        get
        {
            if(instance == null)
            {
                instance =  GameObject.FindFirstObjectByType<T>();
            }
            return instance;
        }
    }
}
