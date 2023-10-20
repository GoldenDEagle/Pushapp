using Assets.Codebase.Models.Progress.Data.TrainingPlans;
using Assets.Codebase.Presenter.Base;

namespace Assets.Codebase.Presenters.PlanSelection
{
    public interface IPlanSelectionPresenter : IPresenter
    {
        public void SelectPlan(TrainingPlan plan);
    }
}
