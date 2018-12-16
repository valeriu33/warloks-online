using UnityEngine;
using Utils;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 1f;

    // How big can be the difference between the desired point and
    // current pos.
    [SerializeField] private float posEqualityTreshold = 0.2f;

    [Header("Amortization")]

    // Distance from where to start amortization.
    [SerializeField] private float amortizationTreshold = 10f;
    [SerializeField] private float amortizationSpeedMult = 0.4f;

    // This velocity is used only by SmoothDamp.
    private Vector3 velocity;

    private bool followLastMouseClick = false;
    private Vector3 targetPosition;
    private Vector3 mousePos;
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update ()
	{
        mousePos = MouseTools.GetMousePos();
        if (Input.GetMouseButton(0))
	    {
	        targetPosition = mousePos;
	        targetPosition.z = 0;
            followLastMouseClick = true;
        }
	}

    private void FixedUpdate()
    {
        RotateToPos(mousePos);

        if (followLastMouseClick)
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

        if (Mathf.Abs(delta.magnitude) < posEqualityTreshold)
        {
            followLastMouseClick = false;
            return;
        }

        var direction = delta.normalized;
        var finalForce = direction * speed * deltaTime;

        if (delta.magnitude < amortizationTreshold)
            finalForce *= amortizationSpeedMult;

        rb.AddForce(finalForce);
    }
}
