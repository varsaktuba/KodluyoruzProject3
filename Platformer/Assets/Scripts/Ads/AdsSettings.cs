#define TEST
using UnityEngine;
using UnityEngine.Advertisements;

namespace Ads
{
    public class AdsSettings : MonoBehaviour, IUnityAdsListener
    {
        //Google: 3864319
        //Apple: 3864318

#if UNITY_IOS
    private string _gameID ="3864318";
#elif UNITY_ANDROID
    private string _gameID ="3864319";
#else
        private string _gameID = "1234567";
#endif

#if TEST
        private bool _isTest = true;
#else
    private bool _isTest = false;
#endif

       
       
        private void Start()
        {
            Advertisement.Initialize(_gameID, _isTest);
            Advertisement.AddListener(this);
        }
        private void OnEnable()
        {
            EventManager.PlayAdsEvent += ShowsAds;
        }
        private void OnDisable()
        {
            EventManager.PlayAdsEvent -= ShowsAds;
        }
        /* private void Update()
         {
             if (Input.GetKeyUp(KeyCode.N))
             {
                 ShowInterstitialAd();
             }
         }
         */
        private void ShowsAds(AdsPlacementType adsType)
        {
            switch (adsType)
            {
                case AdsPlacementType.rewardedVideo:
                    if (Advertisement.IsReady(adsType.ToString()))
                    {
                        Advertisement.Show(adsType.ToString());
                    }
                    else
                    {
                        Debug.LogWarning("Dont have any rewarded ads in the pool");
                    }
                    break;
                case AdsPlacementType.bannerPlacement:
                    Advertisement.Banner.SetPosition(BannerPosition.TOP_CENTER);
                    Advertisement.Banner.Show(adsType.ToString());
                    break;
                case AdsPlacementType.intersitialAds:
                    if (Advertisement.IsReady())
                    {
                        Advertisement.Show();
                    }
                    else
                    {
                        Debug.LogWarning("havuz boş");
                    }
                    break;
            }
        }

        public void OnUnityAdsReady(string placementId)
        {
           
        }

        public void OnUnityAdsDidError(string message)
        {
            
        }

        public void OnUnityAdsDidStart(string placementId)
        {
            
        }

        public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
        {
            switch (showResult)
            {
                case ShowResult.Failed:
                    //Don't give reward
                    break;
                case ShowResult.Skipped:
                    //Don't give reward
                    break;
                case ShowResult.Finished:
                    Debug.Log("REWARD TO PLAYER, MONEY TO ME");
                    break;
            }
        }
    }
}

