using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;


public class TestAreaDetector
{
    // A Test behaves as an ordinary method
    [Test]
    public void TestAreaDetectorSimplePasses()
    {
        GameObject gameObject = new GameObject();
        AreaDetector area_detector = gameObject.AddComponent<AreaDetector>();
        Debug.Log(area_detector.GetShrinkSpeed());
        Assert.IsTrue(area_detector.GetShrinkSpeed() > 0);
        // Use the Assert class to test conditions
    }

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator TestAreaDetectorWithEnumeratorPasses()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        yield return null;
    }
}
