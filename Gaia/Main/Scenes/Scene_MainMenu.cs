
using Gaia.Main.UI;

namespace Gaia.Main.Scenes
{
    internal class Scene_MainMenu : Scene_Template
    {
        //ui
        UI_MainMenu ui;
        

        //keeps track of player object in current "scene"

        public override void LoadScene()
        {

            ui = new UI_MainMenu();
        }

        public override void Dispose()
        {
            ui.Dispose();
            base.Dispose();
        }
    }
}
