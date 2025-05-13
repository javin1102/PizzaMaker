using System.Collections;
using System.Collections.Generic;
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
        // Setup
        var loadSceneAsync = SceneManager.LoadSceneAsync("Restaurant");
        Assert.IsNotNull(loadSceneAsync);

        while (!loadSceneAsync.isDone)
            yield return null;


        yield return null; // Wait an additional frame for initialization
        yield return null; // Wait an additional frame for initialization


        // Arrange
        var pizzaBox = Object.FindFirstObjectByType<PizzaBox>();
        var orderFullFillManager = Object.FindAnyObjectByType<OrderFulFillManager>();

        try
        {
            pizzaBox = Object.Instantiate(pizzaBox, orderFullFillManager.transform, true);
            // Act

            // Assert
            Assert.IsNotNull(pizzaBox, "PizzaBox component should not be null");

            yield return null; // Wait an additional frame for initialization
            Assert.IsNotNull(pizzaBox.orderFulFillManager, "Injected OrderFulFillManager should not be null");
        }
        finally
        {
            // Cleanup
            if (pizzaBox != null)
                Object.Destroy(pizzaBox.gameObject);
        }
    }

    [UnityTest]
    public IEnumerator CheckOrderFulfillManager()
    {
        var loadSceneAsync = SceneManager.LoadSceneAsync("Restaurant");
        Assert.IsNotNull(loadSceneAsync);

        while (!loadSceneAsync.isDone)
            yield return null;

        yield return null; // Wait an additional frame for initialization
        yield return null; // Wait an additional frame for initialization

        yield return null;
        var pizzaBox = Object.FindFirstObjectByType<PizzaBox>();
        var orderFullFillManager = Object.FindAnyObjectByType<OrderFulFillManager>();
        var pizzaBox1 = Object.Instantiate(pizzaBox, orderFullFillManager.transform, true);
        var pizzaCooked1 = new GameObject().gameObject.AddComponent<BoxCollider>().gameObject.AddComponent<PizzaCooked>();
        pizzaBox1.PizzaCooked = pizzaCooked1;
        pizzaCooked1.MenuType = MenuType.PizzaMargherita;
        pizzaCooked1.SetExtraToppingList(new List<string>() { Ingredient.Ham });
        pizzaCooked1.AttachedTo(pizzaBox1.transform);

        var pizzaBox2 = Object.Instantiate(pizzaBox, orderFullFillManager.transform, true);
        var pizzaCooked2 = new GameObject().gameObject.AddComponent<BoxCollider>().gameObject.AddComponent<PizzaCooked>();
        pizzaBox2.PizzaCooked = pizzaCooked2;
        pizzaCooked2.MenuType = MenuType.PizzaBarbeque;
        pizzaCooked2.SetExtraToppingList(new List<string>() { Ingredient.CheeseCheddar, Ingredient.Onion, Ingredient.BlackOlives });
        pizzaCooked2.AttachedTo(pizzaBox2.transform);

        var pizzaBox3 = Object.Instantiate(pizzaBox, orderFullFillManager.transform, true);
        var pizzaCooked3 = new GameObject().gameObject.AddComponent<BoxCollider>().gameObject.AddComponent<PizzaCooked>();
        pizzaBox3.PizzaCooked = pizzaCooked3;
        pizzaCooked3.MenuType = MenuType.PizzaMargherita;
        pizzaCooked3.SetExtraToppingList(new List<string>() { Ingredient.Sausage, Ingredient.Beef, Ingredient.Pepperoni });
        pizzaCooked3.AttachedTo(pizzaBox3.transform);

        var drinkCup = new GameObject().gameObject.AddComponent<BoxCollider>().gameObject.AddComponent<DrinkCup>();
        drinkCup.MenuType = MenuType.DrinkUnicornPop;

        //-=====================================
        orderFullFillManager.AddItem(pizzaBox1);
        //=========================================


        //Assert order menu is match
        bool isOrderFulfilled = orderFullFillManager.IsOrderFulfilled(new List<OrderMenu>()
        {
            new()
            {
                menuType = MenuType.PizzaMargherita,
                extraToppings = new List<string>() { Ingredient.Ham }
            }
        });
        Assert.IsTrue(isOrderFulfilled);

        //Assert extra topping is not match
        isOrderFulfilled = orderFullFillManager.IsOrderFulfilled(new List<OrderMenu>()
        {
            new()
            {
                menuType = MenuType.PizzaMargherita,
            }
        });
        Assert.IsFalse(isOrderFulfilled);

        isOrderFulfilled = orderFullFillManager.IsOrderFulfilled(new List<OrderMenu>()
        {
            new()
            {
                menuType = MenuType.PizzaMargherita,
                extraToppings = new List<string>() { Ingredient.Onion }
            }
        });

        Assert.IsFalse(isOrderFulfilled);

        //Assert multiple order menu is match
        orderFullFillManager.AddItem(drinkCup);
        orderFullFillManager.AddItem(pizzaBox2);
        orderFullFillManager.AddItem(pizzaBox3);
        isOrderFulfilled = orderFullFillManager.IsOrderFulfilled(new List<OrderMenu>()
        {
            new()
            {
                menuType = MenuType.PizzaMargherita,
                extraToppings = new List<string>() { Ingredient.Ham }
            },
            new()
            {
                menuType = MenuType.PizzaMargherita,
                extraToppings = new List<string>() { Ingredient.Sausage, Ingredient.Beef, Ingredient.Pepperoni }
            },
            new()
            {
                menuType = MenuType.PizzaBarbeque,
                extraToppings = new List<string>() { Ingredient.CheeseCheddar, Ingredient.Onion, Ingredient.BlackOlives }
            },
            new()
            {
                menuType = MenuType.DrinkUnicornPop,
            }
        });
        Assert.IsTrue(isOrderFulfilled);


        //Assert multiple order menu not match
        isOrderFulfilled = orderFullFillManager.IsOrderFulfilled(new List<OrderMenu>()
        {
            new()
            {
                menuType = MenuType.PizzaMargherita,
                extraToppings = new List<string>() { Ingredient.Ham }
            },
            new()
            {
                menuType = MenuType.PizzaMargherita,
                extraToppings = new List<string>() { Ingredient.Sausage, Ingredient.Pepperoni }
            },
            new()
            {
                menuType = MenuType.PizzaBarbeque,
                extraToppings = new List<string>() { Ingredient.CheeseCheddar, Ingredient.Onion, Ingredient.BlackOlives }
            },
            new()
            {
                menuType = MenuType.DrinkUnicornPop,
            }
        });
        Assert.IsFalse(isOrderFulfilled);


        isOrderFulfilled = orderFullFillManager.IsOrderFulfilled(new List<OrderMenu>()
        {
            new()
            {
                menuType = MenuType.PizzaMargherita,
                extraToppings = new List<string>() { Ingredient.Ham }
            },
            new()
            {
                menuType = MenuType.PizzaMargherita,
                extraToppings = new List<string>() { Ingredient.Sausage, Ingredient.Beef, Ingredient.Pepperoni }
            },
            new()
            {
                menuType = MenuType.PizzaBarbeque,
                extraToppings = new List<string>() { Ingredient.CheeseCheddar, Ingredient.Onion, Ingredient.BlackOlives }
            },
        });
        Assert.IsTrue(isOrderFulfilled);

        isOrderFulfilled = orderFullFillManager.IsOrderFulfilled(new List<OrderMenu>()
        {
            new()
            {
                menuType = MenuType.PizzaMargherita,
            },
            new()
            {
                menuType = MenuType.PizzaMargherita,
                extraToppings = new List<string>() { Ingredient.Sausage, Ingredient.Beef, Ingredient.Pepperoni }
            },
            new()
            {
                menuType = MenuType.PizzaBarbeque,
                extraToppings = new List<string>() { Ingredient.CheeseCheddar, Ingredient.Onion, Ingredient.BlackOlives }
            },
        });
        Assert.IsFalse(isOrderFulfilled);
    }
}