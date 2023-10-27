using Assets.Codebase.Data.Statistics;
using Assets.Codebase.Presenters.Base;
using Assets.Codebase.Utils.Helpers;
using Assets.Codebase.Utils.Values;
using Assets.Codebase.Views.Base;
using Assets.Codebase.Views.Statistics;
using System;
using System.Linq;
using UniRx;

namespace Assets.Codebase.Presenters.Statistics
{
    public class StatsPresenter : BasePresenter, IStatsPresenter
    {
        public ReactiveProperty<string> CurrentLevelString { get; private set; }
        public ReactiveProperty<string> TotalPushupsString { get; private set; }

        public StatsPresenter()
        {
            ViewId = ViewId.StatsView;

            CurrentLevelString = new ReactiveProperty<string>();
        }

        public override void CreateView()
        {
            base.CreateView();

            CurrentLevelString.Value = ProgressModel.SessionProgress.CurrentTrainingPlan.Value.Level.ToString();
            TotalPushupsString.Value = NumberConverter.Convert(ProgressModel.SessionProgress.TotalPushups.Value);
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
            var resultList = ProgressModel.GetTrainingResultsForPeriod(period);

            int totalPushups = resultList.Sum(x => x.TotalPushups);

            string totalPushupsString = NumberConverter.Convert(totalPushups);
            string recordString = NumberConverter.Convert(resultList.Max(x => x.TotalPushups));
            string trainingsCountString = NumberConverter.Convert(resultList.Count);
            string caloriesString = NumberConverter.Convert(totalPushups * Constants.CaloriesPerPushup);

            return new StatsWidgetInfo(totalPushupsString, recordString, trainingsCountString, caloriesString);
        }
    }
}