using System;
using Data.Character;
using Gravity;
using UnityEngine;

namespace Character
{
    public class CharacterController : MonoBehaviour
    {
        [field:SerializeField] public CharacterControllerData ControllerData { get; private set; }
        [field:SerializeField] public GravityController Gravity { get; private set; }
        
        public CharacterStateManager StateManager { get; private set; }

        #region MonoBehaviour methods

        private void Awake()
        {
            StateManager = new CharacterStateManager();
            StateManager.Initialize(this);
        }

        private void Update()
        {
            StateManager.UpdateState();
        }

        #endregion
        
        
    }
}
