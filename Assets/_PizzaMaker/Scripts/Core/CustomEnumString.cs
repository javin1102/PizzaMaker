using System;

namespace PizzaMaker
{
    public class CustomEnumString<T>
    {
        public int id;
        public string name;

        public CustomEnumString(string name, int id)
        {
            this.name = name;
            this.id = id;
        }
    }
}