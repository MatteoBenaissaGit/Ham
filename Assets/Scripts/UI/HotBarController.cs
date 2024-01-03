using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
            if (UIManager.Instance.Character.Input.UIInput.HotBarNext)
            {
                SetHotBarSelection(true);
            }
            if (UIManager.Instance.Character.Input.UIInput.HotBarPrevious)
            {
                SetHotBarSelection(false);
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
    }
}
