using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;

public class Reklam : MonoBehaviour {

    InterstitialAd interstitial;

    static Reklam reklam;

    void Start ()
    {
        if(reklam==null)
        {
            DontDestroyOnLoad(gameObject);
            reklam = this;
            #if UNITY_ANDROID
                        string appId = "ca - app - pub - 2870930756867982~4544661851";
            #elif UNITY_IPHONE
                                string appId = "ca-app-pub-3940256099942544~1458002511";
            #else
                                string appId = "unexpected_platform";
            #endif

            MobileAds.Initialize(appId);
            //------------------//
            #if UNITY_ANDROID
                        string adUnitId = "ca-app-pub-2870930756867982/1343783448";
            #elif UNITY_IPHONE
                            string adUnitId = "ca-app-pub-3940256099942544/4411468910";
            #else
                            string adUnitId = "unexpected_platform";
            #endif

            interstitial = new InterstitialAd(adUnitId);
            //------------------//
            AdRequest request = new AdRequest.Builder().Build();

            interstitial.LoadAd(request);
        }
        else
        {
            Destroy(gameObject);
        }

        
    }


    public void ReklamGoster()
    {
        if (interstitial.IsLoaded())
        {
            interstitial.Show();
        }
        reklam = null;
        Destroy(gameObject);
        
    }
}
