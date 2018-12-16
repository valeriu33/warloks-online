using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class PlayerMovementTests
    {
        [Test]
        public void TestRotateToPos()
        {
            float zRot;

            var transform = new GameObject().transform;
            var playerMovement = transform.gameObject.AddComponent<PlayerMovement>();

            var rotateToPosMethod = playerMovement
                .GetType()
                .GetMethod("RotateToPos", BindingFlags.NonPublic | BindingFlags.Instance);

            var testCases = new[]
            {
                new {Pos = Vector3.up,      Rot = 0f },
                new {Pos = Vector3.left,    Rot = 90f },
                new {Pos = Vector3.down,    Rot = 180f },
                new {Pos = Vector3.right,   Rot = 270f }
            };

            foreach (var testCase in testCases)
            {
                rotateToPosMethod.Invoke(playerMovement, new object[] { testCase.Pos });
                zRot = transform.rotation.eulerAngles.z;
                Assert.True(Mathf.Approximately(zRot, testCase.Rot));
            }
        }

        [TearDown]
        public void RemoveObjsWithPlayerMovement()
        {
            foreach (var obj in Object.FindObjectsOfType<PlayerMovement>())
                Object.Destroy(obj);
        }
    }
}
