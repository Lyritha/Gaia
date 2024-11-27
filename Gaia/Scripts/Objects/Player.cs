using Gaia.Components;
using Gaia.Utility;
using Gaia.Utility.CustomVariables;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Gaia.Scripts.Objects
{
    internal class Player : PhysicsGameObject
    {
        public event Action<int> OnPlayerTakeDamage;

        private int health = 3;
        public float speed = 2;
        private float timeElapsed = 0;
        private readonly List<Projectile> projectiles = new();

        public override void Initialize(ObjectTags tag, Vector2 position, float rotation, Vector2 scale, string textureName)
        {
            ignoreCollissions.Add(tag);
            ignoreCollissions.Add(ObjectTags.PlayerProjectile);

            base.Initialize(tag, position, rotation, scale, textureName);
        }

        protected override void Update(float deltaTime)
        {
            InputHandler.Update(deltaTime);

            Rotate();
            Shoot(deltaTime);

            base.Update(deltaTime);

            for (int i = projectiles.Count - 1; i >= 0; i--)
            {
                Projectile projectile = projectiles[i];

                if (Utils.IsOutOfBounds(projectile.transform.position))
                {
                    projectiles.Remove(projectile);
                    projectile.Dispose();
                }
            }
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

                physics.AddForce(-(projectile.physics.Velocity * 0.1f / projectile.physics.Mass), ForceType.Impulse);

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

        public override void OnCollisionStarted(CollisionData collisionData)
        {
            //make sure to do base first, so that the collision state is set properly
            base.OnCollisionStarted(collisionData);

            //if enemy or enemy projectile hits you, then "die"
            if (collisionData.tag == ObjectTags.Enemy || collisionData.tag == ObjectTags.EnemyProjectile)
            {
                spriteColor = Color.Red;
                health--;

                if (health <= 0) SceneManager.LoadScene(SceneManager.Scenes.GameOver);

                OnPlayerTakeDamage?.Invoke(health);
            }
        }

        public override void OnCollisionStopped()
        {
            spriteColor = Color.White;

            base.OnCollisionStopped();
        }
    }
}
