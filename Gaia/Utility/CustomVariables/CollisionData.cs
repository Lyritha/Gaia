using Gaia.Components;
using Microsoft.Xna.Framework;

namespace Gaia.Utility.CustomVariables
{
    internal struct CollisionData
    {
        public PhysicsGameObject collidingObject;
        public ObjectTags tag;
        public Vector2 collisionDir;

        public CollisionData(PhysicsGameObject collidingObject, Vector2 collisionDir)
        {
            this.collidingObject = collidingObject;
            tag = collidingObject.Tag;
            this.collisionDir = collisionDir;


        }
    }
}
