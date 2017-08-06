using UnityEngine;
using System.Collections;
/*
 单件继承类
     */
public class Sington<T> : MonoBehaviour where T : Sington<T>
{

    private static T _instance;

    public static T instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType(typeof(T)) as T;
                if (_instance == null)
                {
                    _instance = new GameObject("Singleton of " + typeof(T).ToString(), typeof(T)).GetComponent<T>();
                    _instance.Init();
                }

            }
            return _instance;
        }
    }
    public virtual void Init() { }
}
