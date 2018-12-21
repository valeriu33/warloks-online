using System;
using UnityEngine;
using Zenject;

namespace Shared
{
    [Serializable]
    public class RbMovement
    {
        // In other words - the speed.
        public float velocity;

        // Distance from where to start amortization.
        public float amortizationTreshold;
        public float amortizationSpeedMult;

        // Defines the treshold of equality between two numbers.
        public float posEqualityTreshold;

        public RbMovement(
            float velocity,
            float amortizationTreshold,
            float amortizationSpeedMult,
            float posEqualityTreshold)
        {
            this.velocity = velocity;
            this.amortizationTreshold = amortizationTreshold;
            this.amortizationSpeedMult = amortizationSpeedMult;
            this.posEqualityTreshold = posEqualityTreshold;
        }

        private Vector3 ComputeForce(Vector3 delta, float deltaTime)
        {
            var direction = delta.normalized;
            var finalForce = direction * velocity * deltaTime;

            if (delta.magnitude < amortizationTreshold)
                finalForce *= amortizationSpeedMult;

            return finalForce;
        }

        public bool MoveTowardsPos(Rigidbody2D rb, Vector3 pos, float deltaTime)
        {
            var delta = pos - rb.transform.position;
            delta.z = 0;

            if (Mathf.Abs(delta.magnitude) < posEqualityTreshold)
                return true;

            rb.AddForce(ComputeForce(delta, deltaTime));
            return false;
        }

        public void RotateToPos(Transform transform, Vector3 pos)
        {
            Vector3 diff = pos - transform.position;
            diff.Normalize();

            float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);
        }
    }
}