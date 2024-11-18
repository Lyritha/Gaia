using Gaia.Utility.CustomVariables;
using Gaia.Utility;
using Gaia.Scripts;
using Microsoft.Xna.Framework;

namespace Gaia.Main.Scenes
{
    internal class Scene_Level_1 : Scene_Template
    {
        //keeps track of player object in current "scene"
        private Player player;
        private SpawnAstroids spawnAstroids;

        public override void LoadScene()
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
    }
}
