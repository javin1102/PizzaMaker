using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace PizzaMaker
{
    public static class Ingredients
    {
        public static readonly string[] All =
        {
            SauceTomato,
            Pepperoni,
            SmokedChicken,
            SauceBarbecue,
            Beef,
            CheeseMozzarella,
            CheeseCheddar,
            Basil,
            Oregano,
            Spinach,
            Jalapeno,
            BlackOlives,
            Sausage,
            Ham,
            Tomato,
            Corn,
            Mushroom,
            Onion,
            Pineapple,
            BellPepper,
            RedOnion,
        };

        public const string SauceTomato = "Tomato Sauce";
        public const string Pepperoni = "Pepperoni";
        public const string SmokedChicken = "Smoked Chicken";
        public const string SauceBarbecue = "Barbecue Sauce";
        public const string Beef = "Beef";
        public const string CheeseMozzarella = "Mozzarella Cheese";
        public const string CheeseCheddar = "Cheddar Cheese";
        public const string Basil = "Basil";
        public const string Oregano = "Oregano";
        public const string Spinach = "Spinach";
        public const string Jalapeno = "Jalapeno";
        public const string BlackOlives = "Black Olives";
        public const string Sausage = "Sausage";
        public const string Ham = "Ham";
        public const string Tomato = "Tomato";
        public const string Corn = "Corn";
        public const string Mushroom = "Mushroom";
        public const string Onion = "Onion";
        public const string Pineapple = "Pineapple";
        public const string BellPepper = "Bell Pepper";
        public const string RedOnion = "Red Onion";
    }

    [Serializable]
    public struct PizzaMenu : IEquatable<PizzaMenu>
    {
        public bool Equals(PizzaMenu other)
        {
            return id == other.id && name == other.name;
        }

        public override bool Equals(object obj)
        {
            return obj is PizzaMenu other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(id, name);
        }

        public static PizzaMenu PizzaInvalid = new("Burnt Pizza", -1);
        public static PizzaMenu PizzaMargherita = new("Margherita Pizza", 1);
        public static PizzaMenu PizzaBarbeque = new("Barbeque Pizza", 2);
        public static readonly PizzaMenu[] All = { PizzaMargherita, PizzaBarbeque, PizzaInvalid };

        public static bool operator ==(PizzaMenu a, PizzaMenu b)
        {
            return a.id == b.id;
        }

        public static bool operator !=(PizzaMenu a, PizzaMenu b)
        {
            return !(a == b);
        }

        public int id;
        public string name;

        public PizzaMenu(string name, int id)
        {
            this.name = name;
            this.id = id;
        }
    }


    public static class GlobalVars
    {
        //Layers
        public const int LayerDefault = 0;
        public const int LayerFocus = 6;
        public const string SceneCore = "Core";

        /* -------------------------------------------------------------------------- */
        /*                                    Tags                                    */
        /* -------------------------------------------------------------------------- */
        public const string TagSpawn = "Spawn";
        public const string TagPlayer = "Player";
        public const string SaveData = "SaveData";


        //Zenject Ids
        public const string ZenDialogueMainDatabaseId = "Zen_Dialogue_Main";


        //Lua Variables
        /// <summary>
        /// Check whether a conversation has been done <br/>
        /// Key: conversation title <br/>
        /// Value: lua variable name (Inspect in Dialogue Editor)
        /// </summary>
        public static Dictionary<string, string> ConversationVarDict = new()
        {
            { "day1/boss/intro", "day1_boss_intro" },
        };
    }

    public static class LuaVariables
    {
        public static class Conversations
        {
            public const string Day1BossIntro = "day1_boss_intro";
        }

        public static class Quests
        {
            public static QuestId Day1OpenPhone = new QuestId("day1_open_phone");
        }
    }

    public readonly struct QuestId : IEquatable<QuestId>
    {
        public readonly string id;

        public QuestId(string id)
        {
            this.id = id;
        }

        //override assignment operator for id to other value
        public static implicit operator string(QuestId questId)
        {
            return questId.id;
        }

        // Convert string to QuestId
        public static implicit operator QuestId(string id)
        {
            return new QuestId(id);
        }

        public static bool operator ==(QuestId a, QuestId b)
        {
            return a.id == b.id;
        }

        public static bool operator !=(QuestId a, QuestId b)
        {
            return !(a == b);
        }

        public bool Equals(QuestId other)
        {
            return id == other.id;
        }

        public override bool Equals(object obj)
        {
            return obj is QuestId other && Equals(other);
        }

        public override int GetHashCode()
        {
            return (id != null ? id.GetHashCode() : 0);
        }
    }
}