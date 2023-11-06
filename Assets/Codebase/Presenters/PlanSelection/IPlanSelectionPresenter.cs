using Assets.Codebase.Models.Progress.Data.TrainingPlans;
using Assets.Codebase.Presenter.Base;
using Assets.Codebase.Views.PlanSelection;
using UniRx;

namespace Assets.Codebase.Presenters.PlanSelection
{
    public interface IPlanSelectionPresenter : IPresenter
    {
        public ReactiveProperty<bool> IsBackButtonActive { get; }
        public Subject<TrainingPlanButton> OnPlanButtonAdded { get; }

        public void ViewPlan(TrainingPlan plan);
        public void BackToMenu();
    }
}
