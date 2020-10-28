
using UnityEngine;
using UnityEngine.Advertisements;

namespace Ads
{
    public class RewardedAdsController : MonoBehaviour, IUnityAdsListener
    {
        /*private string _gameID = "1234567";
        private AdsPlacementType _placement = AdsPlacementType.rewardedVideo;

        void Start()
        {
            Advertisement.AddListener(this);
            Advertisement.Initialize(_gameID, true);
        }
        private void Update()
        {
            if (Input.GetKeyUp(KeyCode.L))
            {
                ShowRewardedVideo();
            }
        }

        public void ShowRewardedVideo()
        {
            if (Advertisement.IsReady(_placement.ToString()))
            {
                Advertisement.Show(_placement.ToString());
            }
            else
            {
                Debug.LogWarning("Dont have any rewarded ads in the pool");
            }
        }
        */
        public void OnUnityAdsDidError(string message)
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

        public void OnUnityAdsDidStart(string placementId)
        {

        }

        public void OnUnityAdsReady(string placementId)
        {

        }
        public void OnDisable()
        {
            Advertisement.RemoveListener(this);
        }


    }
}