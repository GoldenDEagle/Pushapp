using Assets.Codebase.Data.Statistics;
using Assets.Codebase.Presenters.Base;
using Assets.Codebase.Views.Base;
using Assets.Codebase.Views.Statistics;
using UniRx;

namespace Assets.Codebase.Presenters.Statistics
{
    public class StatsPresenter : BasePresenter, IStatsPresenter
    {
        public ReactiveProperty<string> CurrentLevelString { get; private set; }

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

        public StatsWidgetInfo GetStatsForPeriod(StatsPeriod period)
        {
            return null;
        }
    }
}
