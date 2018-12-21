using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using Shared;

public class RbMovementTests
{
    private Rigidbody2D InitMyTestSubject()
    {
        var gmObj = Object.Instantiate(new GameObject());
        var rb = gmObj.AddComponent<Rigidbody2D>();
        rb.gravityScale = 0;

        return rb;
    }

    [UnityTest]
    public IEnumerator TestIfHeIsMovingInTheRightDirection()
    {
        var rb = InitMyTestSubject();
        var movement = new RbMovement(10, 0, 0, 0);
        movement.MoveTowardsPos(rb, Vector3.up , 10);

        yield return new WaitForFixedUpdate();

        Assert.True(rb.transform.position.y > 0.01);
        Assert.True(Mathf.Approximately(rb.transform.position.x, 0));
    }

    // Test 0 amortization
    [UnityTest]
    public IEnumerator TestFullAmortization()
    {
        var rb = InitMyTestSubject();
        var movement = new RbMovement(
            velocity: 1,
            amortizationTreshold: 2,
            amortizationSpeedMult: 0f,
            posEqualityTreshold: 0);
            
        movement.MoveTowardsPos(rb, Vector3.left, 1);

        yield return new WaitForFixedUpdate();
        Assert.True(Mathf.Approximately(rb.velocity.magnitude, 0));
    }

    // Test if the amortization speed mult is applied correctly.
    [UnityTest]
    public IEnumerator TestAmortizationAmount()
    {
        var rb = InitMyTestSubject();
        var movement = new RbMovement(
            velocity: 1,
            amortizationTreshold: 1,
            amortizationSpeedMult: 0.2f,
            posEqualityTreshold: 0);

        movement.MoveTowardsPos(rb, Vector3.right * 2, 1);
        yield return new WaitForFixedUpdate();

        var velBeforeAmortization = rb.velocity.magnitude;

        rb.velocity = Vector3.zero;
        rb.transform.position = Vector3.zero;

        movement.MoveTowardsPos(rb, Vector3.right * 0.9f, 1);
        yield return new WaitForFixedUpdate();

        var amortizatedVel = rb.velocity.magnitude;

        Assert.True(Mathf.Approximately(
            velBeforeAmortization * movement.amortizationSpeedMult,
            amortizatedVel));
    }

    [UnityTest]
    public IEnumerator TestPosEqualityTreshold()
    {
        var rb = InitMyTestSubject();
        var movement = new RbMovement(
            velocity: 100,
            amortizationTreshold: 1,
            amortizationSpeedMult: 1,
            posEqualityTreshold: 100);

        var destinationReached = movement.MoveTowardsPos(rb, Vector3.down, 1);
        Assert.IsTrue(destinationReached);
        yield return new WaitForFixedUpdate();
        Assert.True(Mathf.Approximately(rb.velocity.magnitude, 0));
    }

    [TearDown]
    public void RemoveAllTheRbs()
    {
        foreach (var rb in Object.FindObjectsOfType<Rigidbody2D>())
            Object.Destroy(rb.gameObject);
    }
}
