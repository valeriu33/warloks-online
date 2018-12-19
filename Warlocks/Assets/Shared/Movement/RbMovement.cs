using UnityEngine;
using Zenject;

namespace Services
{
    public class RbMovement : IMovement
    {
        // In other words - the speed.
        public float Velocity { get; private set; }

        // Distance from where to start amortization.
        public float AmortizationTreshold { get; private set; }
        public float AmortizationSpeedMult { get; private set; }

        // Defines the treshold of equality between two numbers.
        public float PosEqualityTreshold { get; private set; }

        public RbMovement(
            float velocity,
            float amortizationTreshold,
            float amortizationSpeedMult,
            float posEqualityTreshold)
        {
            this.Velocity = velocity;
            this.AmortizationTreshold = amortizationTreshold;
            this.AmortizationSpeedMult = amortizationSpeedMult;
            this.PosEqualityTreshold = posEqualityTreshold;
        }

        private Vector3 ComputeForce(Vector3 delta, float deltaTime)
        {
            var direction = delta.normalized;
            var finalForce = direction * Velocity * deltaTime;

            if (delta.magnitude < AmortizationTreshold)
                finalForce *= AmortizationSpeedMult;

            return finalForce;
        }

        public bool MoveTowardsPos(Rigidbody2D rb, Vector3 pos, float deltaTime)
        {
            var delta = pos - rb.transform.position;
            delta.z = 0;

            if (Mathf.Abs(delta.magnitude) < PosEqualityTreshold)
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