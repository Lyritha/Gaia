using Gaia.Main.Scenes;
using Gaia.Utility;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Gaia
{
    public class GameManager : Game
    {
        //keeps track of how much time elapsed between the previous and current frame
        private float deltaTime = 0;
        private GraphicsDeviceManager graphics;

        public GameManager()
        {
            graphics = new(this);

            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            GraphicsManager.Initialize(this, graphics);
            GlobalEvents.OnUpdate += CollisionHandler.Update;

            SceneManager.LoadScene(Scenes.Level);

            base.Initialize();
        }

        /// <summary>
        /// updates all scripts listening to the event and gives deltaTime to all scripts
        /// </summary>
        /// <param name="gameTime"></param>
        protected override void Update(GameTime gameTime)
        {
            //update the deltaTime for this frame
            deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            // Invoke the static OnUpdate event via GlobalEvents
            GlobalEvents.RaiseOnUpdate(deltaTime);
            GlobalEvents.RaiseOnLateUpdate(deltaTime);

            base.Update(gameTime);
        }


        /// <summary>
        /// minimal amount of drawing functionality to keep this part clean
        /// </summary>
        /// <param name="gameTime"></param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            GraphicsManager.CreateSpriteBatch();
            base.Draw(gameTime);
        }
    }
}
