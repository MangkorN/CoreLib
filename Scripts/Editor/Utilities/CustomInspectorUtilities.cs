using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace CoreLib.Utilities.Editor
{
    public static class CustomInspectorUtilities
    {
        private static Color _colorDefault = new(0.5f, 0.5f, 0.5f, 0.5f);

        public static void DrawUILine(Color color, float thickness = 2, float padding = 5)
        {
            Rect r = EditorGUILayout.GetControlRect(GUILayout.Height(padding + thickness));
            r.height = thickness;
            r.y += padding / 2;
            r.x -= 2;
            r.width += 6;
            EditorGUI.DrawRect(r, color);
        }

        public static void ShowHideLabel(string label, ref bool toggleStatus, float buttonWidth = 80f)
        {
            EditorGUILayout.BeginHorizontal();
            {
                toggleStatus = EditorGUILayout.ToggleLeft(label, toggleStatus, EditorStyles.boldLabel);
                if (toggleStatus)
                {
                    if (GUILayout.Button("Hide", GUILayout.Width(buttonWidth)))
                    {
                        toggleStatus = false;
                    }
                }
                else
                {
                    if (GUILayout.Button("Show", GUILayout.Width(buttonWidth)))
                    {
                        toggleStatus = true;
                    }
                }
            }
            EditorGUILayout.EndHorizontal();
        }

        public static void DisplayLabelWithButtons(string label, params (string, Action)[] buttonActions)
        {
            EditorGUILayout.BeginVertical("box");
            EditorGUILayout.LabelField(label, EditorStyles.boldLabel);
            DrawUILine(_colorDefault);
            GUILayout.Space(5f);

            foreach (var (buttonLabel, action) in buttonActions)
            {
                if (GUILayout.Button(buttonLabel))
                {
                    action?.Invoke();
                }
            }

            EditorGUILayout.EndVertical();
        }

        public static void SelectableLabel(string text)
        {
            GUIStyle labelStyle = new(EditorStyles.label)
            {
                wordWrap = true
            };

            EditorGUILayout.SelectableLabel(text, labelStyle, GUILayout.Height(EditorGUIUtility.singleLineHeight * CalcLines(text, labelStyle)));
        }

        private static int CalcLines(string text, GUIStyle style)
        {
            GUIContent content = new GUIContent(text);
            float width = EditorGUIUtility.currentViewWidth - 40; // Subtract some width for padding
            float height = style.CalcHeight(content, width);
            return Mathf.Max(1, Mathf.CeilToInt(height / EditorGUIUtility.singleLineHeight));
        }
    }
}
