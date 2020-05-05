using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using Game.Interfaces;
using Game.Additional;

namespace Game.Ads
{
    /// <summary>
    /// Class which init and works with Unity Ads
    /// </summary>
    public class UnityAd : MonoBehaviour, IAd
    {
        #region Global properties
        public EAdStatus AdStatus { get; set; }
        public bool IsNowActive { get; set; }
        public ETypesAd AllowTypesAds { get; set; }
        public ETypesAd CurrentAd { get; set; }
        #endregion

        private void Start()
        {
            
        }

        public async void Load()
        {

        }

        public void Close()
        {
            
        }

        public void HideBanner()
        {
            
        }

        public void ShowBanner()
        {
            
        }

        public void ShowImage()
        {
            
        }

        public void ShowRevenueVideo()
        {
            
        }

        public void ShowVideo()
        {
            
        }
    }
}