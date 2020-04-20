using Game.Additional;

namespace Game.Interfaces
{
    /// <summary>
    /// Interface for ads
    /// </summary>
    public interface IAd
    {
        /// <summary>
        /// Status of ad
        /// </summary>
        EAdStatus AdStatus { get; set; }
        /// <summary>
        /// Is now ad active
        /// </summary>
        bool IsNowActive { get; set; }
        /// <summary>
        /// Types of allowed ads. This field may used bitsets
        /// </summary>
        ETypesAd AllowTypesAds { get; set; }
        /// <summary>
        /// Defines what current ad is shows... or not shows...
        /// </summary>
        ETypesAd CurrentAd { get; set; }

        /// <summary>
        /// Method of load ad. Must be Async
        /// </summary>
        void Load();
        /// <summary>
        /// Closes Ad
        /// </summary>
        void Close();
        /// <summary>
        /// Method for display banner, if it support
        /// </summary>
        void ShowBanner();
        /// <summary>
        /// Method for hide banner
        /// </summary>
        void HideBanner();
        /// <summary>
        /// Method of display ad image, if it support
        /// </summary>
        void ShowImage();
        /// <summary>
        /// Method of display ad video, if it support
        /// </summary>
        void ShowVideo();
        /// <summary>
        /// Method of display revenue video, if it support
        /// </summary>
        void ShowRevenueVideo();
    }
}