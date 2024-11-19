using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Gaia
{
    public static class GraphicsManager
    {
        //keeps track of the values to scale sprites properly so that they take the same amount of space on the screen no matter the resolution
        private static readonly Vector2 virtualResolution = new(1920, 1080);
        public static float ResolutionScaling { get; private set; } = 1;

        // References to global systems and resources
        public static ContentManager Content { get; private set; }
        public static GraphicsDevice GraphicsDevice { get; private set; }
        public static GraphicsDeviceManager GraphicsDeviceManager { get; private set; }
        public static SpriteBatch SpriteBatch { get; private set; }

        // Initialization method to set up the global variables
        public static void Initialize(Game game, GraphicsDeviceManager graphicsDeviceManager)
        {
            Content = game.Content;
            GraphicsDevice = game.GraphicsDevice;
            GraphicsDeviceManager = graphicsDeviceManager;
            SpriteBatch = new(GraphicsDevice);

            InitializeGraphics();
        }

        private static void InitializeGraphics()
        {
            // Get the screen width and height
            int screenWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            int screenHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;

            // Calculate a single resolution scaling factor as the average of both X and Y
            ResolutionScaling = ((screenWidth / virtualResolution.X) + (screenHeight / virtualResolution.Y)) / 2;

            // Set the back buffer dimensions to match the screen resolution
            GraphicsDeviceManager.PreferredBackBufferWidth = screenWidth;
            GraphicsDeviceManager.PreferredBackBufferHeight = screenHeight;

            GraphicsDeviceManager.IsFullScreen = true;

            GraphicsDeviceManager.ApplyChanges();
        }

        public static void CreateSpriteBatch()
        {

            SpriteBatch.Begin(
                SpriteSortMode.BackToFront,
                BlendState.AlphaBlend,
                SamplerState.LinearClamp,
                DepthStencilState.None,
                RasterizerState.CullNone
                );

            // Raise the OnDraw event to allow global subscribers to draw themselves
            GlobalEvents.RaiseOnDraw(SpriteBatch);

            SpriteBatch.End();
        }
    }
}

