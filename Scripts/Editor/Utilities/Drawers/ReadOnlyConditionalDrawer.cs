using UnityEngine;
using UnityEditor;
using UnityEngine.Rendering;
using CoreLib.Utilities;

namespace CoreLib.Utilities.Editor
{
    /// <summary>
    /// This class contains a custom drawer for the ReadOnlyConditional attribute.
    /// </summary>
    [CustomPropertyDrawer(typeof(ReadOnlyConditionalAttribute))]
    public class ReadOnlyConditionalDrawer : PropertyDrawer
    {
        /// <summary>
        /// Unity method for drawing GUI in Editor
        /// </summary>
        /// <param name="position">Position.</param>
        /// <param name="property">Property.</param>
        /// <param name="label">Label.</param>
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            ReadOnlyConditionalAttribute attribute = (ReadOnlyConditionalAttribute)base.attribute;

            bool applyReadOnly = false;

            foreach (string conditionFieldName in attribute.ConditionFieldNames)
            {
                SerializedProperty conditionProperty = property.serializedObject.FindProperty(conditionFieldName);
                applyReadOnly = applyReadOnly || conditionProperty.boolValue;
            }

            if (applyReadOnly)
            {
                GUI.enabled = false;
            }

            EditorGUI.PropertyField(position, property, label);

            GUI.enabled = true;
        }
    }
}