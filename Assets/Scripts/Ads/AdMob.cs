using UnityEngine;
using GoogleMobileAds.Api;
using Game.Interfaces;
using Game.Additional;
using System.Threading.Tasks;

namespace Game.Ads
{
    /// <summary>
    /// Class which init and works with AdMob
    /// </summary>
    public class AdMob : MonoBehaviour, IAd
    {
        /// <summary>
        /// View of banner
        /// </summary>
        BannerView bannerBottom;
        /// <summary>
        /// Image ad
        /// </summary>
        InterstitialAd imageAd;
        /// <summary>
        /// Video ad
        /// </summary>
        InterstitialAd videoAd;
        /// <summary>
        /// Reward video ad
        /// </summary>
        RewardedAd rewardedVideoAd;

        #region Ads keys
        /// <summary>
        /// Ad ID for Android
        /// </summary>
        const string androidID = "ca-app-pub-1130867258699163~6520774498";
        /// <summary>
        /// Ad ID for IOS
        /// </summary>
        const string iosID = "ca-app-pub-1130867258699163~2961954811";
        #endregion
        #region Banner keys
        /// <summary>
        /// Banner ID for Android
        /// </summary>
        const string androidBannerID = "ca-app-pub-1130867258699163/8559973847";
        /// <summary>
        /// Banner ID for IOS
        /// </summary>
        const string iosBannerID = "ca-app-pub-1130867258699163/2578811432";
        #endregion
        #region Image keys
        /// <summary>
        /// Image ID for Android
        /// </summary>
        const string androidImageID = "ca-app-pub-1130867258699163/3196036338";
        /// <summary>
        /// Image ID for IOS
        /// </summary>
        const string iosImageID = "ca-app-pub-1130867258699163/1927186693";
        #endregion
        #region Video keys
        /// <summary>
        /// Video ID for Android
        /// </summary>
        const string androidVideoID = "ca-app-pub-1130867258699163/8712974942";
        /// <summary>
        /// Video ID for IOS
        /// </summary>
        const string iosVideoID = "ca-app-pub-1130867258699163/8109451665";
        #endregion
        #region Reward video for Android
        /// <summary>
        /// Reward video ID for Android
        /// </summary>
        const string androidRewardVideoID = "ca-app-pub-1130867258699163/4229916404";
        /// <summary>
        /// Reward video ID for IOS
        /// </summary>
        const string iosRewardVideoID = "ca-app-pub-1130867258699163/3787063271";
        #endregion

        #region Global properties
        /// <summary>
        /// Status of ad
        /// </summary>
        public EAdStatus AdStatus { get; set; }
        /// <summary>
        /// Is now ad active
        /// </summary>
        public bool IsNowActive { get; set; }
        /// <summary>
        /// Types of allowed ads. This field may used bitsets
        /// </summary>
        public ETypesAd AllowTypesAds { get; set; }
        /// <summary>
        /// Defines what current ad is shows... or not shows...
        /// </summary>
        public ETypesAd CurrentAd { get; set; }
        #endregion
        #region Core methods
        void Start()
        {
            AdStatus = EAdStatus.Loads;
            IsNowActive = false;
            AllowTypesAds = ETypesAd.Banner |
                ETypesAd.Image |
                ETypesAd.Video |
                ETypesAd.VideoGreetings;
            CurrentAd = ETypesAd.None;

#if UNITY_ANDROID
            MobileAds.Initialize(androidID);
#elif UNITY_IPHONE
            MobileAds.Initialize(iosID);
#else
            Debug.LogError("Ads for current platform are not supports!");
#endif
        }

        /// <summary>
        /// Method of load ad. This method is async
        /// </summary>
        public async void Load()
        {
#if UNITY_ANDROID
            bannerBottom = new BannerView(androidBannerID, AdSize.Banner, AdPosition.Bottom);
            imageAd = new InterstitialAd(androidImageID);
            videoAd = new InterstitialAd(androidVideoID);
            rewardedVideoAd = new RewardedAd(androidRewardVideoID);
#elif UNITY_IPHONE
            bannerBottom = new BannerView(iosBannerID, AdSize.Banner, AdPosition.Bottom);
            imageAd = new InterstitialAd(iosImageID);
            videoAd = new InterstitialAd(iosVideoID);
            rewardedVideoAd = new RewardedAd(iosRewardVideoID);
#else
            Debug.LogError("Platform not support!");
            return;
#endif
            AdRequest request = new AdRequest.Builder().Build();

            await Task.Run(() => bannerBottom.LoadAd(request));
            await Task.Run(() => imageAd.LoadAd(request));
            await Task.Run(() => videoAd.LoadAd(request));
            await Task.Run(() => rewardedVideoAd.LoadAd(request));
            
            AddEvents();
        }
        /// <summary>
        /// Closes Ad
        /// </summary>
        public void Close()
        {
            if (bannerBottom != null)
                bannerBottom.Destroy();
            if (imageAd != null)
                imageAd.Destroy();
            if (videoAd != null)
                videoAd.Destroy();
        }
        #endregion
        #region Methods for work with ads
        /// <summary>
        /// Method of show banner
        /// </summary>
        public void ShowBanner()
        {
            if (bannerBottom != null)
                bannerBottom.Show();
        }
        /// <summary>
        /// Method for hide banner
        /// </summary>
        public void HideBanner()
        {
            if (bannerBottom != null)
                bannerBottom.Hide();
        }
        /// <summary>
        /// Method for show ad as image
        /// </summary>
        public void ShowImage()
        {
            if (imageAd != null)
                imageAd.Show();
        }
        /// <summary>
        /// Method of show ad as video
        /// </summary>
        public void ShowVideo()
        {
            if (videoAd != null)
                videoAd.Show();
        }
        /// <summary>
        /// Method of show ad as revenue video
        /// </summary>
        public void ShowRevenueVideo()
        {
            if (rewardedVideoAd != null)
                rewardedVideoAd.Show();
        }
        #endregion
        #region Events
        /// <summary>
        /// Method of add events
        /// </summary>
        void AddEvents()
        {
            bannerBottom.OnAdLoaded += UniversalLoadSucssesEvent;
            bannerBottom.OnAdFailedToLoad += UniversalLoadErrorEvent;

            imageAd.OnAdLoaded += UniversalLoadSucssesEvent;
            imageAd.OnAdFailedToLoad += UniversalLoadErrorEvent;

            videoAd.OnAdLoaded += UniversalLoadSucssesEvent;
            videoAd.OnAdFailedToLoad += UniversalLoadErrorEvent;

            rewardedVideoAd.OnAdLoaded += UniversalLoadSucssesEvent;
            rewardedVideoAd.OnAdFailedToLoad += UniversalLoadErrorEvent;
        }

        /// <summary>
        /// Universal event for sucsses load
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Event arguments</param>
        void UniversalLoadSucssesEvent(object sender, System.EventArgs e)
        {
            if (AdStatus == EAdStatus.Loads)
                AdStatus = EAdStatus.Loaded;
        }
        /// <summary>
        /// Universal event for error load
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Event arguments</param>
        void UniversalLoadErrorEvent(object sender, AdFailedToLoadEventArgs e)
        {
            if (AdStatus != EAdStatus.NoEthernet)
                AdStatus = EAdStatus.Failed;
        }
        #endregion
    }
}