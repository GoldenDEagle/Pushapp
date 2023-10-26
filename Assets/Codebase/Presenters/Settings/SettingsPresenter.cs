using Assets.Codebase.Presenters.Base;
using Assets.Codebase.Views.Base;

namespace Assets.Codebase.Presenters.Settings
{
    public class SettingsPresenter : BasePresenter, ISettingsPresenter
    {
        public SettingsPresenter()
        {
            ViewId = ViewId.SettingsView;
        }

        public void GoToMain()
        {
            GameplayModel.ActivateView(ViewId.MainView);
        }

        public void GoToStatistics()
        {
            GameplayModel.ActivateView(ViewId.StatsView);
        }
    }
}
