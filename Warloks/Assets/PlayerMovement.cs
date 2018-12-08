using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    [SerializeField] private float speed = 1f;

    [Header("Amortization")]
    [SerializeField] private float amortizationTreshold = 10f;
    [SerializeField] private float amortizationSpeedMult = 0.4f;

    private Vector3 velocity = Vector3.zero;
    private Vector3 targetPosition;
    private Vector3 mousePos;

	void Update ()
	{
	    mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (Input.GetMouseButton(0))
	    {
	        targetPosition = mousePos;
	        targetPosition.z = 0;
	    }
	}

    private void FixedUpdate()
    {
        RotateToPos(mousePos);
        MoveTowardsPos(targetPosition, Time.fixedDeltaTime);
    }

    private void RotateToPos(Vector3 pos)
    {
        Vector3 diff = pos - transform.position;
        diff.Normalize();

        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);
    }

    private void MoveTowardsPos(Vector3 pos, float deltaTime)
    {
        var delta = pos - transform.position;
        delta.z = 0;

        if (Mathf.Approximately(delta.magnitude, 0))
            return;

        if (delta.magnitude < amortizationTreshold)
        {
            transform.position = Vector3.SmoothDamp(
                transform.position,
                targetPosition,
                ref velocity,
                1 / (amortizationSpeedMult * speed));
        }
        else
        {
            var distance = speed * deltaTime;
            var finalDistance = 0f;

            if (delta.magnitude < distance)
                finalDistance = delta.magnitude;
            else
                finalDistance = distance;

            var direction = delta.normalized;
            transform.position = transform.position + direction * finalDistance;
        }
    }
}
