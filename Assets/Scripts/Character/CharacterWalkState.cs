﻿using System;
using Inputs;
using UnityEngine;

namespace Character
{
    /// <summary>
    /// This state handle the character when he's moving on the ground
    /// </summary>
    public class CharacterWalkState : CharacterStateBase
    {
        private float _currentAccelerationTime;
        private Vector3 _currentDirection;
        
        public CharacterWalkState(CharacterController controller) : base(controller)
        {
        }

        public override void Enter()
        {
            _currentAccelerationTime = 0;
            
            //if we were jumping and moving we don't reset the acceleration time of the player
            if (Controller.StateManager.PreviousState == Controller.StateManager.JumpState
                && Controller.Input.CharacterControllerInput.IsMovingHorizontalOrVertical())
            {
                _currentAccelerationTime = Controller.Data.AccelerationTime;
            }
            
            Controller.OnCharacterAction.Invoke(CharacterGameplayAction.Walk);

            _currentDirection = Controller.GetCameraRelativeInputDirectionWorld();
        }

        public override void Update()
        {
            if (Controller.Input.CharacterControllerInput.Jump)
            {
                Controller.StateManager.SwitchState(Controller.StateManager.JumpState);
            }

            if (Controller.Input.CharacterControllerInput.IsMovingHorizontalOrVertical() == false)
            {
                Controller.StateManager.SwitchState(Controller.StateManager.IdleState);
            }
            
            CheckForFall();
        }

        public override void FixedUpdate()
        {
            HandleMovement();
        }

        public override void Quit()
        {
            Controller.OnCharacterAction.Invoke(CharacterGameplayAction.StopWalk);
        }

        public override void OnColliderEnter(Collision collision)
        {
            
        }

        /// <summary>
        /// Handle the movement input and apply them to the rigidbody.
        /// Is called in FixedUpdate.
        /// </summary>
        private void HandleMovement()
        {
            _currentAccelerationTime += Time.fixedDeltaTime;
            float accelerationValue = Mathf.Clamp01(_currentAccelerationTime / Controller.Data.AccelerationTime);
            float accelerationMultiplier = Controller.Data.AccelerationCurve.Evaluate(accelerationValue);
            
            Rigidbody rigidbody = Controller.Rigidbody;
            float speed = Controller.Data.WalkSpeed * accelerationMultiplier;
            
            Vector3 direction = Controller.GetCameraRelativeInputDirectionWorld();
            _currentDirection = Vector3.Lerp(_currentDirection, direction, Controller.Data.DirectionChangeLerpSpeed);
            float dotProduct = Vector3.Dot(direction, _currentDirection);
            float directionChangeSpeedMultiplier = 1f;
            if (dotProduct < 0)
            {
                float absDotProduct = Mathf.Clamp(Math.Abs(1 - dotProduct), 0.2f , 1f);
                directionChangeSpeedMultiplier = (absDotProduct * Controller.Data.DirectionChangeSpeedMultiplier);
            }

            rigidbody.MovePosition(rigidbody.position + direction * (speed * directionChangeSpeedMultiplier * Time.fixedDeltaTime));
        }
        
        /// <summary>
        /// Check if the player is falling while walking and change its state if necessary
        /// </summary>
        private void CheckForFall()
        {
            RaycastHit hit = Controller.GetRaycastTowardGround();
            if (hit.collider != null && hit.collider.gameObject != Controller.gameObject)
            {
                return;
            }

            Controller.GameplayData.IsGrounded = false;
            Controller.StateManager.SwitchState(Controller.StateManager.FallState);
        }
    }
}