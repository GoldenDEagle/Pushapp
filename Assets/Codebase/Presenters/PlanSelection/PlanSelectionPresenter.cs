using Assets.Codebase.Data.Trainings;
using Assets.Codebase.Models.Progress.Data.TrainingPlans;
using Assets.Codebase.Presenters.Base;
using Assets.Codebase.Views.Base;
using System.Collections.Generic;

namespace Assets.Codebase.Presenters.PlanSelection
{
    public class PlanSelectionPresenter : BasePresenter, IPlanSelectionPresenter
    {
        private List<TrainingPlan> _trainingPlan;

        public PlanSelectionPresenter()
        {
            ViewId = ViewId.PlanSelectionView;
            _trainingPlan = GameplayModel.TrainingPlansDescriptions.TrainingPlans;
        }

        protected override void SubscribeToModelChanges()
        {
            base.SubscribeToModelChanges();
        }

        public void SelectPlan(TrainingPlan plan)
        {
            ProgressModel.ReactiveProgress.CurrentTrainingPlan.Value = plan;
        }
    }
}
