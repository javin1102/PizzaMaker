using System;
using Unity.Android.Gradle.Manifest;

namespace PizzaMaker
{
    [Serializable]
    public struct Menu : IEquatable<Menu>
    {
        public bool Equals(Menu other)
        {
            return id == other.id && name == other.name;
        }

        public override bool Equals(object obj)
        {
            return obj is Menu other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(id, name);
        }

        //Pizza Types
        public static Menu PizzaBurnt = new("Burnt Pizza", -1, Category.Drink);
        public static Menu PizzaMargherita = new("Margherita Pizza", 1, Category.Drink);
        public static Menu PizzaBarbeque = new("Barbeque Pizza", 2, Category.Drink);
        public static Menu PizzaChickenLover = new("Chicken Lover Pizza", 3, Category.Drink);
        public static Menu PizzaMeatLover = new("Meat Lover Pizza", 4, Category.Drink);
        public static Menu PizzaCheeseLover = new("Cheese Lover Pizza", 5, Category.Drink);
        public static Menu PizzaVegetarian = new("Vegetarian Pizza", 6, Category.Drink);
        public static Menu PizzaPepperoni = new("Pepperoni Pizza", 7, Category.Drink);
        public static Menu PizzaHawaiian = new("Hawaiian Pizza", 8, Category.Drink);

        // Drinks
        public static Menu DrinkChronoCola = new("Chrono Cola", 1000, Category.Drink);
        public static Menu DrinkYoSeott = new("Yo Seott", 1001, Category.Drink);
        public static Menu DrinkUnicornPop = new("Unicorn Pop", 1002, Category.Drink);
        public static Menu DrinkSojaCola = new("Soja Cola", 1003, Category.Drink);
        public static Menu DrinkMrRight = new("Mr Right", 1004, Category.Drink);
        public static Menu DrinkPhoenixin = new("Phoenixin", 1005, Category.Drink);

        public static readonly Menu[] All = { 
            PizzaBurnt, 
            PizzaMargherita, 
            PizzaBarbeque, 
            PizzaChickenLover,
            PizzaMeatLover,
            PizzaCheeseLover,
            PizzaVegetarian,
            PizzaPepperoni,
            PizzaHawaiian,
            DrinkChronoCola, 
            DrinkYoSeott, 
            DrinkUnicornPop, 
            DrinkSojaCola, 
            DrinkMrRight, 
            DrinkPhoenixin
                };

        public static bool operator ==(Menu a, Menu b)
        {
            return a.id == b.id;
        }

        public static bool operator !=(Menu a, Menu b)
        {
            return !(a == b);
        }

        public int id;
        public string name;
        public readonly Category category;
        public Menu(string name, int id, Category category)
        {
            this.name = name;
            this.id = id;
            this.category = category;
        }

        public enum Category
        {
            Pizza,
            Drink
        }
    }
}