using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using NSubstitute;
using Shared;

public class PlayerMovementTests
{
    private PlayerMovement InitTestSubject()
    {
        var gmObj = Object.Instantiate(new GameObject());
        var rb = gmObj.AddComponent<Rigidbody2D>();
        var playerMovement = gmObj.AddComponent<PlayerMovement>();
        rb.gravityScale = 0;

        return playerMovement;
    }

    [UnityTest]
    public IEnumerator TestPlayerMovesToClick()
    {
        var playerMovement = InitTestSubject();
        var movement = new RbMovement(10, 0, 0, 1);
        var inputManager = Substitute.For<IUserInputManager>();
        inputManager
            .GetMousePos()
            .Returns(Vector3.down * 10);
        inputManager
            .GetMouseButton(1)
            .Returns(true);
        playerMovement.Construct(inputManager, movement);

        // Wait for Start()
        yield return null;

        // Wait for it to move.
        yield return new WaitForFixedUpdate();

        var rb = playerMovement.GetComponent<Rigidbody2D>();
        Assert.True(rb.velocity.y < -0.001);
    }

    [UnityTest]
    public IEnumerator TestPlayerRotatesToMouse()
    {
        var playerMovement = InitTestSubject();
        var movement = new RbMovement(0, 0, 0, 0);
        var inputManager = Substitute.For<IUserInputManager>();
        inputManager
            .GetMousePos()
            .Returns(Vector3.down);
        playerMovement.Construct(inputManager, movement);

        // Wait for Start()
        yield return null;

        // Wait for it to move
        yield return new WaitForFixedUpdate();
        Assert.True(Mathf.Approximately(playerMovement.transform.eulerAngles.z, 180));
    }

    [TearDown]
    public void RemoveAllPlayerMovements()
    {
        foreach (var obj in Object.FindObjectsOfType<PlayerMovement>())
            Object.Destroy(obj);
    }
}
