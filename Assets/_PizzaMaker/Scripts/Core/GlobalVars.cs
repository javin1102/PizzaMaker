using System.Collections.Generic;

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
}

