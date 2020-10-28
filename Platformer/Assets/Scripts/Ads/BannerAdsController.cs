using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class BannerAdsController : MonoBehaviour
{
    private string _gameID = "1234567";
    private AdsPlacementType _placement = AdsPlacementType.bannerPlacement;

    void Start()
    {
        Advertisement.Initialize(_gameID, true);
    }


    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.L))
        {
            ShowBannerAds();
        }
    }

    private void ShowBannerAds()
    {

        Advertisement.Banner.SetPosition(BannerPosition.TOP_CENTER);
        Advertisement.Banner.Show(_placement.ToString());
        
    }
}
