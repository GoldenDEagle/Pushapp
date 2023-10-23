using Assets.Codebase.Infrastructure.ServicesManagment.UI;
using Assets.Codebase.Infrastructure.ServicesManagment;
using Assets.Codebase.Models.Progress.Data.TrainingPlans;
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
            ProgressModel.ReactiveProgress.CurrentTrainingPlan = GameplayModel.PreviewedPlan;
            GameplayModel.ActivateView(ViewId.MainView);
        }
    }
}
