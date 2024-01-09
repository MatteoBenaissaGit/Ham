using System;
using Data.Items;
using DG.Tweening;
using Items;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    /// <summary>
    /// This class control a single case of the player's hot bar
    /// </summary>
    public class HotBarCaseController : MonoBehaviour
    {
        public ItemController ItemController { get; private set; }
        
        [Header("References"),SerializeField] private ItemData _itemData;
        [SerializeField] private Image _itemImage;
        [SerializeField] private Image _backgroundImage;
        [Header("Parameters"), SerializeField, Range(0,1)] private float _unselectedTransparency;

        private Vector3 _baseScale;
        private bool _isSelected;
        
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
            _isSelected = isSelected;
            
            float animationTime = 0.2f;
            
            transform.DOKill();
            transform.DOScale(_isSelected ? _baseScale : _baseScale * 0.8f, animationTime);
            
            _backgroundImage.DOKill();
            _backgroundImage.DOColor(_isSelected ? Color.white : new Color(1f, 1f, 1f, _unselectedTransparency), animationTime);
            
            if (_itemData != null)
            {
                _itemImage.DOKill();
                _itemImage.DOColor(_isSelected ? Color.white : new Color(1f, 1f, 1f, _unselectedTransparency), animationTime);
            }
        }

        /// <summary>
        /// Set an item to the case or clear it
        /// </summary>
        /// <param name="item">The item to add, null by default to clear it</param>
        public void SetItem(ItemController item = null)
        {
            ItemController = item;
            
            if (ItemController == null)
            {
                _itemData = null;
                _itemImage.sprite = null;
                _itemImage.color = new Color(1f, 1f, 1f, 0);
                return;
            }
            
            _itemData = ItemController.Data;
            
            _itemImage.color = _isSelected ? Color.white : new Color(1f, 1f, 1f, _unselectedTransparency);
            _itemImage.sprite = _itemData.HotBarSprite;

            if (_isSelected)
            {
                //TODO setup item in player's hands
            }
        }
    }
}
