using System.Collections;
using System.Collections.Generic;
using Shared;
using UnityEngine;
using Zenject;

public class PlayerMovement : MonoBehaviour
{
    // DI
    private IUserInputManager userInputManager;
    [SerializeField] private RbMovement movement;

    private bool followLastMouseClick = false;
    private Vector3 targetPosition;
    private Vector3 mousePos;
    private Rigidbody2D rb;

    [Inject]
    public void Construct(
        IUserInputManager userInputManager,
        RbMovement movement)
    {
        this.userInputManager = userInputManager;
        this.movement = movement;
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update ()
	{
        mousePos = userInputManager.GetMousePos();
        if (userInputManager.GetMouseButton(1))
	    {
	        targetPosition = mousePos;
	        targetPosition.z = 0;
            followLastMouseClick = true;
        }

    }

    private void FixedUpdate()
    {
        movement.RotateToPos(transform, mousePos);

        if (followLastMouseClick)
            followLastMouseClick = !movement.MoveTowardsPos(rb, targetPosition, Time.fixedDeltaTime);
    }
}
