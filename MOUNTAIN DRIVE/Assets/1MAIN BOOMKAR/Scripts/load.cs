using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class load : MonoBehaviour {

    int car;
    public Image loadingbar;
    public GameObject loadingscreen;
    public GameObject levels,top,caranim;
    int n;

    public void changelevel( int m)
    {
        n = m;
        StartCoroutine(LoadSyncOperation());
        loadingscreen.SetActive(true);
        levels.SetActive(false);
        top.SetActive(false);
        caranim.SetActive(true);
    }
    IEnumerator LoadSyncOperation()
    {
        
        AsyncOperation gamelevel = SceneManager.LoadSceneAsync(n);
        while(gamelevel.progress <1 )
        {
            loadingbar.fillAmount = gamelevel.progress;
            yield return new WaitForEndOfFrame();
        }
    }
}
