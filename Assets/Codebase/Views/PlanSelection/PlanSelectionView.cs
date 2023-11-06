using Assets.Codebase.Presenter.Base;
using Assets.Codebase.Presenters.PlanSelection;
using Assets.Codebase.Views.Base;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Codebase.Views.PlanSelection
{
    public class PlanSelectionView : BaseView
    {
        [SerializeField] private Button _backButton;
        [SerializeField] private LayoutGroup _levelButtonsLayout;

        private IPlanSelectionPresenter _presenter;

        public override void Init(IPresenter presenter)
        {
            _presenter = presenter as IPlanSelectionPresenter;

            base.Init(_presenter);
        }

        protected override void SubscribeToUserInput()
        {
            _backButton.OnClickAsObservable().Subscribe(_ => _presenter.BackToMenu()).AddTo(CompositeDisposable);
        }

        protected override void SubscribeToPresenterEvents()
        {
            base.SubscribeToPresenterEvents();
            _presenter.OnPlanButtonAdded.Subscribe(_ => AddPlanButton(_)).AddTo(CompositeDisposable);
        }


        /// <summary>
        /// Manages plan buttons layout
        /// </summary>
        /// <param name="planButton"></param>
        private void AddPlanButton(TrainingPlanButton planButton)
        {
            // parent under layout group
            planButton.transform.SetParent(_levelButtonsLayout.transform, false);
            planButton.OnPlanSelected.Subscribe(plan => _presenter.ViewPlan(plan)).AddTo(CompositeDisposable);
        }
    }
}
