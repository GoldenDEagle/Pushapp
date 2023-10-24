using Assets.Codebase.Presenter.Base;
using Assets.Codebase.Presenters.Training;
using Assets.Codebase.Utils.Extensions;
using Assets.Codebase.Views.Base;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Codebase.Views.Training
{
    public class TrainingView : BaseView
    {
        [SerializeField] private Button _backButton;
        [SerializeField] private Button _nextStepButton;
        [SerializeField] private Button _addPushupButton;
        [SerializeField] private Button _subtractPushupButton;
        [SerializeField] private TMP_Text _resultsLineText;
        [SerializeField] private TMP_Text _currentStepValue;
        [SerializeField] private RestingWidget _restingWidget;

        private ITrainingPresenter _presenter;

        public override void Init(IPresenter presenter)
        {
            _presenter = presenter as ITrainingPresenter;

            base.Init(presenter);
        }

        protected override void SubscribeToUserInput()
        {
            _backButton.OnClickAsObservable().Subscribe(_ => _presenter.BackToMenu()).AddTo(CompositeDisposable);
            _nextStepButton.OnClickAsObservable().Subscribe(_ => _presenter.CompleteStep()).AddTo(CompositeDisposable);
            _addPushupButton.OnClickAsObservable().Subscribe(_ => _presenter.IncreaseStepValue()).AddTo(CompositeDisposable);
            _subtractPushupButton.OnClickAsObservable().Subscribe(_ => _presenter.ReduceStepValue()).AddTo(CompositeDisposable);
        }

        protected override void SubscribeToPresenterEvents()
        {
            base.SubscribeToPresenterEvents();
            _presenter.OnShowRestingWidget.Subscribe(_ => ShowRestingWidget()).AddTo(CompositeDisposable);
            _presenter.OnHideRestingWidget.Subscribe(_ => HideRestingWidget()).AddTo(CompositeDisposable);

            _presenter.CurrentPushupCountText.SubscribeToTMPText(_currentStepValue).AddTo(CompositeDisposable);
            _presenter.TrainingLiveResults.SubscribeToTMPText(_resultsLineText).AddTo(CompositeDisposable);

            _presenter.TimerText.SubscribeToTMPText(_restingWidget.RestingTimerText).AddTo(CompositeDisposable);
        }


        // Add animation later
        private void ShowRestingWidget()
        {
            _restingWidget.gameObject.SetActive(true);
        }
        private void HideRestingWidget()
        {
            _restingWidget.gameObject.SetActive(false);
        }
    }
}
