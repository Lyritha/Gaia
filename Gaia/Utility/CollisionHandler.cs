using Gaia.Components;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace Gaia.Utility
{
    internal static class CollisionHandler
    {
        public static List<GameObject> collisionObjects = new();

        public static void Update(float delaTime)
        {
            CheckAllCollision();
        }

        private static void CheckAllCollision()
        {
            //go through all the objects currently assigned to the collision check
            for (int currentIndex = 0; currentIndex < collisionObjects.Count; currentIndex++)
            {
                //grab the current object
                GameObject currentObject = collisionObjects[currentIndex];

                //go through all the target objects, use calculated default value to avoid double checks
                for (int targetIndex = currentIndex + 1; targetIndex < collisionObjects.Count; targetIndex++)
                {
                    //grab target object
                    GameObject targetObject = collisionObjects[targetIndex];

                    // Skip objects with the same tag
                    if (currentObject.Tag == targetObject.Tag) continue;

                    // Check if the distance is below the threshold
                    bool isColliding = Vector2.Distance(currentObject.transform.position, targetObject.transform.position) < 300f;

                    //if both objects are colliding with eachother
                    if (isColliding)
                    {
                        //only call collision started if it's not yet colliding with something
                        if (!currentObject.isColliding) currentObject.OnCollisionStarted();
                        else currentObject.OnColliding();

                        if (!targetObject.isColliding) targetObject.OnCollisionStarted();
                        else targetObject.OnColliding();
                    }
                    else
                    {
                        if (currentObject.isColliding) currentObject.OnCollisionStopped();
                        if (targetObject.isColliding) targetObject.OnCollisionStopped();
                    }

                }
            }
        }

        public static void Dispose()
        {
            GlobalEvents.OnUpdate -= Update;
        }
    }
}
