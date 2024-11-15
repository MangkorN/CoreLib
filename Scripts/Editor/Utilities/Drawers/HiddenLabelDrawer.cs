using UnityEngine;
using UnityEditor;
using CoreLib.Utilities;

namespace CoreLib.Utilities.Editor
{
    /// <summary>
    /// This class contains a custom drawer for the HiddenLabel attribute. (WIP)
    /// </summary>
    [CustomPropertyDrawer(typeof(HiddenLabelAttribute))]
    public class HiddenLabelDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            // Do nothing
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return 0f; // Return a height of 0 to take up no space
        }
    }
}