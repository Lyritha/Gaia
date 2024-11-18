using Gaia.Components;
using Gaia.Utility;
using Gaia.Utility.CustomVariables;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace Gaia.Scripts.Objects
{
    internal class Player : PhysicsGameObject
    {
        public float speed = 15;
        private float timeElapsed = 0;
        private readonly List<Projectile> projectiles = new();

        public override void Initialize(ObjectTags tag, Vector2 position, float rotation, Vector2 scale, string textureName)
        {
            affectedByCollision = false;
            base.Initialize(tag, position, rotation, scale, textureName);
            physics.Mass = 10;
        }

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
                projectile.Initialize(ObjectTags.PlayerProjectile, spawnPoint, transform.rotation, new(0.05f, 0.05f), "WhiteSquare");
                projectile.physics.AddForce(projectile.transform.Forward() * 100, ForceType.Impulse);
                projectiles.Add(projectile);

                physics.AddForce(-(projectile.physics.Velocity / projectile.physics.Mass), ForceType.Impulse);

                timeElapsed = 0;
            }
        }

        public override void Dispose()
        {
            // Dispose of projectiles
            foreach (var projectile in projectiles)
                projectile.Dispose();

            projectiles.Clear();

            // Call the base class Dispose
            base.Dispose();
        }

        public override void OnColliding(CollisionData collisionData)
        {
            //make sure to do base first, so that the collision state is set properly
            base.OnColliding(collisionData);

            //if enemy or enemy projectile hits you, then "die"
            if (collisionData.tag == ObjectTags.Enemy || collisionData.tag == ObjectTags.EnemyProjectile)
            {
                spriteColor = Color.Red;
            }
        }

        public override void OnCollisionStopped()
        {
            spriteColor = Color.White;

            base.OnCollisionStopped();
        }
    }
}
