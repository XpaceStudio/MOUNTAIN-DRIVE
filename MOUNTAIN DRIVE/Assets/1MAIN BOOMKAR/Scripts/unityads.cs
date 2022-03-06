using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.SceneManagement;


public class unityads : MonoBehaviour {

    string gameId = "3434051";
    bool testMode = false;
    int timer;
    string placementId = "banner";

    public void Start()
    {
        Advertisement.Initialize(gameId, testMode);
        if (SceneManager.GetActiveScene().buildIndex != 0)
        {
            StartCoroutine(ShowBannerWhenReady());
        }
        else
        {
            bannerad();
        }
    }
    IEnumerator ShowBannerWhenReady()
    {
        while (!Advertisement.IsReady(placementId))
        {
            yield return new WaitForSeconds(0.5f);
        }
        Advertisement.Banner.SetPosition(BannerPosition.TOP_CENTER);
        Advertisement.Banner.Show(placementId);

    }
    public void bannerad()
    {
        Advertisement.Banner.Hide(false);
        
    }
    public void showad()
    {
        if (SceneManager.GetActiveScene().buildIndex != 0)
        {
                Advertisement.Show();           
        }

    }

    public void ShowRewardedAd_revive()
    {
        if (Advertisement.IsReady("rewardedVideo"))
        {
            var options = new ShowOptions { resultCallback = HandleShowResult };
            Advertisement.Show("rewardedVideo", options);
        }
    }

    private void HandleShowResult(ShowResult result)
    {
        switch (result)
        {
            case ShowResult.Finished:

                FindObjectOfType<playercon>().revived();
                break;
            case ShowResult.Skipped:
                SSTools.ShowMessage("The ad was skipped", SSTools.Position.bottom, SSTools.Time.oneSecond);
                break;
            case ShowResult.Failed:
                SSTools.ShowMessage("Internet not available", SSTools.Position.bottom, SSTools.Time.oneSecond);
                break;
        }
    }

    // Show an ad:
    public void ShowRewardedAd_coindouble()
    {
        if (Advertisement.IsReady("rewardedVideo"))
        {
            var options = new ShowOptions { resultCallback = HandleShowResultcoin };
            Advertisement.Show("rewardedVideo", options);
        }
    }

    private void HandleShowResultcoin(ShowResult result)
    {
        switch (result)
        {
            case ShowResult.Finished:
                doublecoin();
                break;
            case ShowResult.Skipped:
                SSTools.ShowMessage("The ad was skipped", SSTools.Position.bottom, SSTools.Time.oneSecond);
                break;
            case ShowResult.Failed:
                SSTools.ShowMessage("Internet not available", SSTools.Position.bottom, SSTools.Time.oneSecond);
                break;
        }
    }
    public void doublecoin()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            int money = PlayerPrefs.GetInt("gold", 0);
            money += 100;
            PlayerPrefs.SetInt("gold", money);
            FindObjectOfType<shop>().addshown();
        }
        else
        {
            FindObjectOfType<playercon>().doublecoin();
        }
    }

   

}
