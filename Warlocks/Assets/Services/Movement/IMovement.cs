using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Services
{
    public interface IMovement
    {
        // Return true if the position is reached.
        bool MoveTowardsPos(Rigidbody2D rb, Vector3 pos, float deltaTime);
        void RotateToPos(Transform transform, Vector3 pos);
    }
}