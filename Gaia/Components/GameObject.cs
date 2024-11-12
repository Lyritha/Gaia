using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Gaia.Components
{
    internal class GameObject
    {
        public Transform transform;

        private Vector2 fowardDir;
        private float velocity = 0;

        public Texture2D texture;

        public GameObject(Vector2 position, Texture2D texture, Vector2 scale)
        {
            this.texture = texture;
            transform = new(position, 0, scale);
        }

        public void Update(float deltaTime)
        {
            Rotate(deltaTime);
        }

        public void Destory(Game game)
        {

        }

        public void MoveTowards(float deltaTime)
        {
            transform.position += (fowardDir * velocity * deltaTime);
        }

        public void Rotate(float deltaTime)
        {
            transform.rotation += 1 * deltaTime;
        }


        /// <summary>
        /// Draws this object on the screen
        /// </summary>
        /// <param name="pSpriteBatch"></param>
        public void DrawSelf(SpriteBatch pSpriteBatch, Vector2 resolutionScaling)
        {
            Vector2 scaledScale = transform.scale * resolutionScaling;

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
                scaledScale, 
                SpriteEffects.None, 1f
                );
        }
    }
}
