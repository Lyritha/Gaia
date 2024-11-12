using Gaia.Components;
using Gaia.Scripts;
using Gaia.Utility;
using Gaia.Utility.CustomVariables;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Gaia
{
    internal class Player : GameObject
    {
        private Physics2D physics;
        private float speed = 1;

        private float timeElapsed = 0;
        private List<Projectile> projectiles = new();

        public override void Initialize(ObjectTags tag, Vector2 position, float rotation, Vector2 scale, string textureName)
        {
            base.Initialize(tag, position, rotation, scale, textureName);
            physics = new(transform);
        }

        protected override void Update(float deltaTime)
        {
            InputHandler.Update(deltaTime);

            Move();
            Rotate();
            Shoot(deltaTime);

            base.Update(deltaTime);
        }

        // Method to move the object based on velocity and deltaTime
        private void Move()
        {
            physics.AddForce(transform.Forward() * InputHandler.Wasd.Y * speed);
        }

        private void Rotate()
        {
            transform.LookAt(InputHandler.MousePos);
        }

        private void Shoot(float deltaTime)
        {
            timeElapsed += deltaTime;

            if (timeElapsed > 0.5f && InputHandler.IsMouseLeftDown) 
            {
                Vector2 spawnPoint = transform.position + transform.Forward() * (texture.Height * transform.scale.Y / 2);

                Projectile projectile = new();
                projectile.Initialize(ObjectTags.PlayerProjectile, spawnPoint, transform.rotation, new(0.05f,0.05f), "WhiteSquare");
                projectiles.Add(projectile);

                physics.AddForce(-transform.Forward() * 5);

                timeElapsed = 0;
            }
        }
    }
}
