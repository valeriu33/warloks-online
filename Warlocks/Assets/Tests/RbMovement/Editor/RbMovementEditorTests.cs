using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using Services;

public class RbMovementEditorTests
{
    [Test]
    public void TestRotateToPos()
    {
        var transform = Object.Instantiate(new GameObject()).transform;
        transform.rotation = Quaternion.Euler(0, 0, 1);
        var movement = new RbMovement(1, 0, 0, 0);

        var testCases = new[]
        {
            new { Pos = Vector3.up,     Rot = 0 },
            new { Pos = Vector3.right,  Rot = 270 },
            new { Pos = Vector3.down,   Rot = 180 },
            new { Pos = Vector3.left,   Rot = 90 }
        };

        foreach (var testCase in testCases)
        {
            movement.RotateToPos(transform, testCase.Pos);
            Assert.True(Mathf.Approximately(transform.eulerAngles.z, testCase.Rot));
        }
    }
}
