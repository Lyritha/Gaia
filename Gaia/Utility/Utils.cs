using Microsoft.Xna.Framework;

namespace Gaia.Utility
{
    internal static class Utils
    {
        public static Vector2 UVToScreenPosition(Vector2 UVPos)
        {
            Vector2 ConvertedPosition = Vector2.Zero;

            ConvertedPosition.X = UVPos.X * GraphicsManager.GraphicsDeviceManager.PreferredBackBufferWidth;
            ConvertedPosition.Y = UVPos.Y * GraphicsManager.GraphicsDeviceManager.PreferredBackBufferHeight;

            return ConvertedPosition;
        }
    }
}
