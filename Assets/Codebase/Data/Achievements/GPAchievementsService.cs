using Assets.Codebase.Infrastructure.ServicesManagment.Achievements;
using GamePush;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Codebase.Data.Achievements
{
    public class GPAchievementsService : IAchievementsService
    {
        private List<AchievementsFetch> _gpAchievements;

        public GPAchievementsService()
        {
            GP_Achievements.OnAchievementsFetch += OnAchievementsFetched;

            GP_Achievements.Fetch();
        }

        public void UnlockAchievement(string tag)
        {
            var targetAchievement = _gpAchievements.FirstOrDefault(x => x.tag == tag);

            if (targetAchievement != null)
            {
                GP_Achievements.Unlock(tag);
            }
        }

        private void OnAchievementsFetched(List<AchievementsFetch> achievements)
        {
            _gpAchievements = achievements;
        }
    }
}
