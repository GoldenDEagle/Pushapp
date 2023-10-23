using Assets.Codebase.Presenter.Base;
using Assets.Codebase.Presenters.PlanSelection;
using Assets.Codebase.Views.Base;
using UniRx;

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
            _presenter.OnPlanButtonAdded.Subscribe(_ => AddPlanButton(_)).AddTo(CompositeDisposable);
        }

        private void AddPlanButton(TrainingPlanButton planButton)
        {
            // parent under layout group
            planButton.transform.SetParent(transform, false);
            planButton.OnPlanSelected.Subscribe(plan => _presenter.SelectPlan(plan)).AddTo(CompositeDisposable);
        }
    }
}
