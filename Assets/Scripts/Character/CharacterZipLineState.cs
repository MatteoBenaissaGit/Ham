using System;
using DG.Tweening;
using Interfaces;
using Items.Props.ZipLine;
using UnityEngine;

namespace Character
{
    public class CharacterZipLineState : CharacterStateBase
    {
        private const float YOffset = 3f;
        
        private ZipLineController _zipLine;
        private Transform _zipLineTipStart, _zipLineTipEnd;
        private IInteractable _zipLineTipStartInteractable, _zipLineTipEndInteractable;
        private bool _launchMovement;
        private bool _isStartingAtZipLineStart;
        private Vector3 _positionToGoTo, _startPosition;
        private Vector3 _direction;
        private Vector3 _baseMeshOnZiplineRotation;
        private float _time;
        
        public CharacterZipLineState(CharacterController controller) : base(controller)
        {
        }
        
        public override string ToString()
        {
            return "Zipline state";
        }

        public void InitializeZipLine(ZipLineController zipLine, Transform tip)
        {
            _zipLine = zipLine;
            
            //set start and end zipline tips
            _isStartingAtZipLineStart = tip == zipLine.ZipLineStartTip;
            _zipLineTipStart = _isStartingAtZipLineStart ? zipLine.ZipLineStartTip : zipLine.ZipLineEndTip;
            _zipLineTipEnd = _isStartingAtZipLineStart ? zipLine.ZipLineEndTip : zipLine.ZipLineStartTip;
            
            //set direction
            _positionToGoTo = _zipLineTipEnd.position - _zipLineTipEnd.up * YOffset;
            _startPosition = tip.position - tip.up * YOffset;
            _direction = (_positionToGoTo - _startPosition).normalized;

            //set zipline tip interactable to false
            _zipLineTipStartInteractable = _zipLineTipStart.GetComponent<IInteractable>();
            _zipLineTipEndInteractable = _zipLineTipEnd.GetComponent<IInteractable>();
            _zipLineTipStartInteractable.CanBeInteractedWith = false;
            _zipLineTipEndInteractable.CanBeInteractedWith = false;
            
            //set player's component 
            Controller.GravityBody.ApplyAreaGravity = false;
            Controller.GravityBody.ClearGravityAreas();
            Controller.Rigidbody.isKinematic = true;
            Controller.Collider.enabled = false;
            Controller.GameplayData.IsMeshFollowingInputs = false;

            float animTime = 0.5f;
            
            //move player to start tip
            Controller.transform.DOKill();
            Controller.transform.DOMove(_startPosition, animTime).SetEase(Ease.Flash)
                .OnComplete(() =>
                {
                    _launchMovement = true;
                    Controller.OnCharacterAction.Invoke(CharacterGameplayAction.ZipLine);
                });
            
            //mesh rotation
            Vector3 baseRotation = Controller.Mesh.rotation.eulerAngles;
            Controller.Mesh.LookAt(_positionToGoTo);
            Vector3 newRotation = Controller.Mesh.rotation.eulerAngles;
            Controller.Mesh.rotation = Quaternion.Euler(baseRotation);
            _baseMeshOnZiplineRotation = new Vector3(baseRotation.x, newRotation.y, newRotation.z);
            Controller.Mesh.DORotate(_baseMeshOnZiplineRotation, animTime).SetEase(Ease.Flash);
        }
        
        public override void Enter()
        {
            Controller.GameplayData.IsGrounded = false;
            
            _time = 0;
        }

        public override void Update()
        {
            if (_launchMovement == false)
            {
                return;
            }

            _time += Time.deltaTime * 5f;
            
            float rotationAmplitude = 20f;
            float rotationAmount = (float)Math.Cos(_time) * rotationAmplitude;
            Controller.Mesh.rotation = Quaternion.Euler(_baseMeshOnZiplineRotation + new Vector3(0,0,rotationAmount));
            
            float movementAmplitude = 1f * (Vector3.Dot(Controller.Mesh.forward,Controller.transform.forward));
            float movementAmount = (float)Math.Cos(_time) * movementAmplitude;
            Controller.Mesh.localPosition = new Vector3(movementAmount,0,0);
        }

        public override void FixedUpdate()
        {
            if (_launchMovement == false)
            {
                return;
            }

            float dotProduct = Vector3.Dot(_direction, (_positionToGoTo - Controller.transform.position).normalized);
            if (dotProduct < 0.5f)
            {
                Controller.StateManager.SwitchState(Controller.StateManager.FallState);
                float velocityMultiplierAtEnd = 0.25f;
                Controller.Rigidbody.velocity *= velocityMultiplierAtEnd;
                return;
            }

            Controller.Rigidbody.MovePosition(Controller.Rigidbody.position + _direction * Controller.Data.ZiplineSpeed);
        }

        public override void Quit()
        {
            Controller.OnCharacterAction.Invoke(CharacterGameplayAction.StopZipLine);
            
            Controller.Rigidbody.isKinematic = false;
            Controller.Collider.enabled = true;
            Controller.GravityBody.ApplyAreaGravity = true;
            Controller.GameplayData.IsMeshFollowingInputs = true;

            _zipLine = null;
            
            _zipLineTipStartInteractable.CanBeInteractedWith = true;
            _zipLineTipEndInteractable.CanBeInteractedWith = true;
            _zipLineTipStartInteractable = null;
            _zipLineTipEndInteractable = null;

            _launchMovement = false;
            
            Controller.Mesh.localPosition = Vector3.zero;
            Vector3 rotation = Controller.Mesh.rotation.eulerAngles;
            Controller.Mesh.rotation = Quaternion.Euler(new Vector3(0,rotation.y,rotation.z));
        }

        public override void OnColliderEnter(Collision collision)
        {
        }

        public override void Jump(bool isPressingJump)
        {
            if (_launchMovement == false)
            {
                return;
            }
            
            Controller.StateManager.SwitchState(Controller.StateManager.JumpState);
        }
    }
}