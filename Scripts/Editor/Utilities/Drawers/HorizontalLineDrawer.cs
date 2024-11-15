using UnityEngine;
using UnityEditor;
using CoreLib.Utilities;

namespace CoreLib.Utilities.Editor
{
    /// <summary>
    /// This class contains a custom drawer for the HorizontalLine attribute.
    /// </summary>
    [CustomPropertyDrawer(typeof(HorizontalLineAttribute))]
    public class HorizontalLineDrawer : DecoratorDrawer
    {
        public override float GetHeight()
        {
            return EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
        }

        public override void OnGUI(Rect position)
        {
            position.y += EditorGUIUtility.standardVerticalSpacing / 2;
            position.height = EditorGUIUtility.singleLineHeight;
            EditorGUI.LabelField(position, GUIContent.none, GUI.skin.horizontalSlider);
        }
    }

    /// <summary>
    /// This class contains a custom drawer for the HorizontalLineBold attribute.
    /// </summary>
    [CustomPropertyDrawer(typeof(HorizontalLineBoldAttribute))]
    public class HorizontalLineBoldDrawer : DecoratorDrawer
    {
        public override void OnGUI(Rect position)
        {
            HorizontalLineBoldAttribute attribute = (HorizontalLineBoldAttribute)base.attribute;
            CustomInspectorUtilities.DrawUILine(attribute.LineColor, 5, 1);
        }
    }
}