using Gaia.Components;
using Gaia.Utility;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Gaia
{
    public class GameManager : Game
    {
        //keeps track of how much time elapsed between the previous and current frame
        private float deltaTime = 0;

        //keeps track of the values to scale sprites properly so that they take the same amount of space on the screen no matter the resolution
        private readonly Vector2 virtualResolution = new(1920,1080);
        private Vector2 resolutionScaling;

        //used to render the game
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        //keeps track of all current objects in the "scene"
        private List<GameObject> gameObjects = new();

        // Action triggers whenever an update occurs
        public event Action<float> OnUpdate;

        public GameManager()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        /// <summary>
        /// prepares all the graphics for the game.
        /// </summary>
        private void InitializeGraphics()
        {
            // Get the screen width and height
            int screenWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            int screenHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;

            //check the scaling that needs to be done to keep sprites/objects the same size
            resolutionScaling.X = screenWidth / virtualResolution.X;
            resolutionScaling.Y = screenHeight / virtualResolution.Y;

            // Set the back buffer dimensions to match the screen resolution
            graphics.PreferredBackBufferWidth = screenWidth;
            graphics.PreferredBackBufferHeight = screenHeight;

            graphics.IsFullScreen = true;

            graphics.ApplyChanges();
        }

        protected override void Initialize()
        {
            InitializeGraphics();

            Texture2D texture = Content.Load<Texture2D>("Player");

            GameObject player = new(Utils.UVToScreenPosition(new(0.5f, 0.5f), graphics), texture, new(0.5f, 0.5f));
            OnUpdate += player.Update;
            gameObjects.Add(player);

            base.Initialize();
        }

        protected override void LoadContent() => spriteBatch = new SpriteBatch(GraphicsDevice);

        protected override void Update(GameTime gameTime)
        {
            //update the deltaTime for this frame
            deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            //invokes the update method in every script subscribed
            OnUpdate?.Invoke(deltaTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            foreach (GameObject obj in gameObjects) 
            {
                obj.DrawSelf(spriteBatch, resolutionScaling);
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
