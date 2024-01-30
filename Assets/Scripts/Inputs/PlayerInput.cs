//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.6.3
//     from Assets/Scripts/Inputs/PlayerInput.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public partial class @PlayerInput: IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerInput()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerInput"",
    ""maps"": [
        {
            ""name"": ""CharacterController"",
            ""id"": ""6db98065-dfb1-47e8-b9f0-fbcbc2a67624"",
            ""actions"": [
                {
                    ""name"": ""MoveHorizontal"",
                    ""type"": ""Value"",
                    ""id"": ""75a2fb4b-5fcf-41e9-b0de-4997c5482295"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""MoveVertical"",
                    ""type"": ""Button"",
                    ""id"": ""6e818a53-bb1d-4f9a-9f7e-22958a733e4f"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""738fb884-3abb-48d9-8be2-b8da72ad096b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""Keyboard"",
                    ""id"": ""3f4f972d-1533-48c8-9c6a-524d020c29ab"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveHorizontal"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""85972c9d-e405-4925-b272-7ede89c73e7e"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveHorizontal"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""ef506531-c318-4826-b5b5-9681105d8bd8"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveHorizontal"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Gamepad"",
                    ""id"": ""31f5f91d-8eb8-4b35-9b28-92e31bd00605"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveHorizontal"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""eb068723-a711-4901-9a11-dd1c2f976fc4"",
                    ""path"": ""<Gamepad>/leftStick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveHorizontal"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""051b3fe7-be70-4b56-bf74-a32b45e7e097"",
                    ""path"": ""<Gamepad>/leftStick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveHorizontal"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Keyboard"",
                    ""id"": ""f21efc80-5036-45e3-97ad-619c0b797a7b"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveVertical"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""90efbfa9-7047-4633-b82b-64f043b8d46d"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveVertical"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""66560b09-c678-4a7c-926c-e11ba49df078"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveVertical"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Gamepad"",
                    ""id"": ""eca06080-0f10-4153-be3f-4c1d9062f40e"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveVertical"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""6b27f132-908c-418d-8909-96afcf14d593"",
                    ""path"": ""<Gamepad>/leftStick/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveVertical"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""9cffd68d-4f53-4660-b500-88284d39cfec"",
                    ""path"": ""<Gamepad>/leftStick/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveVertical"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""90934ae4-e745-4f7a-a560-955c9a402fed"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1b0d094b-b448-4e00-b0c9-5197980fa1d9"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""CameraMovement"",
            ""id"": ""f91ac92b-6657-4252-9025-ed127aab7e27"",
            ""actions"": [
                {
                    ""name"": ""XMovement"",
                    ""type"": ""Value"",
                    ""id"": ""393639c8-d26d-4658-a7cb-c4bd7800a7d5"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""YMovement"",
                    ""type"": ""Value"",
                    ""id"": ""cf3e0bc8-7611-4c72-bdbf-5281f7206ec2"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""Mouse"",
                    ""id"": ""dda792f5-286f-46d8-9df9-44e0174ee212"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": ""Scale(factor=0.1)"",
                    ""groups"": """",
                    ""action"": ""XMovement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""47fd91e2-59bb-491d-81ae-8bc0f0c5cf8d"",
                    ""path"": ""<Mouse>/delta/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""XMovement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""136e8097-3acf-4fa4-becb-a196a9cb21f5"",
                    ""path"": ""<Mouse>/delta/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""XMovement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Stick"",
                    ""id"": ""4b277e56-5413-4cfd-ba8e-48dd378f2d02"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": ""Scale(factor=4)"",
                    ""groups"": """",
                    ""action"": ""XMovement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""6f7e58ba-8a42-4402-a8d9-4845a4e5434e"",
                    ""path"": ""<Gamepad>/rightStick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""XMovement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""31bd9704-1d18-4321-bf42-3deadd5358df"",
                    ""path"": ""<Gamepad>/rightStick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""XMovement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Mouse"",
                    ""id"": ""e7e17588-de94-45d6-8a98-67609044ec43"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": ""Scale(factor=0.1)"",
                    ""groups"": """",
                    ""action"": ""YMovement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""5beb4ff7-4e76-464d-9de6-69677154b49b"",
                    ""path"": ""<Mouse>/delta/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""YMovement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""3412af3b-f4c7-4432-ac0e-812e7677dee1"",
                    ""path"": ""<Mouse>/delta/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""YMovement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Stick"",
                    ""id"": ""0d28f6c6-9322-405e-bc1e-15ef234d4577"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": ""Scale(factor=4)"",
                    ""groups"": """",
                    ""action"": ""YMovement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""49e03c88-bbda-48d1-bea3-1579be28dfde"",
                    ""path"": ""<Gamepad>/rightStick/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""YMovement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""ae9df021-c024-4157-ac6b-ed7015a70824"",
                    ""path"": ""<Gamepad>/rightStick/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""YMovement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                }
            ]
        },
        {
            ""name"": ""UI"",
            ""id"": ""9b06bdac-9497-4905-bed8-a0a01dec4601"",
            ""actions"": [
                {
                    ""name"": ""HotBar"",
                    ""type"": ""Value"",
                    ""id"": ""356c4ebe-781f-4c7e-ad74-f95a0b239682"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": ""Press"",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""HotBarDrop"",
                    ""type"": ""Button"",
                    ""id"": ""9a23bd69-4199-4cc2-859d-013b0ea34b77"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""Mouse"",
                    ""id"": ""407f98e1-7737-4a6d-a72d-4b28595ef276"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""HotBar"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""2b061b2e-3553-4511-a34e-282513bbe3d7"",
                    ""path"": ""<Mouse>/scroll/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""HotBar"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""cc7a202a-f536-421e-8e3b-9c4c28a46760"",
                    ""path"": ""<Mouse>/scroll/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""HotBar"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Gamepad"",
                    ""id"": ""a5e57b0e-6c18-4495-99d9-91a1da701f2b"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""HotBar"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""0f16a942-4d2c-4df3-9bc2-7482618ef19f"",
                    ""path"": ""<Gamepad>/dpad/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""HotBar"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""f2166754-bbfa-4c64-8d01-f4977a36ffdf"",
                    ""path"": ""<Gamepad>/dpad/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""HotBar"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""4a4b6108-cce2-4483-9d84-2acb2421a229"",
                    ""path"": ""<Keyboard>/f"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""HotBarDrop"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d531067d-df60-40d2-9d35-c85e9cc1b912"",
                    ""path"": ""<Gamepad>/dpad/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""HotBarDrop"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""ItemController"",
            ""id"": ""84c1c7cc-7b43-4609-9158-d0e6b74c57d9"",
            ""actions"": [
                {
                    ""name"": ""Aim"",
                    ""type"": ""Value"",
                    ""id"": ""60dc16de-dc11-4342-86c7-a863f7cb0c19"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Shoot"",
                    ""type"": ""Button"",
                    ""id"": ""253581d6-7af2-4cd4-b05c-07af29562262"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""ShootOnce"",
                    ""type"": ""Value"",
                    ""id"": ""83f5c38f-4b62-42f4-abcc-9364800cfdc6"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Tap(pressPoint=0.5)"",
                    ""initialStateCheck"": true
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""e6153065-de23-40e7-98f7-d4547fc8b5b4"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Aim"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c7f3d8fc-0d8c-4c0f-b1c0-864ce7d57eff"",
                    ""path"": ""<Gamepad>/leftTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Aim"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b069c67a-2be2-4fa3-8668-df14967890c2"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Shoot"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""7eb4bdfa-be48-4fb0-ab74-de00bac23b87"",
                    ""path"": ""<Gamepad>/rightTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Shoot"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9b83c84d-19db-4934-b5bb-19442bf4d9ef"",
                    ""path"": ""<Gamepad>/rightTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ShootOnce"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""434780a7-56da-46cd-8d31-3594e178189d"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ShootOnce"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // CharacterController
        m_CharacterController = asset.FindActionMap("CharacterController", throwIfNotFound: true);
        m_CharacterController_MoveHorizontal = m_CharacterController.FindAction("MoveHorizontal", throwIfNotFound: true);
        m_CharacterController_MoveVertical = m_CharacterController.FindAction("MoveVertical", throwIfNotFound: true);
        m_CharacterController_Jump = m_CharacterController.FindAction("Jump", throwIfNotFound: true);
        // CameraMovement
        m_CameraMovement = asset.FindActionMap("CameraMovement", throwIfNotFound: true);
        m_CameraMovement_XMovement = m_CameraMovement.FindAction("XMovement", throwIfNotFound: true);
        m_CameraMovement_YMovement = m_CameraMovement.FindAction("YMovement", throwIfNotFound: true);
        // UI
        m_UI = asset.FindActionMap("UI", throwIfNotFound: true);
        m_UI_HotBar = m_UI.FindAction("HotBar", throwIfNotFound: true);
        m_UI_HotBarDrop = m_UI.FindAction("HotBarDrop", throwIfNotFound: true);
        // ItemController
        m_ItemController = asset.FindActionMap("ItemController", throwIfNotFound: true);
        m_ItemController_Aim = m_ItemController.FindAction("Aim", throwIfNotFound: true);
        m_ItemController_Shoot = m_ItemController.FindAction("Shoot", throwIfNotFound: true);
        m_ItemController_ShootOnce = m_ItemController.FindAction("ShootOnce", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    public IEnumerable<InputBinding> bindings => asset.bindings;

    public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
    {
        return asset.FindAction(actionNameOrId, throwIfNotFound);
    }

    public int FindBinding(InputBinding bindingMask, out InputAction action)
    {
        return asset.FindBinding(bindingMask, out action);
    }

    // CharacterController
    private readonly InputActionMap m_CharacterController;
    private List<ICharacterControllerActions> m_CharacterControllerActionsCallbackInterfaces = new List<ICharacterControllerActions>();
    private readonly InputAction m_CharacterController_MoveHorizontal;
    private readonly InputAction m_CharacterController_MoveVertical;
    private readonly InputAction m_CharacterController_Jump;
    public struct CharacterControllerActions
    {
        private @PlayerInput m_Wrapper;
        public CharacterControllerActions(@PlayerInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @MoveHorizontal => m_Wrapper.m_CharacterController_MoveHorizontal;
        public InputAction @MoveVertical => m_Wrapper.m_CharacterController_MoveVertical;
        public InputAction @Jump => m_Wrapper.m_CharacterController_Jump;
        public InputActionMap Get() { return m_Wrapper.m_CharacterController; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(CharacterControllerActions set) { return set.Get(); }
        public void AddCallbacks(ICharacterControllerActions instance)
        {
            if (instance == null || m_Wrapper.m_CharacterControllerActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_CharacterControllerActionsCallbackInterfaces.Add(instance);
            @MoveHorizontal.started += instance.OnMoveHorizontal;
            @MoveHorizontal.performed += instance.OnMoveHorizontal;
            @MoveHorizontal.canceled += instance.OnMoveHorizontal;
            @MoveVertical.started += instance.OnMoveVertical;
            @MoveVertical.performed += instance.OnMoveVertical;
            @MoveVertical.canceled += instance.OnMoveVertical;
            @Jump.started += instance.OnJump;
            @Jump.performed += instance.OnJump;
            @Jump.canceled += instance.OnJump;
        }

        private void UnregisterCallbacks(ICharacterControllerActions instance)
        {
            @MoveHorizontal.started -= instance.OnMoveHorizontal;
            @MoveHorizontal.performed -= instance.OnMoveHorizontal;
            @MoveHorizontal.canceled -= instance.OnMoveHorizontal;
            @MoveVertical.started -= instance.OnMoveVertical;
            @MoveVertical.performed -= instance.OnMoveVertical;
            @MoveVertical.canceled -= instance.OnMoveVertical;
            @Jump.started -= instance.OnJump;
            @Jump.performed -= instance.OnJump;
            @Jump.canceled -= instance.OnJump;
        }

        public void RemoveCallbacks(ICharacterControllerActions instance)
        {
            if (m_Wrapper.m_CharacterControllerActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(ICharacterControllerActions instance)
        {
            foreach (var item in m_Wrapper.m_CharacterControllerActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_CharacterControllerActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public CharacterControllerActions @CharacterController => new CharacterControllerActions(this);

    // CameraMovement
    private readonly InputActionMap m_CameraMovement;
    private List<ICameraMovementActions> m_CameraMovementActionsCallbackInterfaces = new List<ICameraMovementActions>();
    private readonly InputAction m_CameraMovement_XMovement;
    private readonly InputAction m_CameraMovement_YMovement;
    public struct CameraMovementActions
    {
        private @PlayerInput m_Wrapper;
        public CameraMovementActions(@PlayerInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @XMovement => m_Wrapper.m_CameraMovement_XMovement;
        public InputAction @YMovement => m_Wrapper.m_CameraMovement_YMovement;
        public InputActionMap Get() { return m_Wrapper.m_CameraMovement; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(CameraMovementActions set) { return set.Get(); }
        public void AddCallbacks(ICameraMovementActions instance)
        {
            if (instance == null || m_Wrapper.m_CameraMovementActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_CameraMovementActionsCallbackInterfaces.Add(instance);
            @XMovement.started += instance.OnXMovement;
            @XMovement.performed += instance.OnXMovement;
            @XMovement.canceled += instance.OnXMovement;
            @YMovement.started += instance.OnYMovement;
            @YMovement.performed += instance.OnYMovement;
            @YMovement.canceled += instance.OnYMovement;
        }

        private void UnregisterCallbacks(ICameraMovementActions instance)
        {
            @XMovement.started -= instance.OnXMovement;
            @XMovement.performed -= instance.OnXMovement;
            @XMovement.canceled -= instance.OnXMovement;
            @YMovement.started -= instance.OnYMovement;
            @YMovement.performed -= instance.OnYMovement;
            @YMovement.canceled -= instance.OnYMovement;
        }

        public void RemoveCallbacks(ICameraMovementActions instance)
        {
            if (m_Wrapper.m_CameraMovementActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(ICameraMovementActions instance)
        {
            foreach (var item in m_Wrapper.m_CameraMovementActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_CameraMovementActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public CameraMovementActions @CameraMovement => new CameraMovementActions(this);

    // UI
    private readonly InputActionMap m_UI;
    private List<IUIActions> m_UIActionsCallbackInterfaces = new List<IUIActions>();
    private readonly InputAction m_UI_HotBar;
    private readonly InputAction m_UI_HotBarDrop;
    public struct UIActions
    {
        private @PlayerInput m_Wrapper;
        public UIActions(@PlayerInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @HotBar => m_Wrapper.m_UI_HotBar;
        public InputAction @HotBarDrop => m_Wrapper.m_UI_HotBarDrop;
        public InputActionMap Get() { return m_Wrapper.m_UI; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(UIActions set) { return set.Get(); }
        public void AddCallbacks(IUIActions instance)
        {
            if (instance == null || m_Wrapper.m_UIActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_UIActionsCallbackInterfaces.Add(instance);
            @HotBar.started += instance.OnHotBar;
            @HotBar.performed += instance.OnHotBar;
            @HotBar.canceled += instance.OnHotBar;
            @HotBarDrop.started += instance.OnHotBarDrop;
            @HotBarDrop.performed += instance.OnHotBarDrop;
            @HotBarDrop.canceled += instance.OnHotBarDrop;
        }

        private void UnregisterCallbacks(IUIActions instance)
        {
            @HotBar.started -= instance.OnHotBar;
            @HotBar.performed -= instance.OnHotBar;
            @HotBar.canceled -= instance.OnHotBar;
            @HotBarDrop.started -= instance.OnHotBarDrop;
            @HotBarDrop.performed -= instance.OnHotBarDrop;
            @HotBarDrop.canceled -= instance.OnHotBarDrop;
        }

        public void RemoveCallbacks(IUIActions instance)
        {
            if (m_Wrapper.m_UIActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IUIActions instance)
        {
            foreach (var item in m_Wrapper.m_UIActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_UIActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public UIActions @UI => new UIActions(this);

    // ItemController
    private readonly InputActionMap m_ItemController;
    private List<IItemControllerActions> m_ItemControllerActionsCallbackInterfaces = new List<IItemControllerActions>();
    private readonly InputAction m_ItemController_Aim;
    private readonly InputAction m_ItemController_Shoot;
    private readonly InputAction m_ItemController_ShootOnce;
    public struct ItemControllerActions
    {
        private @PlayerInput m_Wrapper;
        public ItemControllerActions(@PlayerInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @Aim => m_Wrapper.m_ItemController_Aim;
        public InputAction @Shoot => m_Wrapper.m_ItemController_Shoot;
        public InputAction @ShootOnce => m_Wrapper.m_ItemController_ShootOnce;
        public InputActionMap Get() { return m_Wrapper.m_ItemController; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(ItemControllerActions set) { return set.Get(); }
        public void AddCallbacks(IItemControllerActions instance)
        {
            if (instance == null || m_Wrapper.m_ItemControllerActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_ItemControllerActionsCallbackInterfaces.Add(instance);
            @Aim.started += instance.OnAim;
            @Aim.performed += instance.OnAim;
            @Aim.canceled += instance.OnAim;
            @Shoot.started += instance.OnShoot;
            @Shoot.performed += instance.OnShoot;
            @Shoot.canceled += instance.OnShoot;
            @ShootOnce.started += instance.OnShootOnce;
            @ShootOnce.performed += instance.OnShootOnce;
            @ShootOnce.canceled += instance.OnShootOnce;
        }

        private void UnregisterCallbacks(IItemControllerActions instance)
        {
            @Aim.started -= instance.OnAim;
            @Aim.performed -= instance.OnAim;
            @Aim.canceled -= instance.OnAim;
            @Shoot.started -= instance.OnShoot;
            @Shoot.performed -= instance.OnShoot;
            @Shoot.canceled -= instance.OnShoot;
            @ShootOnce.started -= instance.OnShootOnce;
            @ShootOnce.performed -= instance.OnShootOnce;
            @ShootOnce.canceled -= instance.OnShootOnce;
        }

        public void RemoveCallbacks(IItemControllerActions instance)
        {
            if (m_Wrapper.m_ItemControllerActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IItemControllerActions instance)
        {
            foreach (var item in m_Wrapper.m_ItemControllerActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_ItemControllerActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public ItemControllerActions @ItemController => new ItemControllerActions(this);
    public interface ICharacterControllerActions
    {
        void OnMoveHorizontal(InputAction.CallbackContext context);
        void OnMoveVertical(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
    }
    public interface ICameraMovementActions
    {
        void OnXMovement(InputAction.CallbackContext context);
        void OnYMovement(InputAction.CallbackContext context);
    }
    public interface IUIActions
    {
        void OnHotBar(InputAction.CallbackContext context);
        void OnHotBarDrop(InputAction.CallbackContext context);
    }
    public interface IItemControllerActions
    {
        void OnAim(InputAction.CallbackContext context);
        void OnShoot(InputAction.CallbackContext context);
        void OnShootOnce(InputAction.CallbackContext context);
    }
}
