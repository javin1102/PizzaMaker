using System.Collections;
using System.Reflection;
using NUnit.Framework;
using PizzaMaker;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools; 

public class MainTest
{
    [UnityTest]
    public IEnumerator NullCheckInjectedOrderFulfillManagerFromInstantiatedPizzaBox()
    {
        var loadSceneAsync = SceneManager.LoadSceneAsync("Restaurant");
        Assert.IsNotNull(loadSceneAsync); 
        while (!loadSceneAsync.isDone)
            yield return null;

        yield return null;

        var pBox = new GameObject().AddComponent<PizzaBox>();
        var orderFullFillManager = Object.FindAnyObjectByType<OrderFulFillManager>();
        pBox.transform.SetParent(orderFullFillManager.transform);
        Assert.IsNotNull(pBox);
        FieldInfo fieldInfo = typeof(PizzaBox).GetField("orderFulFillManager", BindingFlags.NonPublic | BindingFlags.Instance);
        Assert.IsNotNull(fieldInfo);
    }
    
    
}
