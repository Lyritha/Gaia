using Gaia.Utility.CustomVariables;
using Gaia.Utility;
using Gaia.Scripts;
using Microsoft.Xna.Framework;
using Gaia.Scripts.Objects;
using System;
using Gaia.Main.UI;

namespace Gaia.Main.Scenes
{
    internal class Scene_GameOver : Scene_Template
    {
        public int score = 0;

        //ui
        UI_GameOver ui;

        //keeps track of player object in current "scene"

        public override void LoadScene()
        {
            ui = new UI_GameOver();

        }

        public override void Dispose()
        {
            ui.Dispose();
            base.Dispose();
        }
    }
}
