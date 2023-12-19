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
            string dotProductGravityLabelText = "dot product gravity : " + Vector3.Dot(_character.Rigidbody.transform.up, _character.Gravity.GravityOrbitUp);
            GUILayout.Label(dotProductGravityLabelText, style);
        }
    }
}