using Gaia.Utility.CustomVariables;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Gaia.Components
{
    internal class GameObject
    {
        public ObjectTags Tag { get; private set; }

        public Transform transform;

        public Texture2D texture;

        public virtual void Initialize(ObjectTags tag, Vector2 position, float rotation, Vector2 scale, string textureName)
        {
            Tag = tag;
            texture = GraphicsManager.Content.Load<Texture2D>(textureName);
            transform = new(position, rotation, scale * GraphicsManager.ResolutionScaling);

            GlobalEvents.OnUpdate += Update;
            GlobalEvents.OnDraw += DrawSelf;
        }

        protected virtual void Update(float deltaTime) { }

        /// <summary>
        /// Draws this object on the screen
        /// </summary>
        /// <param name="pSpriteBatch"></param>
        private void DrawSelf(SpriteBatch pSpriteBatch)
        {
            //get pivot point of object
            Vector2 pivotPoint = new(texture.Width / 2, texture.Height / 2);

            //draw the sprite
            pSpriteBatch.Draw(
                texture, 
                transform.position, 
                null, 
                Color.White, 
                transform.rotation, 
                pivotPoint,
                transform.scale, 
                SpriteEffects.None, 1f
                );
        }

        // Dispose method for cleanup
        public virtual void Dispose()
        {
            // Unsubscribe from global events
            GlobalEvents.OnUpdate -= Update;
            GlobalEvents.OnDraw -= DrawSelf;

            // Do NOT dispose of the texture here; let ContentManager handle it
            texture = null;  // Set to null so the reference is cleared
        }
    }
}
