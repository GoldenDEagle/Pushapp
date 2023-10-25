using Assets.Codebase.Presenters.Base;
using Assets.Codebase.Views.Base;
using Assets.Codebase.Data.WarmUp;
using UniRx;

namespace Assets.Codebase.Presenters.MainMenu
{
    public class MainMenuPresenter : BasePresenter, IMainMenuPresenter
    {
        public ReactiveProperty<string> TotalTrainingsText { get; private set; }

        public ReactiveProperty<string> CurrentLevelText { get; private set; }

        public ReactiveProperty<string> NextTrainingDate { get; private set; }

        public MainMenuPresenter()
        {
            ViewId = ViewId.MainView;

            TotalTrainingsText = new ReactiveProperty<string>();
            CurrentLevelText = new ReactiveProperty<string>();
            NextTrainingDate = new ReactiveProperty<string>();
        }

        protected override void SubscribeToModelChanges()
        {
            base.SubscribeToModelChanges();

            // foreach info widget
            ProgressModel.SessionProgress.CurrentTrainingPlan.Subscribe(plan => CurrentLevelText.Value = plan.Level.ToString()).AddTo(CompositeDisposable);
        }

        public void ChangePlan()
        {
            GameplayModel.ActivateView(ViewId.PlanSelectionView);
        }


        public void ShowAchievements()
        {
            GameplayModel.ActivateView(ViewId.AchievementsView);
        }

        public void StartTraining()
        {
            GameplayModel.CurrentWarmupMode.Value = WarmupMode.Warmup;
            GameplayModel.ActivateView(ViewId.TrainingView);
        }
    }
}
