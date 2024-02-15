using DG.Tweening;
using UnityEngine;
using CharacterController = Character.CharacterController;

namespace Items.UseBehaviours
{
    public class JetpackUseBehaviour : ItemUseBehaviour
    {
        private const float FlyTime = 2f;
        private const float TimeToReloadMultiplierFlyTime = 0.5f;
        
        private bool _doReload;
        private bool _isUsingJetpack;
        private float _jetpackEnergy;
        
        public JetpackUseBehaviour(ItemController item) : base(item)
        {
        }

        public override void Initialize()
        {
            _jetpackEnergy = FlyTime;
            
            CharacterController.Instance.GameplayData.OnGrounded += (bool doReload) => _doReload = doReload;
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
                return;
            }
            
            _jetpackEnergy -= Time.fixedDeltaTime;
            if (_jetpackEnergy <= 0)
            {
                SetParticles(false);
                _isUsingJetpack = false;
                return;
            }
            //TODO apply add force (! delta time)
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
                return;
            }
            
            _isUsingJetpack = false;
            SetParticles(false);
            if (CharacterController.Instance.GameplayData.IsGrounded)
            {
                _doReload = true;
            }
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