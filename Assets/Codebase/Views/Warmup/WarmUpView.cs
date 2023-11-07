using Assets.Codebase.Presenter.Base;
using Assets.Codebase.Presenters.Warmup;
using Assets.Codebase.Utils.Extensions;
using Assets.Codebase.Views.Base;
using Assets.Codebase.Views.Common;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Codebase.Views.Warmup
{
    public class WarmUpView : BaseView
    {
        [SerializeField] private BottomPanel _bottomPanel;
        [SerializeField] private Button _backButton;
        [Header("Exercise")]
        [SerializeField] private Button _skipButton;
        [SerializeField] private Button _nextStepButton;
        [SerializeField] private TMP_Text _stepNumberText;
        [SerializeField] private TMP_Text _stepDescriptionText;
        [Header("Timer")]
        [SerializeField] private Transform _timerObject;
        [SerializeField] private TMP_Text _timerText;
        [SerializeField] private Slider _timerSlider;

        private IWarmUpPresenter _presenter;

        public override void Init(IPresenter presenter)
        {
            _presenter = presenter as IWarmUpPresenter;

            base.Init(presenter);
        }

        protected override void SubscribeToUserInput()
        {
            _skipButton.OnClickAsObservable().Subscribe(_ => _presenter.SkipWarmup()).AddTo(CompositeDisposable);
            _nextStepButton.OnClickAsObservable().Subscribe(_ => _presenter.GoToNextExcercise()).AddTo(CompositeDisposable);
            _backButton.OnClickAsObservable().Subscribe(_ => _presenter.BackToMenu()).AddTo(CompositeDisposable);
        }

        protected override void SubscribeToPresenterEvents()
        {
            base.SubscribeToPresenterEvents();
            _presenter.StepDescriptionString.SubscribeToTMPText(_stepDescriptionText).AddTo(CompositeDisposable);
            _presenter.StepNumberString.SubscribeToTMPText(_stepNumberText).AddTo(CompositeDisposable);
            _presenter.TimerText.SubscribeToTMPText(_timerText).AddTo(CompositeDisposable);
            _presenter.IsTimerEnabled.Subscribe(value => SetTimerState(value)).AddTo(CompositeDisposable);
            _presenter.TimerSliderValue.Subscribe(value => _timerSlider.value = value).AddTo(CompositeDisposable);
            _presenter.IsBackButtonActive.Subscribe(value => SetBackButtonState(value)).AddTo(CompositeDisposable);
        }


        //
        private void SetTimerState(bool isEnabled)
        {
            _timerObject.gameObject.SetActive(isEnabled);
        }
        private void SetBackButtonState(bool isEnabled)
        {
            _backButton.gameObject.SetActive(isEnabled);
        }
    }
}