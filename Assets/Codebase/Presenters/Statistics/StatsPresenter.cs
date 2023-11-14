using Assets.Codebase.Data.Statistics;
using Assets.Codebase.Data.Trainings;
using Assets.Codebase.Infrastructure.ServicesManagment;
using Assets.Codebase.Infrastructure.ServicesManagment.Localization;
using Assets.Codebase.Presenters.Base;
using Assets.Codebase.Utils.Helpers;
using Assets.Codebase.Utils.Values;
using Assets.Codebase.Views.Base;
using Assets.Codebase.Views.Statistics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniRx;

namespace Assets.Codebase.Presenters.Statistics
{
    public class StatsPresenter : BasePresenter, IStatsPresenter
    {
        public ReactiveProperty<bool> IsNextGraphButtonActive { get; private set; }
        public ReactiveProperty<bool> IsPreviousGraphButtonActive { get; private set; }
        public ReactiveProperty<string> CurrentLevelString { get; private set; }
        public ReactiveProperty<string> TotalPushupsString { get; private set; }
        public ReactiveProperty<string> GraphPeriodString { get; private set; }
        public Subject<PeriodWithTrainingResults> OnShowGraph { get; private set; }

        private DateTime _graphStartingDate;
        private DateTime _graphEndingDate;
        private TimeSpan _graphSwitchStep;
        private int _graphStepInDays = 14;
        private StringBuilder _graphPeriodSB;

        //private List<TrainingResult> _testingResults;

        public StatsPresenter()
        {
            ViewId = ViewId.StatsView;

            _graphSwitchStep = TimeSpan.FromDays(_graphStepInDays);
            CurrentLevelString = new ReactiveProperty<string>();
            TotalPushupsString = new ReactiveProperty<string>();
            GraphPeriodString = new ReactiveProperty<string>();
            IsNextGraphButtonActive = new ReactiveProperty<bool>(true);
            IsPreviousGraphButtonActive = new ReactiveProperty<bool>(true);
            _graphPeriodSB = new StringBuilder();
            OnShowGraph = new Subject<PeriodWithTrainingResults>();

            //_testingResults = CreateTestResults();
        }

        public override void CreateView()
        {
            base.CreateView();

            var localizationService = ServiceLocator.Container.Single<ILocalizationService>();

            CurrentLevelString.Value = localizationService.LocalizeTextByKey(Constants.LevelWithNumberKey) + " " + ProgressModel.SessionProgress.CurrentTrainingPlan.Value.Level.ToString();
            TotalPushupsString.Value = localizationService.LocalizeTextByKey(Constants.TotalWithCountKey) + " " + NumberConverter.Convert(ProgressModel.SessionProgress.TotalPushups.Value);
            _graphEndingDate = DateTime.Now;
            _graphStartingDate = DateTime.Now.Subtract(_graphSwitchStep);
            UpdateGraph();
            ManageButtonStates();
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
            float totalTime = resultList.Sum(x => x.TrainingDuration);

            string totalPushupsString = NumberConverter.Convert(totalPushups);
            string trainingsCountString = NumberConverter.Convert(resultList.Count);
            string caloriesString = NumberConverter.Convert(totalPushups * Constants.CaloriesPerPushup);
            string time = TimeConverter.TimeInHours(totalTime);

            string recordString;
            if (!resultList.Any())
            {
                recordString = 0.ToString();
            }
            else
            {
                recordString = NumberConverter.Convert(resultList.Max(x => x.TotalPushups));
            }

            return new StatsWidgetInfo(totalPushupsString, recordString, trainingsCountString, caloriesString, string.Empty, time);
        }

        public void GoToNextResultsSegment()
        {
            _graphStartingDate = _graphStartingDate.AddDays(_graphStepInDays);
            _graphEndingDate = _graphEndingDate.AddDays(_graphStepInDays);
            UpdateGraph();
            ManageButtonStates();
        }

        public void GoToPreviousResultsSegment()
        {
            _graphStartingDate = _graphStartingDate.Subtract(_graphSwitchStep);
            _graphEndingDate = _graphEndingDate.Subtract(_graphSwitchStep);
            UpdateGraph();
            ManageButtonStates();
        }


        /// <summary>
        /// Disables graph buttons if needed
        /// </summary>
        private void ManageButtonStates()
        {
            var furtherResults = ProgressModel.SessionProgress.AllResults.Where(x => x.Date.DateTime > _graphEndingDate);
            if (!furtherResults.Any())
            {
                IsNextGraphButtonActive.Value = false;
            }
            else
            {
                IsNextGraphButtonActive.Value = true;
            }
        }

        private void UpdateGraph()
        {
            // Build
            var trainingResults = ProgressModel.SessionProgress.AllResults.Where(x => (x.Date.DateTime >= _graphStartingDate) && (x.Date.DateTime <= _graphEndingDate)).ToList();
            // Testing
            //var trainingResults = _testingResults.Where(x => (x.Date.DateTime >= _graphStartingDate) && (x.Date.DateTime <= _graphEndingDate)).ToList();

            OnShowGraph?.OnNext(new PeriodWithTrainingResults(trainingResults));

            _graphPeriodSB.Clear();
            _graphPeriodSB.Append(_graphStartingDate.ToShortDateString()).Append(" - ").Append(_graphEndingDate.ToShortDateString());
            GraphPeriodString.Value = _graphPeriodSB.ToString();
        }


        // Creating results for testing
        //private List<TrainingResult> CreateTestResults()
        //{
        //    DateTime dayBeforeYesterday = DateTime.Now.Subtract(TimeSpan.FromDays(2));
        //    DateTime yesterday = DateTime.Now.Subtract(TimeSpan.FromDays(1));
        //    DateTime today = DateTime.Now;
        //    DateTime lastMonth1 = DateTime.Now.Subtract(TimeSpan.FromDays(25));
        //    DateTime lastMonth2 = DateTime.Now.Subtract(TimeSpan.FromDays(21));

        //    List<TrainingResult> testResults = new List<TrainingResult>()
        //    {
        //            new TrainingResult(new List<int>() { 5, 10, 10 }, 25, lastMonth1),
        //            new TrainingResult(new List<int>() { 4, 6, 7 }, 17, lastMonth1),
        //            new TrainingResult(new List<int>() { 10, 20, 10 }, 40, dayBeforeYesterday),
        //            new TrainingResult(new List<int>() { 5, 7, 4 }, 16, yesterday),
        //            new TrainingResult(new List<int>() {0, 1, 0 }, 1, today),
        //    };

        //    return testResults;
        //}
    }
}