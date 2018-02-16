using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class CoroutineTest {

	[Test]
	public void CoroutineTestSimplePasses() {
		// Use the Assert class to test conditions.
	}

	// A UnityTest behaves like a coroutine in PlayMode
	// and allows you to yield null to skip a frame in EditMode
	[UnityTest]
	public IEnumerator WaitTest() {

        var startTime = Time.realtimeSinceStartup;
        var endTime = 0f;
        CoroutineChain.Start
            .Wait(0.1f)
            .Call(() => endTime = Time.realtimeSinceStartup);

        Assert.GreaterOrEqual(endTime, 0.1f);

		// Use the Assert class to test conditions.
		// yield to skip a frame
		yield return null;
	}
}
