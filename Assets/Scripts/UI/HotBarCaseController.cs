using System;
using Data.Items;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    /// <summary>
    /// This class control a single case of the player's hot bar
    /// </summary>
    public class HotBarCaseController : MonoBehaviour
    {
        [field: SerializeField] public ItemData Item { get; private set; }
        [field: SerializeField] public Image ItemImage { get; private set; }

        private Vector3 _baseScale;
        
        private void Awake()
        {
            ItemImage.color = new Color(1f, 1f, 1f, 0f);
            
            _baseScale = transform.localScale;
            transform.localScale = _baseScale * 0.8f;
        }

        /// <summary>
        /// Set the current selected state of the case
        /// </summary>
        /// <param name="isSelected">select the case if true, unselect if false</param>
        public void SetSelected(bool isSelected)
        {
            transform.DOKill();
            transform.DOScale(isSelected ? _baseScale : _baseScale * 0.8f, 0.2f);
        }
    }
}
