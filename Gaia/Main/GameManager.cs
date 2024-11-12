using Gaia.Utility;
using Gaia.Utility.CustomVariables;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Gaia
{
    public class GameManager : Game
    {
        //keeps track of how much time elapsed between the previous and current frame
        private float deltaTime = 0;

        //used to render the game
        private SpriteBatch spriteBatch;

        //keeps track of all current objects in the "scene"
        private Player player;

        public GameManager()
        {
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            GraphicsDeviceManager graphics = new GraphicsDeviceManager(this);
            GraphicsManager.Initialize(Content, GraphicsDevice, graphics, spriteBatch);
        }

        protected override void Initialize()
        {
            CreatePlayer();

            base.Initialize();
        }

        private void CreatePlayer()
        {
            //get start position and scale, account for different screen resolutions
            Vector2 startPos = Utils.UVToScreenPosition(new(0.5f, 0.5f));
            Vector2 startScale = new Vector2(0.1f, 0.1f);

            //create the player
            player = new();

            //set the default values, pass the inputHandler to the player
            player.Initialize(ObjectTags.Player, startPos, 0, startScale, "arrow");
        }

        protected override void LoadContent() => spriteBatch = new SpriteBatch(GraphicsDevice);

        protected override void Update(GameTime gameTime)
        {
            //update the deltaTime for this frame
            deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            // Invoke the static OnUpdate event via GlobalEvents
            GlobalEvents.RaiseOnUpdate(deltaTime);
            GlobalEvents.RaiseOnLateUpdate(deltaTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            // Raise the OnDraw event to allow global subscribers to draw themselves
            GlobalEvents.RaiseOnDraw(spriteBatch);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
