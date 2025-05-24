using System.Collections.Generic;

namespace PizzaMaker
{
    public static class SaveKey
    {
        public const string DataDialogueState = "dat_dialogue_state";
        
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
}