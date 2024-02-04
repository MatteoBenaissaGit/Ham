using System;
using System.Collections.Generic;
using Data.Items;
using Items;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;

namespace UI
{
    /// <summary>
    /// This class manage the player's hot bar
    /// </summary>
    public class HotBarController : MonoBehaviour
    {
        [SerializeField] private HorizontalOrVerticalLayoutGroup _layout;
        [SerializeField] private HotBarCaseController _hotBarCasePrefab;
        [SerializeField] private int _caseNumber;

        private List<HotBarCaseController> _cases = new List<HotBarCaseController>();
        private int _currentIndex;
        
        private void Awake()
        {
            if (_caseNumber <= 0)
            {
                Debug.LogWarning($"{_caseNumber} hot bar case !");
                return;
            }
            
            for (int i = 0; i < _caseNumber; i++)
            {
                HotBarCaseController hotBarCase = Instantiate(_hotBarCasePrefab, _layout.transform);
                _cases.Add(hotBarCase);
            }

            _currentIndex = 0;
            _cases[_currentIndex].SetSelected(true);
        }

        private void Update()
        {
            if (Character.CharacterController.Instance.Input.UIInput.HotBarNext)
            {
                Character.CharacterController.Instance.Input.UIInput.HotBarNext = false;
                SetHotBarSelection(true);
            }
            if (Character.CharacterController.Instance.Input.UIInput.HotBarPrevious)
            {
                Character.CharacterController.Instance.Input.UIInput.HotBarPrevious = false;
                SetHotBarSelection(false);
            }
            if (Character.CharacterController.Instance.Input.UIInput.HotBarDrop)
            {
                DropItem();
            }
        }

        /// <summary>
        /// This method change the current selected case
        /// </summary>
        /// <param name="next">select the next case if true, the previous one if false</param>
        private void SetHotBarSelection(bool next)
        {
            _cases[_currentIndex].SetSelected(false);

            if (next)
            {
                _currentIndex++;
                if (_currentIndex >= _cases.Count)
                {
                    _currentIndex = 0;
                }
            }
            else
            {
                _currentIndex--;
                if (_currentIndex < 0)
                {
                    _currentIndex = _cases.Count - 1;
                }
            }
            
            _cases[_currentIndex].SetSelected(true);
        }

        /// <summary>
        /// This method check if there is free space in the hotbar
        /// </summary>
        /// <returns>True is there is space, false if not</returns>
        public bool IsThereSpaceInHotBar()
        {
            for (int i = 0; i < _cases.Count; i++)
            {
                if (_cases[i].ItemController != null)
                {
                    continue;
                }

                return true;
            }

            return false;
        }
        
        /// <summary>
        /// Add an item to the hotbar
        /// </summary>
        /// <param name="item">The item controller to add</param>
        public void AddItemToBar(ItemController item)
        {
            if (IsThereSpaceInHotBar() == false)
            {
                Debug.Log("Item wasn't added to hot bar");
                return;
            }
            
            for (int i = 0; i < _cases.Count; i++)
            {
                if (_cases[i].ItemController != null)
                {
                    continue;
                }

                _cases[i].SetItem(item);
                if (_currentIndex == i)
                {
                    item.UsedState.SetActive(true);
                }
                return;
            }
        }

        /// <summary>
        /// Drop the current hold item
        /// </summary>
        private void DropItem()
        {
            if (_cases[_currentIndex].ItemController == null)
            {
                return;
            }
            
            _cases[_currentIndex].ItemController.Drop();
            _cases[_currentIndex].SetItem();
        }

        /// <summary>
        /// Destroy the specified item in hot bar
        /// </summary>
        /// <param name="item">the item to destroy</param>
        public void DestroyItem(ItemController item)
        {
            foreach (HotBarCaseController hotBarCase in _cases)
            {
                if (hotBarCase.ItemController == null || hotBarCase.ItemController != item)
                {
                    continue;
                }
                GameObject itemGameObject = item.gameObject;
                Destroy(item);
                Destroy(itemGameObject);
                hotBarCase.SetItem();
            }
        }
    }
}
