namespace Assets.Codebase.Infrastructure.ServicesManagment.Achievements
{
    public interface IAchievementsService : IService
    {
        public void ShowAchievementsList();
        public void UnlockAchievement(string tag);
        public void UnlockPushupsAchievement(int achievementId);

        public void UnlockTrainingCountAchievement(int achievementId);

        public void UnlockCaloriesAchievement(int achievementId);
    }
}
