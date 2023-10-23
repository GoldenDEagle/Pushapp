using Assets.Codebase.Presenter.Base;
using Assets.Codebase.Views.PlanPreview;
using Assets.Codebase.Views.PlanSelection;
using UniRx;

namespace Assets.Codebase.Presenters.PlanPreview
{
    public interface IPlanPreviewPresenter : IPresenter
    {
        public Subject<TrainingDayWidget> OnTrainingDayAdded { get; }

        public void SelectPlan();
    }
}
