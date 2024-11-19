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

        public static bool IsOutOfBounds(Vector2 pos)
        {
            int width = GraphicsManager.GraphicsDeviceManager.PreferredBackBufferWidth;
            int height = GraphicsManager.GraphicsDeviceManager.PreferredBackBufferHeight;

            int marginWidth = width / 10;
            int marginHeight = height / 10;

            return pos.X < 0 - marginWidth || pos.X > width + marginWidth || pos.Y < 0 - marginHeight || pos.Y > height + marginHeight;
        }
    }
}
