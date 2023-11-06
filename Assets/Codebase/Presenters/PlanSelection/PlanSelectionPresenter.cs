using Assets.Codebase.Data.Trainings;
using Assets.Codebase.Infrastructure.ServicesManagment;
using Assets.Codebase.Infrastructure.ServicesManagment.UI;
using Assets.Codebase.Models.Progress.Data.TrainingPlans;
using Assets.Codebase.Presenters.Base;
using Assets.Codebase.Views.Base;
using Assets.Codebase.Views.PlanSelection;
using System.Collections.Generic;
using UniRx;

namespace Assets.Codebase.Presenters.PlanSelection
{
    public class PlanSelectionPresenter : BasePresenter, IPlanSelectionPresenter
    {
        public Subject<TrainingPlanButton> OnPlanButtonAdded { get; private set; }

        private List<TrainingPlan> _trainingPlans;

        public PlanSelectionPresenter()
        {
            ViewId = ViewId.PlanSelectionView;

            OnPlanButtonAdded = new Subject<TrainingPlanButton>();
        }

        public override void CreateView()
        {
            base.CreateView();

            _trainingPlans = GameplayModel.TrainingPlansDescriptions.TrainingPlans;

            SummonPlanButtons();
        }

        private void SummonPlanButtons()
        {
            var uiFactory = ServiceLocator.Container.Single<IUiFactory>();
            foreach (var plan in _trainingPlans)
            {
                var button = uiFactory.CreateTrainingPlanButton(plan);
                OnPlanButtonAdded?.OnNext(button);
            }
        }

        protected override void SubscribeToModelChanges()
        {
            base.SubscribeToModelChanges();
        }

        public void ViewPlan(TrainingPlan plan)
        {
            // With preview
            //GameplayModel.PreviewedPlan.Value = plan;
            //GameplayModel.ActivateView(ViewId.PlanPreviewView);


            // Without preview
            ProgressModel.SessionProgress.IsTrainingPlanSelected.Value = true;
            ProgressModel.SessionProgress.CurrentTrainingPlan.Value = plan;
            ProgressModel.SessionProgress.CurrentTrainingDayId.Value = 1;
            GameplayModel.ActivateView(ViewId.MainView);
        }

        public void BackToMenu()
        {
            // Better hide button
            if (!ProgressModel.SessionProgress.IsTrainingPlanSelected.Value) return;

            GameplayModel.ActivateView(ViewId.MainView);
        }
    }
}
