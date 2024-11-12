using Microsoft.Xna.Framework;

namespace Gaia.Components
{
    public record Transform
    {
        public Vector2 position;
        public float rotation;
        public Vector2 scale;
        public Transform(Vector2 position, float rotation, Vector2 scale) 
        { 
            this.position = position;
            this.rotation = rotation;
            this.scale = scale;
        }

        /// <summary>
        /// set the scale of the transform uniformly
        /// </summary>
        /// <param name="uniformScale"></param>
        public void SetScale(float uniformScale) => SetScale(new Vector2(uniformScale,uniformScale));
        /// <summary>
        /// set the scale of the transform non-uniformly
        /// </summary>
        /// <param name="uniformScale"></param>
        public void SetScale(Vector2 nonUniformScale) => scale = nonUniformScale;
    }
}
