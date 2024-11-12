using Gaia.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Gaia
{
    public class GameManager : Game
    {
        private float deltaTime = 0;

        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        private List<GameObject> gameObjects = new();

        public GameManager()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            Vector2 screenCenter = new Vector2(GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height / 2);
            Texture2D texture = Content.Load<Texture2D>("Player");

            GameObject player = new(screenCenter, texture, new(0.5f, 0.5f));
            gameObjects.Add(player);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            foreach (GameObject obj in gameObjects) 
            { 
                obj.Rotate(deltaTime);
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            foreach (GameObject obj in gameObjects) 
            {
                obj.DrawSelf(spriteBatch);
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
