using Gaia.Components;
using Gaia.Utility;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Gaia.Scripts
{
    internal class SpawnAstroids
    {
        private int screenHeight = 0;
        private int screenWidth = 0;

        private float spawnDelay = 0.1f;
        private float timeElapsed = 0;

        private List<Astroid> astroids = new();

        public SpawnAstroids(GraphicsDeviceManager deviceManager) 
        {
            screenHeight = deviceManager.PreferredBackBufferHeight;
            screenWidth = deviceManager.PreferredBackBufferWidth;

            GlobalEvents.OnUpdate += Update;
        }

        private void Update(float deltaTime)
        {
            timeElapsed += deltaTime;

            /*if (timeElapsed > spawnDelay)
            {
                Vector2 spawnPoint = Vector2.Zero;

                Astroid astroid = new();
                astroid.Initialize(ObjectTags.Enemy, spawnPoint, 0, new(0.05f, 0.05f), texture);
                astroids.Add(astroid);

                timeElapsed = 0;

                timeElapsed = 0;
            }*/
        }
    }
}
