
using Gaia.Utility;
using System;

namespace Gaia.Main.UI
{
    internal class UI_Input_Listener : UI_Element
    {
        public override void Update(float deltaTime)
        {
            InputHandler.Update(deltaTime);

            if (InputHandler.IsMouseRightDown) SceneManager.LoadScene(SceneManager.Scenes.Level);
        }
    }
}
