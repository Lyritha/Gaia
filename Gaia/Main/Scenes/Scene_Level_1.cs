using Gaia.Utility.CustomVariables;
using Gaia.Utility;
using Gaia.Scripts;
using Microsoft.Xna.Framework;
using Gaia.Scripts.Objects;
using System;
using Gaia.Main.UI;

namespace Gaia.Main.Scenes
{
    internal class Scene_Level_1 : Scene_Template
    {
        //ui
        UI_Template ui;

        //keeps track of player object in current "scene"
        private Player player;
        private SpawnAstroids spawnAstroids;

        public override void LoadScene()
        {
            ui = new UI_Template();
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
        }

        public override void Dispose()
        {
            player?.Dispose();
            spawnAstroids?.Dispose();
            base.Dispose();
        }
    }
}
