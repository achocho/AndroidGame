using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class AdManager : MonoBehaviour, IUnityAdsListener
{

    private string PlayStoreId = "3936741";
    private string AppStoreId = "3929996";
    private string VideoAd = "video";
    public PlayerController player;
    private string RewardedAd = "rewardedVideo";
    public bool IsTargetPlayStore;
    public bool IsTestAd;
    public GameObject ReviveButton;

    public int RevivedTimes;
    void Start()
    {
        RevivedTimes = PlayerPrefs.GetInt("Revived");
        Advertisement.AddListener(this);
        InitaliazedAd();
    }
    private void Update() 
    {
        if (RevivedTimes >= 1)
        {
            ReviveButton.SetActive(false);
        }
        else 
        {
            ReviveButton.SetActive(true);
        }
    }
    private void InitaliazedAd()
    {

        if (IsTargetPlayStore)
        {
            Advertisement.Initialize(PlayStoreId, IsTestAd);
            return;
        }
        Advertisement.Initialize(AppStoreId, IsTestAd);
    }

    public void PlayInterstitialAd()
    {
        if (!Advertisement.IsReady(VideoAd))
        {
            return;
        }
        Advertisement.Show(VideoAd);
    }
    public void PlayRewardedVideoAd()
    {

        if (!Advertisement.IsReady(RewardedAd))
        {
            return;
        }
        Advertisement.Show(RewardedAd);
    }

    public void OnUnityAdsReady(string placementId)
    {
        // throw new System.NotImplementedException();
    }

    public void OnUnityAdsDidError(string message)
    {
        // throw new System.NotImplementedException();
    }

    public void OnUnityAdsDidStart(string placementId)
    {
      
    }

    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {

        switch (showResult)
        {

            case ShowResult.Failed:

                break;
            case ShowResult.Skipped:

                break;
            case ShowResult.Finished:

                if (placementId == RewardedAd)
                {
               
                    RevivedTimes++;
                    PlayerPrefs.SetInt("Revived",RevivedTimes);
                    PlayerPrefs.SetInt("Score",player.Score);
                    SceneManager.LoadScene("Level");
                    Debug.Log("Reward");
                 
                }
                if (placementId == VideoAd)
                {
                    Debug.Log("Finished");
                }
                break;
        }
    }

}