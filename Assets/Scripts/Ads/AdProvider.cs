using System.Collections.Generic;
using UnityEngine;
using Game.Interfaces;

namespace Game.Ads
{
    /// <summary>
    /// Ad provider. Provides correct work of adverstions
    /// </summary>
    public class AdProvider : MonoBehaviour
    {
        /// <summary>
        /// List of ad providers
        /// </summary>
        List<IAd> adList;

        // Start is called before the first frame update
        async void Start()
        {
            adList = new List<IAd>();
            adList.AddRange(gameObject.GetComponents<IAd>());
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}