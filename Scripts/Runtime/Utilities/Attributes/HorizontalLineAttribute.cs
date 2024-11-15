using UnityEngine;

namespace CoreLib.Utilities
{
    /// <summary>
    /// HorizontalLine attribute.
    /// Attribute is used to draw a horizontal line in the inspector.
    /// </summary>
    public class HorizontalLineAttribute : PropertyAttribute { }

    /// <summary>
    /// HorizontalBoldLine attribute.
    /// Attribute is used to draw a bold horizontal line in the inspector.
    /// </summary>
    public class HorizontalLineBoldAttribute : PropertyAttribute
    {
        public Color LineColor;

        /// <summary>
        /// Initializes a new instance of the <see cref="HorizontalLineBoldAttribute"/> class.
        /// </summary>
        /// <param name="r">Red component of the line color.</param>
        /// <param name="g">Green component of the line color.</param>
        /// <param name="b">Blue component of the line color.</param>
        /// <param name="a">Alpha component of the line color.</param>
        public HorizontalLineBoldAttribute(float r = 0.5f, float g = 0.5f, float b = 0.5f, float a = 1)
        {
            this.LineColor = new Color(r, g, b, a);
        }
    }
}