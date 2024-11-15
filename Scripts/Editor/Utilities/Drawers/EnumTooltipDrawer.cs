using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace CoreLib.Utilities.Editor
{
    /// <summary>
    /// This class contains a custom drawer for the EnumTooltipAttribute.
    /// </summary>
    [CustomPropertyDrawer(typeof(EnumTooltipAttribute))]
    public class EnumTooltipDrawer : PropertyDrawer
    {
        /// <summary>
        /// Override the OnGUI method to draw the property with tooltips.
        /// </summary>
        /// <param name="position">Position of the property in the inspector.</param>
        /// <param name="property">Serialized property being drawn.</param>
        /// <param name="label">Label of the property.</param>
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EnumTooltipAttribute tooltipAttribute = (EnumTooltipAttribute)attribute;
            string[] tooltips = tooltipAttribute.Tooltips;

            if (property.propertyType == SerializedPropertyType.Enum)
            {
                int index = property.enumValueIndex;
                if (index >= 0 && index < tooltips.Length)
                {
                    label.tooltip = tooltips[index];
                }
            }

            EditorGUI.PropertyField(position, property, label);
        }
    }
}
