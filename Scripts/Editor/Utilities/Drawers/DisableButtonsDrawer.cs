using UnityEngine;
using UnityEditor;
using CoreLib.Utilities;

namespace CoreLib.Utilities.Editor
{
    /// <summary>
    /// This class contains a custom drawer for the DisableButtons attribute.
    /// </summary>
    [CustomPropertyDrawer(typeof(DisableButtonsAttribute))]
    public class DisplayButtonsDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            DisableButtonsAttribute attribute = (DisableButtonsAttribute)base.attribute;

            int numButtons = attribute.ButtonNames.Length;
            float buttonWidth = position.width / numButtons;

            Rect[] buttonRects = new Rect[numButtons];
            for (int i = 0; i < numButtons; i++)
            {
                buttonRects[i] = new Rect(position.x + (buttonWidth * i), position.y, buttonWidth, position.height);
            }

            // Disable the buttons
            EditorGUI.BeginDisabledGroup(true);
            for (int i = 0; i < numButtons; i++)
            {
                bool buttonClicked = GUI.Button(buttonRects[i], attribute.ButtonNames[i]);
            }
            EditorGUI.EndDisabledGroup();

            // Draw the property
            EditorGUI.PropertyField(position, property, label);
        }
    }
}