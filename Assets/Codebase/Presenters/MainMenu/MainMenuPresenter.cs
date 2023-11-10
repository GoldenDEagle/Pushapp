using Assets.Codebase.Data.WarmUp;
using Assets.Codebase.Infrastructure.ServicesManagment;
using Assets.Codebase.Infrastructure.ServicesManagment.Achievements;
using Assets.Codebase.Infrastructure.ServicesManagment.Localization;
using Assets.Codebase.Presenters.Base;
using Assets.Codebase.Utils.Extensions;
using Assets.Codebase.Utils.Helpers;
using Assets.Codebase.Utils.Values;
using Assets.Codebase.Views.Base;
using UniRx;

namespace Assets.Codebase.Presenters.MainMenu
{
    public class MainMenuPresenter : BasePresenter, IMainMenuPresenter
    {
        public ReactiveProperty<string> TotalTrainingsText { get; private set; }
        public ReactiveProperty<string> CurrentLevelText { get; private set; }
        public ReactiveProperty<string> TotalPushupsText { get; private set; }
        public ReactiveProperty<string> NextPushupsTargetText { get; private set; }
        public ReactiveProperty<float> PushupsSliderValue { get; private set; }
        public ReactiveProperty<string> NextTrainingDateText { get; private set; }
        public ReactiveProperty<string> NextTrainingLevelText { get; private set; }
        public ReactiveProperty<string> NextTrainingNameText { get; private set; }
        public ReactiveProperty<string> NextTrainingPushupListText { get; private set; }

        public MainMenuPresenter()
        {
            ViewId = ViewId.MainView;

            TotalTrainingsText = new ReactiveProperty<string>();
            CurrentLevelText = new ReactiveProperty<string>();
            TotalPushupsText = new ReactiveProperty<string>();
            NextPushupsTargetText = new ReactiveProperty<string>();
            PushupsSliderValue = new ReactiveProperty<float>();
            NextTrainingDateText = new ReactiveProperty<string>();
            NextTrainingLevelText = new ReactiveProperty<string>();
            NextTrainingNameText = new ReactiveProperty<string>();
            NextTrainingPushupListText = new ReactiveProperty<string>();
        }

        public override void CreateView()
        {
            base.CreateView();
            ConfigureView();
        }

        protected override void SubscribeToModelChanges()
        {
            base.SubscribeToModelChanges();

            // foreach info widget
            ProgressModel.SessionProgress.CurrentTrainingPlan.Subscribe(plan => CurrentLevelText.Value = plan?.Level.ToString()).AddTo(CompositeDisposable);
        }

        public void ChangePlan()
        {
            GameplayModel.ActivateView(ViewId.PlanSelectionView);
        }


        public void ShowAchievements()
        {
            ServiceLocator.Container.Single<IAchievementsService>().ShowAchievementsList();
        }

        public void StartTraining()
        {
            GameplayModel.StartTrainingTimer();
            if (ProgressModel.SessionProgress.IsWarmupEnabled.Value)
            {
                GameplayModel.CurrentWarmupMode.Value = WarmupMode.Warmup;
                GameplayModel.ActivateView(ViewId.WarmupView);
            }
            else
            {
                GameplayModel.ActivateView(ViewId.TrainingView);
            }
        }

        public void GoToStatistics()
        {
            GameplayModel.ActivateView(ViewId.StatsView);
        }

        public void GoToSettings()
        {
            GameplayModel.ActivateView(ViewId.SettingsView);
        }

        
        private void ConfigureView()
        {
            var localizationService = ServiceLocator.Container.Single<ILocalizationService>();
            var currentPushups = ProgressModel.SessionProgress.TotalPushups.Value;
            var pushupsTarget = Constants.PushupTargets[ProgressModel.SessionProgress.NextPushupTargetId.Value];

            TotalTrainingsText.Value = NumberConverter.Convert(ProgressModel.SessionProgress.AllResults.Count);
            CurrentLevelText.Value = ProgressModel.SessionProgress.CurrentTrainingPlan.Value.Level.ToString();
            TotalPushupsText.Value = NumberConverter.Convert(currentPushups);
            NextPushupsTargetText.Value = NumberConverter.Convert(pushupsTarget);
            PushupsSliderValue.Value = currentPushups / pushupsTarget;
            NextTrainingDateText.Value = ProgressModel.SessionProgress.NextTrainingDate.Value.DateTime.Date.ToShortDateString();
            NextTrainingLevelText.Value = localizationService.LocalizeTextByKey(Constants.LevelWordKey) + " " + ProgressModel.SessionProgress.CurrentTrainingPlan.Value.Level.ToString();

            if (ProgressModel.SessionProgress.IsOnTestingStage.Value)
            {
                NextTrainingNameText.Value = localizationService.LocalizeTextByKey(Constants.TestTrainingNameKey);
                NextTrainingPushupListText.Value = ProgressModel.SessionProgress.CurrentTrainingPlan.Value.TestDay.Pushups[0].ToString();
            }
            else
            {
                NextTrainingNameText.Value = localizationService.LocalizeTextByKey(Constants.DayTrainingNameKey) + " " + ProgressModel.SessionProgress.CurrentTrainingDayId.Value.ToString();
                NextTrainingPushupListText.Value = ProgressModel.SessionProgress.CurrentTrainingPlan.Value.TrainingDays[ProgressModel.SessionProgress.CurrentTrainingDayId.Value - 1].Pushups.ToPushupsListString();
            }
        }
    }
}
