using Assets.Codebase.Presenters.Base;
using Assets.Codebase.Views.Base;

namespace Assets.Codebase.Presenters.PlanPreview
{
    public class PlanPreviewPresenter : BasePresenter, IPlanPreviewPresenter
    {
        public PlanPreviewPresenter()
        {
            ViewId = ViewId.PlanPreviewView;
        }

        public override void CreateView()
        {
            base.CreateView();

            // Fill plan info
        }

        public void SelectPlan()
        {
            ProgressModel.ReactiveProgress.CurrentTrainingPlan = GameplayModel.PreviewedPlan;
            GameplayModel.ActivateView(ViewId.MainView);
        }
    }
}
