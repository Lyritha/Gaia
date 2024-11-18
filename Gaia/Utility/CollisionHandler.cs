using Gaia.Components;
using Gaia.Utility.CustomVariables;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Gaia.Utility
{
    internal static class CollisionHandler
    {
        public static List<PhysicsGameObject> collisionObjects = new();
        private static HashSet<(PhysicsGameObject, PhysicsGameObject)> collidingPairs = new();

        public static void Update(float deltaTime)
        {
            CheckAllCollisions();
        }

        private static void CheckAllCollisions()
        {
            // Iterate over all the collisions
            for (int currentIndex = 0; currentIndex < collisionObjects.Count; currentIndex++)
                for (int targetIndex = currentIndex + 1; targetIndex < collisionObjects.Count; targetIndex++)
                    CheckCollision(collisionObjects[currentIndex], collisionObjects[targetIndex]);
        }

        private static void CheckCollision(PhysicsGameObject currentObject, PhysicsGameObject targetObject)
        {
            // Define the current pair
            (PhysicsGameObject, PhysicsGameObject) pair = (currentObject, targetObject);

            Vector2 currentObjPos = currentObject.transform.position;
            Vector2 targetObjPos = targetObject.transform.position;

            // Check if the objects are colliding (based on distance and radii)
            bool isColliding = Vector2.Distance(currentObjPos, targetObjPos) < currentObject.colliderRadius + targetObject.colliderRadius;

            if (isColliding)
            {
                // Calculate direction vectors
                Vector2 directionToCurrent = currentObject.transform.position - targetObject.transform.position;
                Vector2 directionToTarget = targetObject.transform.position - currentObject.transform.position;

                directionToCurrent.Normalize();
                directionToTarget.Normalize();

                // Create collision data
                CollisionData currentObjectData = new(targetObject, directionToCurrent);
                CollisionData targetObjectData = new(currentObject, directionToTarget);

                // If the object pair doesn't already exist, add the pair
                if (!collidingPairs.Contains(pair))
                {
                    //only call OnCollisionStarted if this object is not yet in a pair
                    if (!CheckIfInPairs(currentObject)) currentObject.OnCollisionStarted(currentObjectData);
                    if (!CheckIfInPairs(targetObject)) targetObject.OnCollisionStarted(targetObjectData);

                    // Apply the collision forces
                    ApplyCollisionForces(currentObject, targetObject);

                    //add this pair to the list
                    collidingPairs.Add(pair);
                }
                else
                {
                    currentObject.OnColliding(currentObjectData);
                    targetObject.OnColliding(targetObjectData);
                }
            }
            else
            {
                collidingPairs.Remove(pair);

                // Call OnCollisionStopped only if the objects were previously colliding
                if (!CheckIfInPairs(currentObject)) currentObject.OnCollisionStopped();
                if (!CheckIfInPairs(targetObject)) targetObject.OnCollisionStopped();
            }
        }


        private static void ApplyCollisionForces(PhysicsGameObject currentObject, PhysicsGameObject targetObject)
        {
            //if either objects aren't affected by collision forces skip the check
            if (!currentObject.affectedByCollision || !targetObject.affectedByCollision) return;

            // Get the velocity of the colliding object (target)
            Vector2 otherObjectVelocity = targetObject.physics.Velocity;

            // Calculate the relative velocity: This is the difference between the two objects' velocities
            Vector2 relativeVelocity = otherObjectVelocity - currentObject.physics.Velocity;

            // Calculate the direction of the collision (normalized)
            Vector2 collisionDir = targetObject.transform.position - currentObject.transform.position;
            collisionDir.Normalize();

            // Compute a force based on relative velocity and masses
            // Basic idea: Force = mass * relative velocity. This is simplified to impulse transfer.
            float massRatio = targetObject.physics.Mass / (currentObject.physics.Mass + targetObject.physics.Mass);

            // Scale the force applied based on the relative velocity and mass ratio
            Vector2 force = collisionDir * relativeVelocity.Length() * massRatio * 1;  // Scaling factor (you can adjust this)

            // Apply the force as an impulse to the current object
            currentObject.physics.AddForce(-force, ForceType.Impulse);

            // Apply the opposite force to the target object (so both objects are affected)
            targetObject.physics.AddForce(force, ForceType.Impulse);
        }

        private static bool CheckIfInPairs(PhysicsGameObject obj)
        {
            foreach ((PhysicsGameObject, PhysicsGameObject) existingPair in collidingPairs)
                if (existingPair.Item1 == obj || existingPair.Item2 == obj) return true;

            return false;
        }

        public static void Dispose()
        {
            GlobalEvents.OnUpdate -= Update;
        }
    }
}
