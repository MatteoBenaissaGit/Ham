using System;
using Inputs;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class InteractionUIController : MonoBehaviour
    {
        [SerializeField] private Image _interactionButtonImage;
        [SerializeField] private Sprite _interactionSpriteXbox;
        [SerializeField] private Sprite _interactionSpritePlay;
        [SerializeField] private Sprite _interactionSpriteKeyboard;

        private Transform _transformToFollow;

        private void Awake()
        {
            _interactionButtonImage.gameObject.SetActive(false);
        }

        private void Update()
        {
            if (_transformToFollow == null)
            {
                return;
            }

            _interactionButtonImage.transform.position = _transformToFollow.position + _transformToFollow.up;
            _interactionButtonImage.transform.LookAt(Character.CharacterController.Instance.CameraController.Camera.transform.position);
        }

        public void SetInteractionButton(Transform interactable = null)
        {
            _transformToFollow = interactable;

            if (_transformToFollow == null)
            {
                Clear();
                return;
            }

            _interactionButtonImage.sprite = InputManager.CurrentDevice switch
            {
                InputDevice.Keyboard => _interactionSpriteKeyboard,
                InputDevice.XboxGamepad => _interactionSpriteXbox,
                InputDevice.PlayGamepad => _interactionSpritePlay,
                _ => throw new ArgumentOutOfRangeException()
            };
            _interactionButtonImage.gameObject.SetActive(true);
        }
        
        private void Clear()
        {
            _transformToFollow = null;
            _interactionButtonImage.gameObject.SetActive(false);
        }
    }
}