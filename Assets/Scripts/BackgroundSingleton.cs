using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundSingleton : MonoBehaviour
{
    static BackgroundSingleton instance;
    void Awake()
    {
        ManageSingleton();
    }

    void ManageSingleton()
    {
        if (instance != null)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
}
