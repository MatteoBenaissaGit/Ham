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
        
        public CharacterZipLineState(CharacterController controller) : base(controller)
        {
        }

        public void InitializeZipLine(ZipLineController zipLine, Transform tip)
        {
            _zipLine = zipLine;
            bool isStartingAtZipLineStart = tip == zipLine.ZipLineStartTip;
            _zipLineTipStart = isStartingAtZipLineStart ? zipLine.ZipLineStartTip : zipLine.ZipLineEndTip;
            _zipLineTipEnd = isStartingAtZipLineStart ? zipLine.ZipLineEndTip : zipLine.ZipLineStartTip;

            _zipLineTipStartInteractable = _zipLineTipStart.GetComponent<IInteractable>();
            _zipLineTipEndInteractable = _zipLineTipEnd.GetComponent<IInteractable>();
            _zipLineTipStartInteractable.CanBeInteractedWith = false;
            _zipLineTipEndInteractable.CanBeInteractedWith = false;
            
            Controller.GravityBody.enabled = false;
            
            Controller.Rigidbody.isKinematic = true;
            Controller.Collider.enabled = false;
            Controller.GameplayData.IsMeshFollowingInputs = false;
            
            Controller.transform.DOKill();
            Controller.transform.DOMove(_zipLineTipStart.position - _zipLineTipStart.up * YOffset, 1f)
                .SetEase(Ease.Flash)
                .OnComplete(() => _launchMovement = true);
        }
        
        public override void Enter()
        {
        }

        public override void Update()
        {
        }

        public override void FixedUpdate()
        {
        }

        public override void Quit()
        {
            Controller.GameplayData.IsMeshFollowingInputs = true;

            Controller.Rigidbody.isKinematic = false;
            Controller.Collider.enabled = true;
            Controller.GravityBody.enabled = true;

            _zipLine = null;
            
            _zipLineTipStartInteractable.CanBeInteractedWith = true;
            _zipLineTipEndInteractable.CanBeInteractedWith = true;
            _zipLineTipStartInteractable = null;
            _zipLineTipEndInteractable = null;
        }

        public override void OnColliderEnter(Collision collision)
        {
        }

        public override void Jump(bool isPressingJump)
        {
        }
    }
}