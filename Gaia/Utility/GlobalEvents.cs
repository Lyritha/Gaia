using Microsoft.Xna.Framework.Graphics;
using System;

namespace Gaia
{
    internal static class GlobalEvents
    {
        public static event Action<float> OnUpdate;
        public static event Action<float> OnLateUpdate;
        public static event Action<SpriteBatch> OnDraw;
        public static event Action<int> OnEarnScore;

        public static void RaiseOnUpdate(float deltaTime) => OnUpdate?.Invoke(deltaTime);
        public static void RaiseOnLateUpdate(float deltaTime) => OnLateUpdate?.Invoke(deltaTime);
        public static void RaiseOnDraw(SpriteBatch spriteBatch) => OnDraw?.Invoke(spriteBatch);

        public static void RaiseOnEarnScore(int score) => OnEarnScore?.Invoke(score);
    }
}
