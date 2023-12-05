using Assets.Codebase.Data.Trainings;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Codebase.Utils.Values
{
    public static class Constants
    {
        public const float CaloriesPerPushup = 0.5f;
        public static readonly List<int> PushupTargets = new List<int> { 100, 200, 500, 1000, 5000, 10000, 50000, 100000, 1000000 }; 
        public static readonly List<int> CaloriesTargets = new List<int> { 100, 200, 500, 1000, 5000, 10000, 50000, 100000, 1000000 };
        public static readonly List<int> TrainingCountTargets = new List<int> { 1, 10, 50, 100, 200, 300, 400, 500, 600, 700, 800, 900, 1000 };


        // Common localization keys
        public static readonly string WelcomeInfoKey = "welcome_info";
        public static readonly string LevelWordKey = "level_word";
        public static readonly string TestTrainingNameKey = "test_word";
        public static readonly string DayTrainingNameKey = "day_word";
        public static readonly string TotalWithCountKey = "total_count";
        public static readonly string LevelWithNumberKey = "level_withNumber";
        public static readonly string HoursWordKey = "hours_word";
        public static readonly string NextTrainingDateKey = "nextTraining_text";
        public static readonly string ApproachesWithCountKey = "approaches_withCount";
        // Warnings
        public static readonly string DeleteProgressWarningKey = "warning_deleteProgress";
        public static readonly string EarlyTrainingWarningKey = "warning_earlyTraining";
        public static readonly string ProgramSwitchWarningKey = "warning_programSwitch";
        // Tutorial step descriptions
        public static readonly string TutorialDescription1Key = "tutorial_1";
        public static readonly string TutorialDescription2Key = "tutorial_2";
        public static readonly string TutorialDescription3Key = "tutorial_3";
        public static readonly string TutorialDescription4Key = "tutorial_4";
        public static readonly string TutorialNextButtonKey = "tutorial_nextButton";
        public static readonly string TutorialEndButtonKey = "tutorial_endButton";

        // Difficulty level colors
        public static Dictionary<DifficultyLevel, Color> LevelColors = new Dictionary<DifficultyLevel, Color>()
        {
                { DifficultyLevel.Beginner, new Color32(66, 119, 255, 255) },
                { DifficultyLevel.Easy, new Color32(227, 151, 0, 255) },
                { DifficultyLevel.Medium, new Color32(221, 95, 2, 255) },
                { DifficultyLevel.Hard, new Color32(215, 30, 18, 255) },
                { DifficultyLevel.Nightmare, new Color32(164, 0, 0, 255) },
        };
    }
}
