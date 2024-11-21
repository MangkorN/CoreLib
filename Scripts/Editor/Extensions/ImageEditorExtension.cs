using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UnityEditor.UI
{
    public class ImageEditorExtension
    {
        /// <summary>
        /// Resizes the UI object to the image source dimensions
        /// </summary>
        [MenuItem("CONTEXT/Image/Extensions/Resize to Source")]
        public static void AutoResizeImageToSource(MenuCommand command)
        {
            Image image = (Image)command.context;

            // Get the sprite from the image
            Sprite sprite = image.sprite;

            // Get the texture from the sprite
            Texture2D texture = sprite.texture;

            // Get the original dimensions of the texture
            int textureWidth = texture.width;
            int textureHeight = texture.height;

            // Resize the RectTransform to match the texture dimensions
            RectTransform rectTransform = image.rectTransform;
            rectTransform.sizeDelta = new Vector2(textureWidth, textureHeight);
        }
    }
}
