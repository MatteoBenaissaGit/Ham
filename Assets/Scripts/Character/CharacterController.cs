using System;
using UnityEngine;

namespace Character
{
    public class CharacterController : MonoBehaviour
    {
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
