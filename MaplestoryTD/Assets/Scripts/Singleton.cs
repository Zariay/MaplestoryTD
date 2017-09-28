using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Singleton<T> : MonoBehaviour where T: MonoBehaviour
{
    private static T inst;

    public static T Instance
    {
        get
        {
            if(inst == null)
            {
                inst = FindObjectOfType<T>();
            }

            return inst;
        }
    }
}
