using Assets.Codebase.Presenters.Base;
using Assets.Codebase.Views.Base;

namespace Assets.Codebase.Presenters.Statistics
{
    public class StatsPresenter : BasePresenter, IStatsPresenter
    {
        public StatsPresenter()
        {
            ViewId = ViewId.StatsView;
        }

        public void GoToMain()
        {
            GameplayModel.ActivateView(ViewId.MainView);
        }

        public void GoToSettings()
        {
            GameplayModel.ActivateView(ViewId.SettingsView);
        }
    }
}
