using UnityEngine;
using UnityEditor;
using CoreLib.Utilities;

namespace CoreLib.Utilities.Editor
{
    /// <summary>
    /// This class contains a custom drawer for the DisplayName attribute.
    /// </summary>
    [CustomPropertyDrawer(typeof(DisplayNameAttribute))]
    public class DisplayNameDrawer : PropertyDrawer
    {
        /// <summary>
        /// Unity method for drawing GUI in Editor
        /// </summary>
        /// <param name="position">Position.</param>
        /// <param name="property">Property.</param>
        /// <param name="label">Label.</param>
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            DisplayNameAttribute attribute = (DisplayNameAttribute)base.attribute;

            label.text = attribute.DisplayName;
            EditorGUI.PropertyField(position, property, label, true);
        }   
    }
}