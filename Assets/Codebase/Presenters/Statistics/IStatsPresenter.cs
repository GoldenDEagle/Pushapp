using Assets.Codebase.Data.Statistics;
using Assets.Codebase.Data.Trainings;
using Assets.Codebase.Presenter.Base;
using Assets.Codebase.Views.Statistics;
using UniRx;

namespace Assets.Codebase.Presenters.Statistics
{
    public interface IStatsPresenter : IPresenter
    {
        public ReactiveProperty<string> CurrentLevelString { get; }
        public ReactiveProperty<string> TotalPushupsString { get; }
        public Subject<PeriodWithTrainingResults> OnShowGraph { get; }

        public void GoToMain();
        public void GoToSettings();
        public StatsWidgetInfo GetStatsForPeriod(StatsPeriod period);
        public void GoToNextResultsSegment();
        public void GoToPreviousResultsSegment();
    }
}
