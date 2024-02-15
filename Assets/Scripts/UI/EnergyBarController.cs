using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class EnergyBarController : MonoBehaviour
    {
        [SerializeField] private GameObject _energyBar;
        [SerializeField] private Image _bar;

        private const float MovementAmplitude = 200;
        private Vector3 _basePosition;

        private void Awake()
        {
            _basePosition = _energyBar.transform.position;
            _energyBar.transform.position = _basePosition + new Vector3(-MovementAmplitude,0,0);
        }

        public void SetBar(bool doShow)
        {
            _energyBar.transform.DOKill();
            _energyBar.transform.DOMove(_basePosition + new Vector3((doShow ? 0 : -MovementAmplitude), 0, 0), 0.5f);
        }

        public void SetBarLevel(float percentage)
        {
            _bar.fillAmount = percentage;
        }
    }
}
