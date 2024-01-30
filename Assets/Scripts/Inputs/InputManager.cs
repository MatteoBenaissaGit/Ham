using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Inputs
{
    /// <summary>
    /// This class handle the inputs the player is making and storing them in dedicated classes
    /// </summary>
    public class InputManager
    {
        private PlayerInput _input;
        
        public CharacterControllerInput CharacterControllerInput { get; private set; }
        public CameraMovementInput CameraMovementInput { get; private set; }
        public UIInput UIInput { get; private set; }
        public ItemInput ItemInput { get; private set; }

        public void Initialize()
        {
            CharacterControllerInput = new CharacterControllerInput();
            CameraMovementInput = new CameraMovementInput();
            UIInput = new UIInput();
            ItemInput = new ItemInput();

            _input = new PlayerInput();
            _input.Enable();
            
            //Character controller
            _input.CharacterController.MoveHorizontal.performed += CharacterControllerInput.SetHorizontalMovement;
            _input.CharacterController.MoveHorizontal.canceled += CharacterControllerInput.SetHorizontalMovement;
            _input.CharacterController.MoveVertical.performed += CharacterControllerInput.SetVerticalMovement;
            _input.CharacterController.MoveVertical.canceled += CharacterControllerInput.SetVerticalMovement;
            _input.CharacterController.Jump.performed += CharacterControllerInput.SetJump;
            _input.CharacterController.Jump.canceled += CharacterControllerInput.SetJump;
            
            //camera movement
            _input.CameraMovement.XMovement.performed += CameraMovementInput.SetXMovement;
            _input.CameraMovement.XMovement.canceled += CameraMovementInput.SetXMovement;
            _input.CameraMovement.YMovement.performed += CameraMovementInput.SetYMovement;
            _input.CameraMovement.YMovement.canceled += CameraMovementInput.SetYMovement;
            
            //ui
            _input.UI.HotBar.performed += UIInput.SetHotBar;
            _input.UI.HotBar.canceled += UIInput.SetHotBar;
            _input.UI.HotBarDrop.performed += UIInput.SetHotBarDrop;
            _input.UI.HotBarDrop.canceled += UIInput.SetHotBarDrop;
            
            //items
            _input.ItemController.Aim.performed += ItemInput.SetAim;
            _input.ItemController.Aim.canceled += ItemInput.SetAim;
            _input.ItemController.Shoot.performed += ItemInput.SetShoot;
            _input.ItemController.Shoot.canceled += ItemInput.SetShoot;
            _input.ItemController.ShootOnce.performed += ItemInput.SetShootOnce;
        }
        
        public void Disable()
        {
            //Character controller
            _input.CharacterController.MoveHorizontal.performed -= CharacterControllerInput.SetHorizontalMovement;
            _input.CharacterController.MoveHorizontal.canceled -= CharacterControllerInput.SetHorizontalMovement;
            _input.CharacterController.MoveVertical.performed -= CharacterControllerInput.SetVerticalMovement;
            _input.CharacterController.MoveVertical.canceled -= CharacterControllerInput.SetVerticalMovement;
            _input.CharacterController.Jump.performed -= CharacterControllerInput.SetJump;
            _input.CharacterController.Jump.canceled -= CharacterControllerInput.SetJump;
            
            //camera movement
            _input.CameraMovement.XMovement.performed -= CameraMovementInput.SetXMovement;
            _input.CameraMovement.XMovement.canceled -= CameraMovementInput.SetXMovement;
            _input.CameraMovement.YMovement.performed -= CameraMovementInput.SetYMovement;
            _input.CameraMovement.YMovement.canceled -= CameraMovementInput.SetYMovement;
            
            //ui
            _input.UI.HotBar.performed -= UIInput.SetHotBar;
            _input.UI.HotBar.canceled -= UIInput.SetHotBar;
            _input.UI.HotBarDrop.performed -= UIInput.SetHotBarDrop;
            _input.UI.HotBarDrop.canceled -= UIInput.SetHotBarDrop;
            
            //items
            _input.ItemController.Aim.performed -= ItemInput.SetAim;
            _input.ItemController.Aim.canceled -= ItemInput.SetAim;
            _input.ItemController.Shoot.performed -= ItemInput.SetShoot;
            _input.ItemController.Shoot.canceled -= ItemInput.SetShoot;
            _input.ItemController.ShootOnce.performed -= ItemInput.SetShootOnce;
        }

        public void Update()
        {
            CharacterControllerInput.LastJumpInputTime += Time.deltaTime;
        }
    }
    
    /// <summary>
    /// This class receive and store the character controller inputs
    /// </summary>
    public class CharacterControllerInput
    {
        public float HorizontalMovement { get; private set; }
        public float VerticalMovement { get; private set; }
        public bool Jump { get; private set; }
        public float LastJumpInputTime { get; internal set; }

        public bool IsMovingHorizontalOrVertical()
        {
            return HorizontalMovement != 0 || VerticalMovement != 0;
        }
        
        public void SetHorizontalMovement(InputAction.CallbackContext context)
        {
            if (context.performed == false)
            {
                HorizontalMovement = 0;
                return;
            }

            HorizontalMovement = context.ReadValue<float>();
        }
        
        public void SetVerticalMovement(InputAction.CallbackContext context)
        {
            if (context.performed == false)
            {
                VerticalMovement = 0;
                return;
            }

            VerticalMovement = context.ReadValue<float>();
        }
        
        public void SetJump(InputAction.CallbackContext context)
        {
            if (context.performed == false || context.canceled)
            {
                Jump = false;
                return;
            }
            
            Jump = true;

            if (context.action.triggered)
            {
                LastJumpInputTime = 0;
            }
        }

        public Vector2 GetInputVector()
        {
            return new Vector3(HorizontalMovement, VerticalMovement).normalized;
        }
    }

    /// <summary>
    /// This class receive and store the camera control inputs
    /// </summary>
    public class CameraMovementInput
    {
        public float CameraXMovement { get; private set; }
        public float CameraYMovement { get; private set; }
        
        public void SetXMovement(InputAction.CallbackContext context)
        {
            if (context.performed == false)
            {
                CameraXMovement = 0;
                return;
            }

            CameraXMovement = context.ReadValue<float>();
        }
        
        public void SetYMovement(InputAction.CallbackContext context)
        {
            if (context.performed == false)
            {
                CameraYMovement = 0;
                return;
            }

            CameraYMovement = context.ReadValue<float>();
        }
    }
    
    /// <summary>
    /// This class receive and store the camera control inputs
    /// </summary>
    public class UIInput
    {
        public bool HotBarPrevious { get; set; }
        public bool HotBarNext { get; set; }
        public bool HotBarDrop { get; private set; }

        
        public void SetHotBar(InputAction.CallbackContext context)
        {
            float axis = context.ReadValue<float>();
            if (axis == 0 || context.performed == false)
            {
                HotBarNext = false;
                HotBarPrevious = false;
                return;
            }
            
            HotBarNext = axis > 0;
            HotBarPrevious = axis < 0;
        }

        public void SetHotBarDrop(InputAction.CallbackContext context)
        {
            if (context.performed == false || HotBarDrop)
            {
                HotBarDrop = false;
                return;
            }

            HotBarDrop = true;
        }
    }

    /// <summary>
    /// This class receive and store the items control inputs
    /// </summary>
    public class ItemInput
    {
        public bool Aim { get; private set; }
        public Action OnShoot { get; set; }
        public Action OnShootOnce { get; set; }
        public Action<bool> OnAim { get; set; }
        
        public void SetAim(InputAction.CallbackContext context)
        {
            bool wasAimingBefore = Aim;
            
            Aim = context.performed || context.started;

            if (Aim != wasAimingBefore)
            {
                OnAim?.Invoke(Aim);
            }
        }

        public void SetShoot(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                OnShoot?.Invoke();
            }
        }

        public void SetShootOnce(InputAction.CallbackContext context)
        {
            if (context.performed == false)
            {
                return;
            }
            
            OnShootOnce?.Invoke();
        }
    }
}
