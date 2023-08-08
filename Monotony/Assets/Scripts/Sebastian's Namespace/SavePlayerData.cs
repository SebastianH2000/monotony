using System.Collections;
using System.Collections.Generic;

namespace SebastiansNamespace {
    public static class SavePlayerData
    {
        public static float sanity = 1f;
        public static bool lookingAtMonster = false;
        public static float monsterDistance = 0;
        public static int day = 1;
        public static float time;

        public static string lastTask = "Menu";
        public static string currentTask = "Menu";
        public static string nextTask = "Web";
        public static bool menuOpen = true;

        public static string[] taskArray = new string[4];
        public static bool[] completedArray = new bool[4];
        public static bool hasWatchedIntro = false;
    }
}
