using UnityEngine;

public class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance;
    private static object lockObject = new object();
    private static bool isCreate = false;
    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                lock (lockObject)
                {
                    instance = Transform.FindObjectOfType<T>();
                    if (instance == null)
                    {
                        var go = new GameObject(typeof(T).Name);
                        GameObject.DontDestroyOnLoad(go);
                        instance = go.AddComponent<T>();
                    }
                    else
                    {
                        GameObject.DontDestroyOnLoad(instance);
                    }
                }
                (instance as MonoSingleton<T>).Initialize();
                isCreate = true;
            }
            return instance;
        }
    }

    public static bool IsCreate => isCreate;

    protected virtual void Initialize()
    {

    }
}