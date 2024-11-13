using Gaia.Components;
using Gaia.Utility;
using Gaia.Utility.CustomVariables;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace Gaia.Scripts
{
    internal class SpawnAstroids
    {
        private int screenHeight = 0;
        private int screenWidth = 0;

        private float spawnDelay = 0.5f;
        private float timeElapsed = 0;

        Random random;
        private List<Astroid> astroids = new();

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
            float min = -30;
            float max = 30;
            rotation += MathHelper.ToRadians((float)(random.NextDouble() * (max - min) + min));

            //spawn the actual astroid
            Astroid astroid = new();
            astroid.Initialize(ObjectTags.Enemy, spawnPoint, rotation, new(0.05f, 0.05f), "WhiteSquare");
            astroid.speed = 50;

            //add the astroid to the list
            astroids.Add(astroid);
        }

        // Override Dispose to clean up projectiles
        public void Dispose()
        {
            GlobalEvents.OnUpdate -= Update;

            // Dispose all projectiles and clear the list
            foreach (Astroid astroid in astroids) astroid.Dispose();
            astroids.Clear();
        }
    }
}
