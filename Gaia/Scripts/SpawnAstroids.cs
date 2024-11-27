using Gaia.Components;
using Gaia.Scripts.Objects;
using Gaia.Utility;
using Gaia.Utility.CustomVariables;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Gaia.Scripts
{
    internal class SpawnAstroids : IDisposable
    {
        private int screenHeight = 0;
        private int screenWidth = 0;

        private float spawnDelay = 0.5f;
        private float timeElapsed = 0;

        Random random;

        public List<PhysicsGameObject> astroids { get; private set; } = new();

        public SpawnAstroids()
        {
            screenHeight = GraphicsManager.GraphicsDeviceManager.PreferredBackBufferHeight;
            screenWidth = GraphicsManager.GraphicsDeviceManager.PreferredBackBufferWidth;

            random = new Random();

            GlobalEvents.OnUpdate += Update;
        }

        private void Update(float deltaTime)
        {
            timeElapsed += deltaTime;

            if (timeElapsed > spawnDelay)
            {
                SpawnNewAstroid();
                timeElapsed = 0;
            }

            for (int i = astroids.Count - 1; i >= 0; i--)
            {
                PhysicsGameObject astroid = astroids[i];

                if (Utils.IsOutOfBounds(astroid.transform.position))
                {
                    astroid.Dispose();
                    astroids.Remove(astroid);
                }
            }
        }

        private void SpawnNewAstroid()
        {
            int randX = random.Next(0, screenWidth + 1);
            int randY = random.Next(0, screenHeight + 1);

            // Select spawn point and rotation based on random wall
            (Vector2 spawnPoint, float rotation) = random.Next(0, 4) switch
            {
                0 => (new Vector2(0, randY), MathHelper.ToRadians(90)),  // left wall, rotate to the right
                1 => (new Vector2(randX, 0), MathHelper.ToRadians(180)),  // top wall, rotate downward
                2 => (new Vector2(screenWidth, randY), MathHelper.ToRadians(270)),  // right wall, rotate to the left
                3 => (new Vector2(randX, screenHeight), MathHelper.ToRadians(0)),  // bottom wall, rotate upward
                _ => (Vector2.Zero, 0)  // default case (should never be hit)
            };

            //add varying rotation to astroids
            rotation += MathHelper.ToRadians(RandomFloat(-30, 30));

            //add varying size to astroids
            float randomSize = RandomFloat(0.03f, 0.07f);

            //spawn the actual astroid
            Astroid astroid = new(this);
            astroid.Initialize(ObjectTags.Enemy, spawnPoint, rotation, new(randomSize, randomSize), "WhiteSquare");
            astroid.physics.AddForce(astroid.transform.Forward() * 50, ForceType.Impulse);

            //add the astroid to the list
            astroids.Add(astroid);
        }

        public void RemoveAstroid(PhysicsGameObject obj)
        {
            astroids.Remove(obj);
            obj.Dispose();
        }

        private float RandomFloat(float min, float max) => (float)(random.NextDouble() * (max - min) + min);

        // Override Dispose to clean up projectiles
        public void Dispose()
        {
            GlobalEvents.OnUpdate -= Update;

            random = null;

            // Dispose all projectiles and clear the list
            foreach (Astroid astroid in astroids) astroid.Dispose();
            astroids.Clear();
        }

    }
}
