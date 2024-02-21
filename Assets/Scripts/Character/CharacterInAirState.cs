using UnityEngine;

namespace Character
{
    public class CharacterInAirState : CharacterStateBase
    {
        public CharacterInAirState(CharacterController controller) : base(controller)
        {
        }

        public override string ToString()
        {
            return "In Air State";
        }

        public override void Enter()
        {
            Controller.GameplayData.IsGrounded = false;
            Controller.CameraController.SetCameraAfterCurrent(Controller.CameraController.Data.FallCamera);
            Debug.Log("in air enter");
        }

        public override void Update()
        {
            
        }

        public override void FixedUpdate()
        {
            float speed = Controller.Data.InAirMovementAmplitude;
            Vector3 forceDirection = Controller.GetCameraRelativeInputDirectionWorld();

            Vector3 force = forceDirection * (speed * Time.fixedDeltaTime);
            Controller.Rigidbody.AddForce(force, ForceMode.Impulse);
        }

        public override void Quit()
        {
            Debug.Log("in air quit");
            Controller.CameraController.EndCurrentCameraState();
        }

        public override void OnColliderEnter(Collision collision)
        {
        }

        public override void Jump(bool isPressingJump)
        {
        }
    }
}