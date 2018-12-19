using System.Collections;
using System.Collections.Generic;
using Services;
using UnityEngine;
using Zenject;

public class PlayerMovement : MonoBehaviour
{
    // DI
    private IUserInputManager userInputManager;
    private IMovement movement;

    private bool followLastMouseClick = false;
    private Vector3 targetPosition;
    private Vector3 mousePos;
    private Rigidbody2D rb;

    [Inject]
    public void Construct(
        IUserInputManager userInputManager,
        IMovement movement)
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
        if (userInputManager.GetMouseButton(0))
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
