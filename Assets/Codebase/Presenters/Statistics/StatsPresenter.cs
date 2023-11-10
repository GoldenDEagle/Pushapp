using Assets.Codebase.Data.Statistics;
using Assets.Codebase.Data.Trainings;
using Assets.Codebase.Presenters.Base;
using Assets.Codebase.Utils.Helpers;
using Assets.Codebase.Utils.Values;
using Assets.Codebase.Views.Base;
using Assets.Codebase.Views.Statistics;
using System;
using System.Collections.Generic;
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

        private List<TrainingResult> _testingResults;

        public StatsPresenter()
        {
            ViewId = ViewId.StatsView;

            _graphSwitchStep = TimeSpan.FromDays(_graphStepInDays);
            CurrentLevelString = new ReactiveProperty<string>();
            TotalPushupsString = new ReactiveProperty<string>();
            OnShowGraph = new Subject<PeriodWithTrainingResults>();

            _testingResults = CreateTestResults();
        }

        public override void CreateView()
        {
            base.CreateView();

            CurrentLevelString.Value = ProgressModel.SessionProgress.CurrentTrainingPlan.Value.Level.ToString();
            TotalPushupsString.Value = NumberConverter.Convert(ProgressModel.SessionProgress.TotalPushups.Value);
            _graphEndingDate = DateTime.Now;
            _graphStartingDate = DateTime.Now.Subtract(_graphSwitchStep);
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
            _graphStartingDate = _graphStartingDate.AddDays(_graphStepInDays);
            _graphEndingDate = _graphEndingDate.AddDays(_graphStepInDays);
            UpdateGraph();
        }

        public void GoToPreviousResultsSegment()
        {
            _graphStartingDate = _graphStartingDate.Subtract(_graphSwitchStep);
            _graphEndingDate = _graphEndingDate.Subtract(_graphSwitchStep);
            UpdateGraph();
        }


        private void UpdateGraph()
        {
            // Build
            //var trainingResults = ProgressModel.SessionProgress.AllResults.Where(x => (x.Date.DateTime >= _graphStartingDate) && (x.Date.DateTime <= _graphEndingDate)).ToList();
            // Testing
            var trainingResults = _testingResults.Where(x => (x.Date.DateTime >= _graphStartingDate) && (x.Date.DateTime <= _graphEndingDate)).ToList();

            if (!trainingResults.Any()) return;

            OnShowGraph?.OnNext(new PeriodWithTrainingResults(trainingResults));
        }


        // Creating results for testing
        private List<TrainingResult> CreateTestResults()
        {
            DateTime dayBeforeYesterday = DateTime.Now.Subtract(TimeSpan.FromDays(2));
            DateTime yesterday = DateTime.Now.Subtract(TimeSpan.FromDays(1));
            DateTime today = DateTime.Now;
            DateTime lastMonth1 = DateTime.Now.Subtract(TimeSpan.FromDays(25));
            DateTime lastMonth2 = DateTime.Now.Subtract(TimeSpan.FromDays(21));

            List<TrainingResult> testResults = new List<TrainingResult>()
            {
                    new TrainingResult(new List<int>() { 5, 10, 10 }, 25, lastMonth1),
                    new TrainingResult(new List<int>() { 4, 6, 7 }, 17, lastMonth1),
                    new TrainingResult(new List<int>() { 10, 20, 10 }, 40, dayBeforeYesterday),
                    new TrainingResult(new List<int>() { 5, 7, 4 }, 16, yesterday),
                    new TrainingResult(new List<int>() {0, 1, 0 }, 1, today),
            };

            return testResults;
        }
    }
}