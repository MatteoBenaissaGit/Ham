using DG.Tweening;
using UnityEngine;
using CharacterController = Character.CharacterController;

namespace Items.UseBehaviours
{
    public class JetpackUseBehaviour : ItemUseBehaviour
    {
        private bool _doReload;
        private float _jetpackEnergy;
        private float _timeToReload = 4f;
        private float _flyTime = 3f;
        
        public JetpackUseBehaviour(ItemController item) : base(item)
        {
        }

        public override void Initialize()
        {
            CharacterController.Instance.GameplayData.OnGrounded += (bool doReload) => _doReload = doReload;
        }

        public override void Update()
        {
            if (_jetpackEnergy >= _timeToReload || _doReload == false)
            {
                return;
            }
            _jetpackEnergy += Time.deltaTime;
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

        public override void ShootBehaviour()
        {
        }

        public override void ShootOnceBehaviour()
        {
        }
    }
}