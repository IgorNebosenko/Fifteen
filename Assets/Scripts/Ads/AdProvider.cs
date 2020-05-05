using System.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;
using Game.Interfaces;
using Game.Additional;

namespace Game.Ads
{
    /// <summary>
    /// Ad provider. Provides correct work of ads
    /// </summary>
    public class AdProvider : MonoBehaviour
    {
        /// <summary>
        /// List of ad providers
        /// </summary>
        private List<IAd> adList;

        private async void Start()
        {
            adList = new List<IAd>();
            adList.AddRange(gameObject.GetComponents<IAd>());

            bool internetAvailable = IsInthernetConnected();

            foreach (IAd ad in adList)
            {
                if (internetAvailable)
                    await Task.Run(ad.Load);
                else
                    ad.AdStatus = EAdStatus.NoEthernet;
            }
        }

        private void Update()
        {

        }

        /// <summary>
        /// Defines is Internet avalible
        /// </summary>
        /// <returns>True - if connected, false - if not connected</returns>
        private bool IsInthernetConnected()
        {
            return Application.internetReachability !=
                NetworkReachability.NotReachable;
        }
    }
}