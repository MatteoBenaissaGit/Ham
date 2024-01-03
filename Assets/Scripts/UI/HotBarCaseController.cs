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
        [Header("References"),SerializeField] private ItemData _item;
        [SerializeField] private Image _itemImage;
        [SerializeField] private Image _backgroundImage;
        [Header("Parameters"), SerializeField, Range(0,1)] private float _unselectedTransparency;

        private Vector3 _baseScale;
        
        private void Awake()
        {
            _backgroundImage.color = new Color(1f, 1f, 1f, _unselectedTransparency);
            _itemImage.color = new Color(1f, 1f, 1f, 0f);
            
            _baseScale = transform.localScale;
            transform.localScale = _baseScale * 0.8f;
        }

        /// <summary>
        /// Set the current selected state of the case
        /// </summary>
        /// <param name="isSelected">select the case if true, unselect if false</param>
        public void SetSelected(bool isSelected)
        {
            float animationTime = 0.2f;
            
            transform.DOKill();
            transform.DOScale(isSelected ? _baseScale : _baseScale * 0.8f, animationTime);
            
            _backgroundImage.DOKill();
            _backgroundImage.DOColor(isSelected ? Color.white : new Color(1f, 1f, 1f, _unselectedTransparency), animationTime);
            
            if (_item != null)
            {
                _itemImage.DOKill();
                _itemImage.DOColor(isSelected ? Color.white : new Color(1f, 1f, 1f, _unselectedTransparency), animationTime);
            }
        }
    }
}
