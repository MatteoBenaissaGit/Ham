using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class AimUIController : MonoBehaviour
    {
        [field: SerializeField] private Image _baseCrosshair;

        private void Awake()
        {
            _baseCrosshair.gameObject.SetActive(false);
        }

        /// <summary>
        /// Set the visibility of the crosshair
        /// </summary>
        /// <param name="doShow">show the crosshair</param>
        public void Set(bool doShow)
        {
            _baseCrosshair.gameObject.SetActive(doShow);
        }
    }
}