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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pos"></param>
        /// <param name="marginPercentage">margin size in length/margin</param>
        /// <returns></returns>
        public static bool IsOutOfBounds(Vector2 pos, float marginPercentage = 10)
        {
            int width = GraphicsManager.GraphicsDeviceManager.PreferredBackBufferWidth;
            int height = GraphicsManager.GraphicsDeviceManager.PreferredBackBufferHeight;

            int marginWidth = (int)(width / marginPercentage);
            int marginHeight = (int)(height / marginPercentage);

            return pos.X < 0 - marginWidth || pos.X > width + marginWidth || pos.Y < 0 - marginHeight || pos.Y > height + marginHeight;
        }
    }
}
