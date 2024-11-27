using Gaia.Components;
using Gaia.Utility.CustomVariables;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace Gaia.Utility
{
    internal static class CollisionHandler
    {
        public static List<PhysicsGameObject> collisionObjects = new();
        private static HashSet<(PhysicsGameObject, PhysicsGameObject)> collidingPairs = new();

        private static float timer = 0f;
        private static float interval = 0.05f; 

        public static void Update(float deltaTime)
        {
            timer += deltaTime;

            if (timer >= interval)
            {
                timer = 0f;
                CheckAllCollisions();
            }
        }

        public static void RemoveCollisionObject(PhysicsGameObject obj)
        {
            collisionObjects.Remove(obj);

            foreach ((PhysicsGameObject, PhysicsGameObject) existingPair in collidingPairs)
            {
                if (existingPair.Item1 == obj)
                {
                    collidingPairs.Remove(existingPair);

                    if (!CheckIfInPairs(existingPair.Item2))
                        existingPair.Item2.OnCollisionStopped();

                }
                else if (existingPair.Item2 == obj)
                {
                    collidingPairs.Remove(existingPair);

                    if (!CheckIfInPairs(existingPair.Item1))
                        existingPair.Item1.OnCollisionStopped();

                }
            }
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
            //skip collision check if they are in the lists
            if (currentObject.ignoreCollissions.Contains(targetObject.Tag) || targetObject.ignoreCollissions.Contains(currentObject.Tag)) return;

            // Define the current pair
            (PhysicsGameObject, PhysicsGameObject) pair = (currentObject, targetObject);

            Vector2 currentObjPos = currentObject.transform.position;
            Vector2 targetObjPos = targetObject.transform.position;

            // Compare squared distances instead of using Vector2.Distance to avoid sqrt operation
            float squaredDistance = (currentObjPos - targetObjPos).LengthSquared();
            float squaredRadiusSum = (currentObject.colliderRadius + targetObject.colliderRadius) * (currentObject.colliderRadius + targetObject.colliderRadius);

            bool isColliding = squaredDistance < squaredRadiusSum;

            // Handle collision case
            if (isColliding)
            {
                // Calculate and normalize direction vectors only once
                Vector2 directionToCurrent = currentObjPos - targetObjPos;
                Vector2 directionToTarget = targetObjPos - currentObjPos;

                directionToCurrent.Normalize();
                directionToTarget.Normalize();

                // Create collision data
                CollisionData currentObjectData = new(targetObject, directionToCurrent);
                CollisionData targetObjectData = new(currentObject, directionToTarget);

                // Handle new collision pairs (only if not already in pairs)
                if (!collidingPairs.Contains(pair))
                {
                    // Add the pair to the list
                    collidingPairs.Add(pair);

                    currentObject.OnCollisionStarted(currentObjectData);
                    targetObject.OnCollisionStarted(targetObjectData);

                    // Apply the collision forces
                    ApplyCollisionForces(currentObject, targetObject);

                }
                else
                {
                    // Objects are colliding, call OnColliding
                    currentObject.OnColliding(currentObjectData);
                    targetObject.OnColliding(targetObjectData);
                }
            }
            else
            {
                // Remove pair from colliding pairs list
                if (collidingPairs.Contains(pair))
                {
                    collidingPairs.Remove(pair);

                    // Call OnCollisionStopped only if not already in pairs
                    if (!CheckIfInPairs(currentObject))
                        currentObject.OnCollisionStopped();

                    if (!CheckIfInPairs(targetObject))
                        targetObject.OnCollisionStopped();
                }
            }
        }


        private static void ApplyCollisionForces(PhysicsGameObject currentObject, PhysicsGameObject targetObject)
        {
            // If either object isn't affected by collision forces, skip the check
            if (currentObject.isTrigger || targetObject.isTrigger) return;


            // Get velocities and masses to avoid redundant property access
            Vector2 currentVelocity = currentObject.physics.Velocity;
            Vector2 targetVelocity = targetObject.physics.Velocity;
            float currentMass = currentObject.physics.Mass;
            float targetMass = targetObject.physics.Mass;

            // Calculate the relative velocity between the two objects
            Vector2 relativeVelocity = targetVelocity - currentVelocity;

            // Calculate the direction of the collision and normalize it manually (avoiding extra calls to Normalize)
            Vector2 collisionDir = targetObject.transform.position - currentObject.transform.position;
            float dirMagnitudeSquared = collisionDir.X * collisionDir.X + collisionDir.Y * collisionDir.Y;

            // If the magnitude is small, we can avoid division by zero issues
            if (dirMagnitudeSquared > 1e-6f)
            {
                // Avoiding sqrt operation for normalization, just scale the direction components
                float invMagnitude = 1.0f / MathF.Sqrt(dirMagnitudeSquared);
                collisionDir.X *= invMagnitude;
                collisionDir.Y *= invMagnitude;
            } else return;

            // Calculate mass ratio once
            float massRatio = targetMass / (currentMass + targetMass);

            // Calculate the impulse magnitude based on the relative velocity (without sqrt)
            float impulseMagnitude = relativeVelocity.X * collisionDir.X + relativeVelocity.Y * collisionDir.Y;
            impulseMagnitude *= massRatio;

            // Calculate the force vector (avoiding sqrt and unnecessary vector operations)
            Vector2 force = new(collisionDir.X * impulseMagnitude, collisionDir.Y * impulseMagnitude);

            // Apply the forces as impulses
            currentObject.physics.AddForce(force, ForceType.Acceleration);  // Opposite force to the current object
            targetObject.physics.AddForce(-force, ForceType.Acceleration);    // Force applied to the target object
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
