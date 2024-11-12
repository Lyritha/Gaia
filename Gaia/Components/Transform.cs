using Microsoft.Xna.Framework;
using System;

namespace Gaia.Components
{
    internal record Transform
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



        /// <summary>
        /// Get the forward direction of the object depending on it's current rotation a rotation of 0 makes it move upwards
        /// </summary>
        /// <returns>direction of local forward vector</returns>
        public Vector2 Forward() => new((float)Math.Sin(rotation), -(float)Math.Cos(rotation));

        public void LookAt(Vector2 lookAtTarget)
        {
            Vector2 dir = lookAtTarget - position;
            rotation = (float)Math.Atan2(dir.X, -dir.Y);
        }

    }
}
