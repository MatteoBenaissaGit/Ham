using DG.Tweening;
using UnityEngine;
using CharacterController = Character.CharacterController;

namespace Items.UseBehaviours
{
    public class JetpackUseBehaviour : ItemUseBehaviour
    {
        private const float FlyTime = 2f;
        private const float TimeToReloadMultiplierFlyTime = 0.5f;
        private const float FlyPower = 60f;
        private const float MaxFlyVelocity = 20f;
        
        private bool _doReload;
        private bool _isUsingJetpack;
        private float _jetpackEnergy;
        private Rigidbody _rigidbody;
        private CharacterController _character;
        
        public JetpackUseBehaviour(ItemController item) : base(item)
        {
        }

        public override void Initialize()
        {
            _jetpackEnergy = FlyTime;
            
            _character = CharacterController.Instance;
            _character.GameplayData.OnGrounded += (bool doReload) => _doReload = doReload;
            _rigidbody = _character.Rigidbody;
        }

        public override void Update()
        {
            Character.CharacterController.Instance.UI.EnergyBar.SetBarLevel(_jetpackEnergy / FlyTime);

            if (_jetpackEnergy >= FlyTime || _doReload == false)
            {
                return;
            }
            _jetpackEnergy += Time.deltaTime * TimeToReloadMultiplierFlyTime;
        }

        public override void FixedUpdate()
        {
            if (_isUsingJetpack == false)
            {
                if (_character.StateManager.CurrentState == _character.StateManager.InAirState)
                {
                    Debug.Log("check for change");
                    _character.StateManager.InAirState.CheckForChangeStateAtLanding();
                }
                return;
            }

            _jetpackEnergy -= Time.fixedDeltaTime;
            if (_jetpackEnergy <= 0)
            {
                EndJetpackEnergy();
                return;
            }
            
            Debug.Log("add force");
            _rigidbody.AddForce(_rigidbody.transform.up * FlyPower);
            if (_rigidbody.velocity.magnitude > MaxFlyVelocity)
            {
                Debug.Log("clamp velocity");
                _rigidbody.velocity *= 0.9f;
            }
        }

        public override void Quit()
        {
            CharacterController.Instance.GameplayData.OnGrounded -= (bool doReload) => _doReload = doReload;
        }

        public override void AimBehaviour(bool doAim)
        {
        }

        public override void AimStayBehaviour()
        {
        }

        public override void ShootBehaviour(bool isShooting)
        {
            if (isShooting && _isUsingJetpack == false)
            {
                if (_jetpackEnergy <= 0)
                {
                    return;
                }
                
                _doReload = false;
                _isUsingJetpack = true;
                SetParticles(true);
                _character.StateManager.SwitchState(_character.StateManager.InAirState);
                return;
            }
            
            EndJetpackEnergy();
            if (CharacterController.Instance.GameplayData.IsGrounded)
            {
                _doReload = true;
            }
        }

        private void EndJetpackEnergy()
        {
            _isUsingJetpack = false;
            SetParticles(false);
        }

        public override void ShootOnceBehaviour()
        {
            
        }

        private void SetParticles(bool doPlay)
        {
            if (doPlay)
            {
                Item.Particles[0].Play();
                Item.Particles[1].Play();
                return;
            }
            Item.Particles[0].Stop();
            Item.Particles[1].Stop();
        }
    }
}