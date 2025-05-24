using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace PizzaMaker
{
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

    public static class CustomLua
    {
        public static class Variables
        {
            public const string Day1BossIntro = "day1_boss_intro";
        }

        public static class Quests
        {
            public static QuestId Day1OpenPhone = new ("day1_open_phone");
            public static QuestId Day1PizzaOrderAzis = new("day1_pizza_order_azis");
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