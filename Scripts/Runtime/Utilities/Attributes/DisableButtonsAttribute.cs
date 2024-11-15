using UnityEngine;

namespace CoreLib.Utilities
{
    /// <summary>
    /// DisableButtons attribute.
    /// Attribute is used to disable buttons in properties that are lists or arrays.
    /// </summary>
    public class DisableButtonsAttribute : PropertyAttribute
    {
        public string[] ButtonNames;

        public DisableButtonsAttribute(params string[] buttonNames)
        {
            this.ButtonNames = buttonNames;
        }
    }
}