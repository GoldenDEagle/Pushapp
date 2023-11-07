using Assets.Codebase.Data.Statistics;
using Assets.Codebase.Data.Trainings;
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
        public Subject<PeriodWithTrainingResults> OnShowGraph { get; private set; }

        private DateTime _graphStartingDate;
        private DateTime _graphEndingDate;
        private TimeSpan _graphSwitchStep;
        private int _graphStepInDays = 14;

        public StatsPresenter()
        {
            ViewId = ViewId.StatsView;

            _graphSwitchStep = TimeSpan.FromDays(_graphStepInDays);
            CurrentLevelString = new ReactiveProperty<string>();
            TotalPushupsString = new ReactiveProperty<string>();
            OnShowGraph = new Subject<PeriodWithTrainingResults>();
            _graphEndingDate = DateTime.Now;
            _graphStartingDate = DateTime.Now.Subtract(_graphSwitchStep);
        }

        public override void CreateView()
        {
            base.CreateView();

            CurrentLevelString.Value = ProgressModel.SessionProgress.CurrentTrainingPlan.Value.Level.ToString();
            TotalPushupsString.Value = NumberConverter.Convert(ProgressModel.SessionProgress.TotalPushups.Value);
            UpdateGraph();
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
            string trainingsCountString = NumberConverter.Convert(resultList.Count);
            string caloriesString = NumberConverter.Convert(totalPushups * Constants.CaloriesPerPushup);

            string recordString;
            if (!resultList.Any())
            {
                recordString = 0.ToString();
            }
            else
            {
                recordString = NumberConverter.Convert(resultList.Max(x => x.TotalPushups));
            }

            return new StatsWidgetInfo(totalPushupsString, recordString, trainingsCountString, caloriesString);
        }

        public void GoToNextResultsSegment()
        {
            _graphStartingDate.AddDays(_graphStepInDays);
            _graphEndingDate.AddDays(_graphStepInDays);
            UpdateGraph();
        }

        public void GoToPreviousResultsSegment()
        {
            _graphStartingDate.Subtract(_graphSwitchStep);
            _graphEndingDate.Subtract(_graphSwitchStep);
            UpdateGraph();
        }


        private void UpdateGraph()
        {
            var trainingResults = ProgressModel.SessionProgress.AllResults.Where(x => (x.Date.DateTime >= _graphStartingDate) && (x.Date.DateTime <= _graphEndingDate)).ToList();
            OnShowGraph?.OnNext(new PeriodWithTrainingResults(trainingResults));
        }
    }
}