using Assets.Codebase.Presenter.Base;
using Assets.Codebase.Presenter.MainMenu;
using Assets.Codebase.Presenters.PlanSelection;
using Assets.Codebase.Views.Base;
using System;
using TMPro;
using UniRx;
using UnityEngine;

namespace Assets.Codebase.Views.PlanSelection
{
    public class PlanSelectionView : BaseView
    {
        private IPlanSelectionPresenter _presenter;

        public override void Init(IPresenter presenter)
        {
            _presenter = presenter as IPlanSelectionPresenter;

            base.Init(_presenter);
        }

        protected override void SubscribeToUserInput()
        {
            
        }

        protected override void SubscribeToPresenterEvents()
        {
            base.SubscribeToPresenterEvents();
        }

        private void AddPlanButton(TrainingPlanButton planButton)
        {
            planButton.transform.SetParent(transform, false);
            planButton.OnPlanSelected.Subscribe(plan => _presenter.SelectPlan(plan)).AddTo(CompositeDisposable);
        }
    }
}
