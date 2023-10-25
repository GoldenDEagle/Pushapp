using Assets.Codebase.Infrastructure.ServicesManagment;
using Assets.Codebase.Infrastructure.ServicesManagment.UI;
using Assets.Codebase.Presenters.Base;
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
            foreach (var day in GameplayModel.PreviewedPlan.Value.TrainingDays)
            {
                var widget = uiFactory.CreateTrainingDayWidget(day);
                OnTrainingDayAdded?.OnNext(widget);
            }
        }

        public void SelectPlan()
        {
            ProgressModel.SessionProgress.IsTrainingPlanSelected.Value = true;
            ProgressModel.SessionProgress.CurrentTrainingPlan.Value = GameplayModel.PreviewedPlan.Value;
            ProgressModel.SessionProgress.CurrentTrainingDayId.Value = 1;
            GameplayModel.PreviewedPlan.Value = null;
            GameplayModel.ActivateView(ViewId.MainView);
        }

        public void BackToSelection()
        {
            GameplayModel.ActivateView(ViewId.PlanSelectionView);
        }
    }
}
