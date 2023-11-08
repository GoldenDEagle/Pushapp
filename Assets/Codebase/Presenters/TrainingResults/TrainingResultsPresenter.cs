using Assets.Codebase.Data.Trainings;
using Assets.Codebase.Data.WarmUp;
using Assets.Codebase.Infrastructure.ServicesManagment;
using Assets.Codebase.Infrastructure.ServicesManagment.Ads;
using Assets.Codebase.Infrastructure.ServicesManagment.Localization;
using Assets.Codebase.Presenters.Base;
using Assets.Codebase.Utils.CustomTypes;
using Assets.Codebase.Utils.Extensions;
using Assets.Codebase.Utils.Helpers;
using Assets.Codebase.Utils.Values;
using Assets.Codebase.Views.Base;
using UniRx;
using Unity.VisualScripting;

namespace Assets.Codebase.Presenters.TrainingResults
{
    public class TrainingResultsPresenter : BasePresenter, ITrainingResultsPresenter
    {
        public ReactiveProperty<string> ViewHeaderString { get; }
        public ReactiveProperty<string> LevelString { get; private set; }
        public ReactiveProperty<string> TrainingDayString { get; private set; }
        public ReactiveProperty<string> ResultsString { get; private set; }
        public ReactiveProperty<string> TotalPushupsString { get; private set; }
        public ReactiveProperty<string> TrainingDurationString { get; private set; }
        public ReactiveProperty<string> BurntCaloriesString { get; private set; }
        public ReactiveProperty<string> NextTrainingDateString { get; private set; }

        private TrainingResult _lastTrainingResult;

        private const string TestPassedKey = "test_passed";
        private const string TestFailedKey = "test_failed";
        private const string TrainingEnded = "training_ended";

        public TrainingResultsPresenter()
        {
            ViewId = ViewId.TrainingResultView;

            ViewHeaderString = new ReactiveProperty<string>();
            LevelString = new ReactiveProperty<string>();
            TrainingDayString = new ReactiveProperty<string>();
            ResultsString = new ReactiveProperty<string>();
            TotalPushupsString = new ReactiveProperty<string>();
            TrainingDurationString = new ReactiveProperty<string>();
            BurntCaloriesString = new ReactiveProperty<string>();
            NextTrainingDateString = new ReactiveProperty<string>();
        }

        public override void CreateView()
        {
            _lastTrainingResult = ProgressModel.SessionProgress.AllResults[ProgressModel.SessionProgress.AllResults.Count - 1];

            base.CreateView();

            FillViewWithData();
        }

        private void FillViewWithData()
        {
            var localizationService = ServiceLocator.Container.Single<ILocalizationService>();

            LevelString.Value = localizationService.LocalizeTextByKey(Constants.LevelWordKey) + " " + ProgressModel.SessionProgress.CurrentTrainingPlan.Value.Level.ToString();
            ResultsString.Value = _lastTrainingResult.PushupAttempts.ToPushupsListString();
            TotalPushupsString.Value = localizationService.LocalizeTextByKey("total_count") + " " + NumberConverter.Convert(_lastTrainingResult.TotalPushups);
            TrainingDurationString.Value = TimeConverter.TimeInMinutes(_lastTrainingResult.TrainingDuration);
            BurntCaloriesString.Value = NumberConverter.Convert(_lastTrainingResult.TotalPushups * Constants.CaloriesPerPushup);

            if (ProgressModel.SessionProgress.IsOnTestingStage.Value)
            {
                TrainingDayString.Value = localizationService.LocalizeTextByKey(Constants.TestTrainingNameKey);
                if (CheckTestCompletion())
                {
                    ViewHeaderString.Value = localizationService.LocalizeTextByKey(TestPassedKey);
                }
                else
                {
                    ViewHeaderString.Value = localizationService.LocalizeTextByKey(TestFailedKey);
                }
            }
            else
            {
                ViewHeaderString.Value = localizationService.LocalizeTextByKey(TrainingEnded);
                TrainingDayString.Value = localizationService.LocalizeTextByKey(Constants.DayTrainingNameKey) + " " + ProgressModel.SessionProgress.CurrentTrainingDayId.Value.ToString();
            }

            var currentTime = TimeProvider.GetServerTime();
            var nextTrainingDate = new SerializableDateTime(currentTime.AddHours(ProgressModel.SessionProgress.CurrentTrainingPlan.Value.TrainingDays[ProgressModel.SessionProgress.CurrentTrainingDayId.Value].RestingTime));
            NextTrainingDateString.Value = nextTrainingDate.DateTime.ToShortDateString();
        }

        public void GoNextClicked()
        {
            ProgressUpdate();

            ShowAd();

            GoToNextView();
        }

        public void StretchingToggleClicked(bool toggleState)
        {
            ProgressModel.SessionProgress.IsStretchingEnabled.Value = toggleState;
        }

        public bool IsStretchingEnabled()
        {
            return ProgressModel.SessionProgress.IsStretchingEnabled.Value;
        }

        // Internal ...................................................
        private void GoToNextView()
        {
            // Go to stretching if needed
            if (ProgressModel.SessionProgress.IsStretchingEnabled.Value)
            {
                GameplayModel.CurrentWarmupMode.Value = WarmupMode.Stretching;
                GameplayModel.ActivateView(ViewId.WarmupView);
            }
            // Or straight to main
            else
            {
                GameplayModel.ActivateView(ViewId.MainView);
            }
        }

        private void ProgressUpdate()
        {
            // If it was a test
            if (ProgressModel.SessionProgress.IsOnTestingStage.Value)
            {
                if (CheckTestCompletion())
                {
                    ProgressModel.SessionProgress.PassTrainingDay();
                }
                else
                {
                    // Test failed logic if needed
                }
            }
            // If it was regular day
            else
            {
                ProgressModel.SessionProgress.PassTrainingDay();
            }

            ProgressModel.SaveProgress();
        }

        private bool CheckTestCompletion()
        {
            if (_lastTrainingResult.TotalPushups >= ProgressModel.SessionProgress.CurrentTrainingPlan.Value.TestDay.GetTotalPushupCount())
            {
                return true;
            }
            return false;
        }

        private void ShowAd()
        {
            var adService = ServiceLocator.Container.Single<IAdsService>();
            if (adService.CheckIfFullscreenIsAvailable())
            {
                adService.ShowFullscreen();
            }
        }
    }
}
