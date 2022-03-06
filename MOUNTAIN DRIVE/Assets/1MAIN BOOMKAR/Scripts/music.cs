using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class music : MonoBehaviour {

    private static music instance;


    private void Awake()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        if (instance!=null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(transform.gameObject);
        }

    }
    private void Update()
    {

        if (SceneManager.GetActiveScene().buildIndex==0 || SceneManager.GetActiveScene().buildIndex == 1)
        {
            return;
        }
        else
        {
            Destroy(gameObject);
        }
    }

}
