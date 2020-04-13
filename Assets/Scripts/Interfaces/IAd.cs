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
    }
}