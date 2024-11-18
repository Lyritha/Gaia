using Gaia.Components;
using Gaia.Utility.CustomVariables;
using Microsoft.Xna.Framework;

namespace Gaia.Scripts.Objects
{
    internal class Astroid : PhysicsGameObject
    {
        public override void Initialize(ObjectTags tag, Vector2 position, float rotation, Vector2 scale, string textureName)
        {
            base.Initialize(tag, position, rotation, scale, textureName);
            spriteColor = Color.Gray;
        }
    }
}
