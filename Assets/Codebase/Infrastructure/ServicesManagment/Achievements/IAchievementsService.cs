namespace Assets.Codebase.Infrastructure.ServicesManagment.Achievements
{
    public interface IAchievementsService : IService
    {
        public void UnlockAchievement(string tag);
    }
}
