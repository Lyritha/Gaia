using Gaia.Utility.CustomVariables;
using Gaia.Utility;
using Microsoft.Xna.Framework;
using Gaia.Scripts;

namespace Gaia.Main.Scenes
{
    internal class Scene_Template
    {
        public int sceneIndex { get; private set; } = 0;

        public virtual void LoadScene()
        {
        }

        // Dispose method to clean up resources and unsubscribe events
        public virtual void Dispose()
        {
        }
    }
}
