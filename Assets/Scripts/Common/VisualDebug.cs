using System;
using UnityEngine;

namespace Common
{
    public class VisualDebug : MonoBehaviour
    {
        [SerializeField] private Character.CharacterController _character;

        private void OnGUI()
        {
            GUIStyle style = new GUIStyle();
            style.fontSize = 30;
            style.normal.textColor = Color.white;

            //velocity
            string velocityLabelText = "velocity : " + _character.Rigidbody.velocity;
            GUILayout.Label(velocityLabelText, style);
            
            //dot product gravity
            string dotProductGravityLabelText = "dot product gravity rb/orbit : " + Vector3.Dot(_character.Rigidbody.transform.up, _character.GravityBody.GravityDirection);
            GUILayout.Label(dotProductGravityLabelText, style);
            
            //state
            string state = "state : " + _character.StateManager.CurrentState.ToString();
            GUILayout.Label(state, style);
        }
    }
}