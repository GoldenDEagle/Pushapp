using Assets.Codebase.Presenter.Base;
using Assets.Codebase.Presenters.PlanPreview;
using Assets.Codebase.Views.Base;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Codebase.Views.PlanPreview
{
    public class PlanPreviewView : BaseView
    {
        [SerializeField] private RectTransform _widgetsGroup;
        [SerializeField] private Button _backButton;
        [SerializeField] private Button _acceptButton;

        private IPlanPreviewPresenter _presenter;

        public override void Init(IPresenter presenter)
        {
            _presenter = presenter as IPlanPreviewPresenter;

            base.Init(_presenter);
        }

        protected override void SubscribeToUserInput()
        {
            _acceptButton.OnClickAsObservable().Subscribe(_ => _presenter.SelectPlan()).AddTo(CompositeDisposable);
            _backButton.OnClickAsObservable().Subscribe(_ => _presenter.BackToSelection()).AddTo(CompositeDisposable);
        }

        protected override void SubscribeToPresenterEvents()
        {
            base.SubscribeToPresenterEvents();
            _presenter.OnTrainingDayAdded.Subscribe(item => AddDayWidget(item)).AddTo(CompositeDisposable);
        }


        /// <summary>
        /// Manages day info layout
        /// </summary>
        /// <param name="planButton"></param>
        private void AddDayWidget(TrainingDayWidget planButton)
        {
            planButton.transform.SetParent(_widgetsGroup, false);
        }
    }
}
