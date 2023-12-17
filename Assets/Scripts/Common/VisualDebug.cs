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
            style.fontSize = 20;
            style.normal.textColor = Color.white;

            string labelText = _character.Rigidbody.velocity.ToString();
            
            GUILayout.Label(labelText, style);
        }
    }
}