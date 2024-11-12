using Gaia.Utility.CustomVariables;
using Microsoft.Xna.Framework;
using System;

namespace Gaia.Components
{
    internal class Physics2D
    {
        private readonly Transform transformRef;

        public float maxVelocity = 100;
        public Vector2 CurrentVelocity { get; private set; } = Vector2.Zero;

        public Physics2D(Transform transform)
        {
            transformRef = transform;
            GlobalEvents.OnUpdate += Update;
        }

        public void Update(float deltaTime)
        {
            transformRef.position += CurrentVelocity * GraphicsManager.ResolutionScaling * deltaTime * 10;
        }

        public void AddForce(Vector2 force) => AddForce(force, ForceType.Acceleration);
        public void AddForce(Vector2 force, ForceType forceType)
        {
            switch (forceType)
            {
                case ForceType.Acceleration: AddForceAcceleration(force); break;
                case ForceType.VelocityChange: AddForceVelocityChange(force); break;
                default: throw new ArgumentOutOfRangeException(nameof(forceType), forceType, "Invalid ForceType specified.");
            }
        }

        private void AddForceAcceleration(Vector2 force)
        {
            CurrentVelocity += force;
            CurrentVelocity = CurrentVelocity.Length() > maxVelocity ? Vector2.Normalize(CurrentVelocity) * maxVelocity : CurrentVelocity;
        }

        private void AddForceVelocityChange(Vector2 force)
        {
            CurrentVelocity = force.Length() > maxVelocity ? Vector2.Normalize(force) * maxVelocity : force;
        }
    }
}
