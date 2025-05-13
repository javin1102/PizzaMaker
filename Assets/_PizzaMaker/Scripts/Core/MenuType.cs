using System;

namespace PizzaMaker
{
    [Serializable]
    public struct MenuType : IEquatable<MenuType>
    {
        public bool Equals(MenuType other)
        {
            return id == other.id && name == other.name;
        }

        public override bool Equals(object obj)
        {
            return obj is MenuType other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(id, name);
        }

        public static MenuType PizzaBurnt = new("Burnt Pizza", -1);
        public static MenuType PizzaMargherita = new("Margherita Pizza", 1);
        public static MenuType PizzaBarbeque = new("Barbeque Pizza", 2);
        public static MenuType DrinkChronoCola = new("Drink Chrono Cola", 1000);
        public static MenuType DrinkYoSeott = new("Yo Seott", 1001);
        public static MenuType DrinkUnicornPop = new("Unicorn Pop", 1002);
        public static MenuType DrinkSojaCola = new("Soja Cola", 1003);
        public static MenuType DrinkMrRight = new("Mr Right", 1004);
        public static MenuType DrinkPhoenixin = new("Phoenixin", 1005);

        public static readonly MenuType[] All = { PizzaMargherita, PizzaBarbeque, PizzaBurnt, DrinkChronoCola, DrinkYoSeott, DrinkUnicornPop, DrinkSojaCola, DrinkMrRight, DrinkPhoenixin };

        public static bool operator ==(MenuType a, MenuType b)
        {
            return a.id == b.id;
        }

        public static bool operator !=(MenuType a, MenuType b)
        {
            return !(a == b);
        }

        public int id;
        public string name;

        public MenuType(string name, int id)
        {
            this.name = name;
            this.id = id;
        }
    }
}