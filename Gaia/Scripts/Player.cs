using Gaia.Components;
using Gaia.Scripts;
using Gaia.Utility;
using Gaia.Utility.CustomVariables;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Gaia
{
    internal class Player : PhysicsGameObject
    {
        private float timeElapsed = 0;
        private List<Projectile> projectiles = new();

        protected override void Update(float deltaTime)
        {
            InputHandler.Update(deltaTime);

            Rotate();
            Shoot(deltaTime);

            base.Update(deltaTime);
        }

        protected override void Move()
        {
            physics.AddForce(transform.Forward() * InputHandler.Wasd.Y * speed);
            base.Move();
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
                projectile.speed = 50;
                projectiles.Add(projectile);

                physics.AddForce(-transform.Forward() * projectile.speed / 4);

                timeElapsed = 0;
            }
        }

        

        // Override Dispose to clean up projectiles
        public override void Dispose()
        {
            base.Dispose();

            // Dispose all projectiles and clear the list
            foreach (Projectile projectile in projectiles) projectile.Dispose();
            projectiles.Clear();
        }
    }
}
