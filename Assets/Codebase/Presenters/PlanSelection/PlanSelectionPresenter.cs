﻿using Assets.Codebase.Data.Trainings;
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
            _trainingPlans = GameplayModel.TrainingPlansDescriptions.TrainingPlans;
            OnPlanButtonAdded = new Subject<TrainingPlanButton>();
        }

        public override void CreateView()
        {
            base.CreateView();

            SummonPlanButtons();
        }

        private void SummonPlanButtons()
        {
            foreach (var plan in _trainingPlans)
            {
                var button = ServiceLocator.Container.Single<IUiFactory>().CreateTrainingPlanButton(plan);
                OnPlanButtonAdded?.OnNext(button);
            }
        }

        protected override void SubscribeToModelChanges()
        {
            base.SubscribeToModelChanges();
        }

        public void SelectPlan(TrainingPlan plan)
        {
            GameplayModel.PreviewedPlan.Value = plan;
            GameplayModel.ActivateView(ViewId.PlanPreviewView);
        }
    }
}
