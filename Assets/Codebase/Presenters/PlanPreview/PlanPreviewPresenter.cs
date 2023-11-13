using Assets.Codebase.Infrastructure.ServicesManagment;
using Assets.Codebase.Infrastructure.ServicesManagment.Localization;
using Assets.Codebase.Infrastructure.ServicesManagment.UI;
using Assets.Codebase.Presenters.Base;
using Assets.Codebase.Utils.Values;
using Assets.Codebase.Views.Base;
using Assets.Codebase.Views.PlanPreview;
using UniRx;

namespace Assets.Codebase.Presenters.PlanPreview
{
    public class PlanPreviewPresenter : BasePresenter, IPlanPreviewPresenter
    {
        public Subject<TrainingDayWidget> OnTrainingDayAdded { get; private set; }

        public PlanPreviewPresenter()
        {
            ViewId = ViewId.PlanPreviewView;
            OnTrainingDayAdded = new Subject<TrainingDayWidget>();
        }

        public override void CreateView()
        {
            base.CreateView();

            SummonDayWidgets();
        }

        private void SummonDayWidgets()
        {
            var uiFactory = ServiceLocator.Container.Single<IUiFactory>();

            for (int i = 0; i < GameplayModel.PreviewedPlan.Value.TrainingDays.Count; i++)
            {
                var widget = uiFactory.CreateTrainingDayWidget();
                var planInfo = GameplayModel.PreviewedPlan.Value;
                widget.Init(planInfo.TrainingDays[i], i + 1, Constants.LevelColors[planInfo.DifficultyLevel]);
                OnTrainingDayAdded?.OnNext(widget);
            }
        }

        public void SelectPlan()
        {
            if (ProgressModel.SessionProgress.IsTrainingPlanSelected.Value)
            {
                // Show warning
                var localizationService = ServiceLocator.Container.Single<ILocalizationService>();
                var warningWindow = ServiceLocator.Container.Single<IUiFactory>().CreateWarningWindow();
                warningWindow.SetWarningText(localizationService.LocalizeTextByKey(Constants.ProgramSwitchWarningKey));
                warningWindow.OnWindowClosed.Subscribe(value => OnWarningWindowClosed(value)).AddTo(CompositeDisposable);
            }
            else
            {
                PickAProgram();
            }
        }

        public void BackToSelection()
        {
            GameplayModel.ActivateView(ViewId.PlanSelectionView);
        }


        private void PickAProgram()
        {
            ProgressModel.SessionProgress.IsTrainingPlanSelected.Value = true;
            ProgressModel.SessionProgress.CurrentTrainingPlan.Value = GameplayModel.PreviewedPlan.Value;
            ProgressModel.SessionProgress.CurrentTrainingDayId.Value = 1;
            GameplayModel.PreviewedPlan.Value = null;
            GameplayModel.ActivateView(ViewId.MainView);
        }

        private void OnWarningWindowClosed(bool wasAccepted)
        {
            if (wasAccepted)
            {
                PickAProgram();
            }
        }
    }
}
