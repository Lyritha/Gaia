using Gaia.Utility.CustomVariables;
using Microsoft.Xna.Framework;
using System;

namespace Gaia.Components
{
    internal class Physics2D : IDisposable
    {
        //references
        private readonly Transform transformRef;


        //parameters
        public float maxVelocity = 100; // Max velocity limit
        public Vector2 Velocity { get; private set; } = Vector2.Zero;
        public float Mass = 1; // Default mass
        public float DragCoefficient { get; private set; } = 0.001f; // Default drag coefficient (this can be tuned)



        public Physics2D(Transform transform)
        {
            transformRef = transform;
            GlobalEvents.OnUpdate += Update;
        }

        public void Update(float deltaTime)
        {
            ApplyDrag();

            //apply the forces to the object
            transformRef.position += Velocity * GraphicsManager.ResolutionScaling * deltaTime * 10;
        }

        public void AddForce(Vector2 force) => AddForce(force, ForceType.Acceleration);
        public void AddForce(Vector2 force, ForceType forceType)
        {
            switch (forceType)
            {
                case ForceType.Acceleration: Acceleration(force); break;
                case ForceType.VelocityChange: VelocityChange(force); break;
                case ForceType.Impulse: Impulse(force); break;
                default: throw new ArgumentOutOfRangeException(nameof(forceType), forceType, "Invalid ForceType specified.");
            }
        }



        // force types

        private void Acceleration(Vector2 force)
        {
            Velocity +=  force / Mass;
            Velocity = Velocity.Length() > maxVelocity ? Vector2.Normalize(Velocity) * maxVelocity : Velocity;
        }

        private void VelocityChange(Vector2 force)
        {
            // Directly set the velocity (but still clamped by maxVelocity)
            Velocity = force.Length() > maxVelocity ? Vector2.Normalize(force) * maxVelocity : force;
        }

        private void Impulse(Vector2 force)
        {
            Velocity += force / Mass;
        }



        // drag

        private void ApplyDrag()
        {
            // Drag force is proportional to velocity and the drag coefficient
            Vector2 dragForce = -Velocity * DragCoefficient;

            // Apply the drag force
            AddForce(dragForce, ForceType.Acceleration);
        }



        // other

        public void Dispose()
        {
            // Unsubscribe from global events
            GlobalEvents.OnUpdate -= Update;
        }
    }
}
