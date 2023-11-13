using Assets.Codebase.Infrastructure.ServicesManagment.Achievements;
using GamePush;
using System.Collections.Generic;

namespace Assets.Codebase.Data.Achievements
{
    public class GPAchievementsService : IAchievementsService
    {
        private const string PushUpsAchievementKey = "PushUps_";
        private const string TrainingCountAchievementKey = "TrainingsCount_";
        private const string CaloriesAchievementKey = "Calories_";

        private List<AchievementsFetch> _gpAchievements;

        public GPAchievementsService()
        {
            GP_Achievements.OnAchievementsFetch += OnAchievementsFetched;

            GP_Achievements.Fetch();
        }

        public void ShowAchievementsList()
        {
            GP_Achievements.Open();
        }

        public void UnlockAchievement(string tag)
        {
            if (GP_Achievements.Has(tag)) return;

            GP_Achievements.Unlock(tag);
        }

        public void UnlockPushupsAchievement(int achievementId)
        {
            string achievementTag = PushUpsAchievementKey + achievementId.ToString();
            UnlockAchievement(achievementTag);
        }

        public void UnlockTrainingCountAchievement(int achievementId)
        {
            string achievementTag = TrainingCountAchievementKey + achievementId.ToString();
            UnlockAchievement(achievementTag);
        }

        public void UnlockCaloriesAchievement(int achievementId)
        {
            string achievementTag = CaloriesAchievementKey + achievementId.ToString();
            UnlockAchievement(achievementTag);
        }

        private void OnAchievementsFetched(List<AchievementsFetch> achievements)
        {
            _gpAchievements = achievements;
        }
    }
}
