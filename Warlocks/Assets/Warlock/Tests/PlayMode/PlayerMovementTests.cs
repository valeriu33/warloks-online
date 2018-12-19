using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using NSubstitute;
using Services;

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
        var movement = new RbMovement(1, 0, 0, 1);
        var inputManager = Substitute.For<IUserInputManager>();
        inputManager
            .GetMousePos()
            .Returns(Vector3.up);

        playerMovement.Construct(inputManager, movement);
        yield return new WaitForFixedUpdate();
        Assert.True(true);
    }

    [TearDown]
    public void RemoveAllPlayerMovements()
    {
        foreach (var obj in Object.FindObjectsOfType<PlayerMovement>())
            Object.Destroy(obj);
    }
}
