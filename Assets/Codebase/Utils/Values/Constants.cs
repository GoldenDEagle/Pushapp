using System.Collections.Generic;

namespace Assets.Codebase.Utils.Values
{
    public static class Constants
    {
        public const float CaloriesPerPushup = 0.5f;
        public static readonly List<int> PushupTargets = new List<int> { 100, 200, 500, 1000, 5000, 10000, 50000, 100000, 1000000 }; 
        public static readonly List<int> CaloriesTargets = new List<int> { 100, 200, 500, 1000, 5000, 10000, 50000, 100000, 1000000 };
        public static readonly List<int> TrainingCountTargets = new List<int> { 1, 10, 50, 100, 200, 300, 400, 500, 600, 700, 800, 900, 1000 };


        // Common localization keys
        public static readonly string LevelWordKey = "level_word";
        public static readonly string TestTrainingNameKey = "test_word";
        public static readonly string DayTrainingNameKey = "day_word";
    }
}
