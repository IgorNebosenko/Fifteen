using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;

namespace Game.Ads
{
    /// <summary>
    /// Class which init and works with AdMob
    /// </summary>
    public class AdMob : MonoBehaviour
    {
        /// <summary>
        /// View of banner
        /// </summary>
        BannerView bannerBottom;

        /// <summary>
        /// Ad ID for android
        /// </summary>
        const string androidID = "ca-app-pub-1130867258699163~6520774498";
        /// <summary>
        /// Ad ID for IOS
        /// </summary>
        const string iosID = "ca-app-pub-1130867258699163~6520774498";

        /// <summary>
        /// Banner ID for android
        /// </summary>
        const string androidBannerID = "ca-app-pub-1130867258699163/8559973847";
        /// <summary>
        /// Banner ID for IOS
        /// </summary>
        const string iosBannerID = "ca-app-pub-1130867258699163/8559973847";

        public bool EnableShow { get; set; }

        void Start()
        {
#if UNITY_ANDROID
            MobileAds.Initialize(androidID);
#elif UNITY_IPHONE
            MobileAds.Initialize(iosID);
#else
            Debug.LogError("Ads for platform are not supports!");
#endif

            RequestBanner();
        }

        void RequestBanner()
        {
            bannerBottom = new BannerView(androidBannerID, AdSize.Banner, AdPosition.Bottom);
            AdRequest request = new AdRequest.Builder().Build();
            bannerBottom.LoadAd(request);

            bannerBottom.OnAdLoaded += BannerBottom_OnAdLoaded;
            bannerBottom.OnAdFailedToLoad += BannerBottom_OnAdFailedToLoad;
            bannerBottom.OnAdLeavingApplication += BannerBottom_OnAdLeavingApplication;

            bannerBottom.Show();
        }

        private void BannerBottom_OnAdLeavingApplication(object sender, System.EventArgs e)
        {

        }

        private void BannerBottom_OnAdLoaded(object sender, System.EventArgs e)
        {

        }

        /// <summary>
        /// If error load - try Unity Ads
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">AdFailedToLoadEventArgs</param>
        void BannerBottom_OnAdFailedToLoad(object sender, AdFailedToLoadEventArgs e)
        {
            throw new System.NotImplementedException();
        }
    }
}