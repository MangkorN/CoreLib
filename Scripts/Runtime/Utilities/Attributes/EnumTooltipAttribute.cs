using UnityEngine;

namespace CoreLib.Utilities
{
    /// <summary>
    /// EnumTooltip attribute.
    /// Attribute is used to provide tooltips for individual enum values in the inspector.
    /// </summary>
    public class EnumTooltipAttribute : PropertyAttribute
    {
        public string[] Tooltips { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="EnumTooltipAttribute"/> class.
        /// </summary>
        /// <param name="tooltips">Array of tooltips corresponding to the enum values.</param>
        public EnumTooltipAttribute(params string[] tooltips)
        {
            Tooltips = tooltips;
        }
    }
}