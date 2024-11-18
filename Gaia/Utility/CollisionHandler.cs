using Gaia.Components;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Gaia.Utility
{
    internal static class CollisionHandler
    {
        public static List<GameObject> collisionObjects = new();
        private static HashSet<(GameObject, GameObject)> collidingPairs = new();

        public static void Update(float delaTime)
        {
            CheckAllCollisions();
        }

        private static void CheckAllCollisions()
        {
            // create a new hashset
            HashSet<(GameObject,GameObject)> newCollidingPairs = new();

            // go through all the collissionObjects
            for (int currentIndex = 0; currentIndex < collisionObjects.Count; currentIndex++)
            {
                // grab the first object (current object)
                GameObject currentObject = collisionObjects[currentIndex];

                // go through all the collission objects again, making sure to avoid checking the first object
                for (int targetIndex = currentIndex + 1; targetIndex < collisionObjects.Count; targetIndex++)
                {
                    // grab the second object (target object)
                    GameObject targetObject = collisionObjects[targetIndex];

                    // Check if the objects are colliding
                    bool isColliding = Vector2.Distance(currentObject.transform.position, targetObject.transform.position) < 300f;

                    // grab the 2 objects and put them in one var (matching the hashSet)
                    var pair = (currentObject, targetObject);

                    if (isColliding)
                    {
                        //if the objects aren't already colliding together
                        if (!collidingPairs.Contains(pair))
                        {
                            // Collision Start
                            currentObject.OnCollisionStarted(targetObject.Tag);
                            targetObject.OnCollisionStarted(currentObject.Tag);
                        }
                        else
                        {
                            // Collision Stay
                            currentObject.OnColliding(targetObject.Tag);
                            targetObject.OnColliding(currentObject.Tag);
                        }

                        // Add to the new colliding pairs set
                        newCollidingPairs.Add(pair);
                    }
                    else if (collidingPairs.Contains(pair))
                    {
                        // Collision Stop
                        currentObject.OnCollisionStopped(targetObject.Tag);
                        targetObject.OnCollisionStopped(currentObject.Tag);
                    }
                }
            }

            // Update colliding pairs
            collidingPairs = newCollidingPairs;

            // Ensure objects not in collidingPairs are reset (if needed)
            foreach (var obj in collisionObjects)
            {
                obj.isColliding = newCollidingPairs.Any(pair => pair.Item1 == obj || pair.Item2 == obj);
            }
        }

        public static void Dispose()
        {
            GlobalEvents.OnUpdate -= Update;
        }
    }
}
