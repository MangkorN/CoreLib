using UnityEngine;

namespace CoreLib.Utilities
{
    /// <summary>
    /// DisplayName attribute.
    /// Attribute is used to override property names.
    /// </summary>
    public class DisplayNameAttribute : PropertyAttribute
    {
        public string DisplayName;

        public DisplayNameAttribute(string displayName)
        {
            this.DisplayName = displayName;
        }
    }
}