using System;
using System.Collections.Generic;

namespace PizzaMaker
{
    public enum IngredientType
    {
        SauceTomato = 0,
        Pepperoni = 1,
        SmokedChicken = 2,
        SauceBarbecue = 3,
        Beef = 4,
        CheeseMozzarella = 5,
        CheeseCheddar = 6,
        Basil = 7,
        Oregano = 8,
        Spinach = 9,
        Jalapeno = 10,
        BlackOlives = 11,
        Sausage = 12,
        Ham = 13,
        Tomato = 14,
        Corn = 15,
        Mushroom = 16,
        Onion = 17,
        Pineapple = 18,
        BellPepper = 19,
        RedOnion = 20,
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
        public static class Conversatons
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