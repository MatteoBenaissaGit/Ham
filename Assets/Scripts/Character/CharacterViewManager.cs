using System;
using DG.Tweening;
using UnityEngine;

namespace Character
{
    public class CharacterViewManager : MonoBehaviour
    {
        [SerializeField] private CharacterController _characterController; 
        [Space(10), SerializeField] private Animator _animator;
        [Space(10), SerializeField] private ParticleSystem _walkParticles;
        [SerializeField] private ParticleSystem _jumpParticles;
        [SerializeField] private ParticleSystem _landParticles;
        [SerializeField] private TrailRenderer _jumpTrail;
        [SerializeField] private ParticleSystem _ziplineParticles;

        private void Awake()
        {
            _jumpTrail.time = 0f;
            
            _characterController.OnCharacterAction += HandleCharacterActions;
        }

        private void HandleCharacterActions(CharacterGameplayAction action)
        {
            switch (action)
            {
                case CharacterGameplayAction.Walk:
                    SetWalkView(true);
                    break;
                case CharacterGameplayAction.StopWalk:
                    SetWalkView(false);
                    break;
                case CharacterGameplayAction.Jump:
                    SetJumpView(true);
                    break;
                case CharacterGameplayAction.Fall:
                    LaunchFallView();
                    break;
                case CharacterGameplayAction.Land:
                    SetJumpView(false);
                    break;
                case CharacterGameplayAction.Aim:
                    SetAimView(true);
                    break;
                case CharacterGameplayAction.StopAim:
                    SetAimView(false);
                    break;
                case CharacterGameplayAction.ZipLine:
                    SetZiplineView(true);
                    break;
                case CharacterGameplayAction.StopZipLine:
                    SetZiplineView(false);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(action), action, null);
            }
        }

        private void SetWalkView(bool doLaunch)
        {
            _animator.SetBool("isWalking", doLaunch);
            
            if (doLaunch)
            {
                _walkParticles.Play();
                return;
            }
            
            _walkParticles.Stop();
        }
        
        private void SetJumpView(bool doLaunch)
        {
            _animator.SetBool("isJumping", doLaunch);
            _jumpTrail.DOKill();
            
            if (doLaunch)
            {
                _jumpTrail.DOTime(_characterController.Data.JumpTrailLength, 0.1f);
                if (_characterController.GameplayData.IsGrounded)
                {
                    _jumpParticles.Play();
                }
                return;
            }
            
            _jumpTrail.DOTime(0, 0.25f);
            _landParticles.Play();
        }

        private void LaunchFallView()
        {
            _animator.SetBool("isJumping", true);
            _jumpTrail.DOKill();
            _jumpTrail.DOTime(_characterController.Data.JumpTrailLength, 0.1f);
        }

        private void SetAimView(bool doAim)
        {
            _animator.SetBool("isAiming", doAim);
        }

        private void SetZiplineView(bool doZipline)
        {
            if (doZipline)
            {
                Vector3 meshRotation = _characterController.Mesh.rotation.eulerAngles;
                Vector3 baseRotation = _ziplineParticles.transform.rotation.eulerAngles;
                _ziplineParticles.transform.rotation = Quaternion.Euler(new Vector3(baseRotation.x,-meshRotation.y,baseRotation.z));
                _ziplineParticles.Play();
            }
            else
            {
                _ziplineParticles.Stop();
            }
        }
    }
}
