using Gaia.Utility.CustomVariables;
using Gaia.Utility;
using Microsoft.Xna.Framework;
using Gaia.Scripts;

namespace Gaia.Main.Scenes
{
    internal class Scene
    {
        //keeps track of player object in current "scene"
        private Player player;
        private SpawnAstroids spawnAstroids;
        public int sceneIndex { get; private set; } = 0;

        public void LoadScene()
        {
            CreatePlayer();
            spawnAstroids = new();
        }

        private void CreatePlayer()
        {
            //get start position and scale, account for different screen resolutions
            Vector2 startPos = Utils.UVToScreenPosition(new(0.5f, 0.5f));
            Vector2 startScale = new(0.1f, 0.1f);

            //create the player
            player = new();

            //set the default values, pass the inputHandler to the player
            player.Initialize(ObjectTags.Player, startPos, 0, startScale, "arrow");
            player.speed = 2;
        }

        // Dispose method to clean up resources and unsubscribe events
        public void Dispose()
        {
            player?.Dispose();
            spawnAstroids?.Dispose();
        }
    }
}
