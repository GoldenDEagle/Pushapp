﻿using Assets.Codebase.Data.WarmUp;
using Assets.Codebase.Presenter.Base;
using Assets.Codebase.Presenters.Warmup;
using Assets.Codebase.Utils.Extensions;
using Assets.Codebase.Views.Base;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Codebase.Views.Warmup
{
    public class WarmUpView : BaseView
    {
        [SerializeField] private Button _skipButton;
        [SerializeField] private Button _nextStepButton;
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
        }

        protected override void SubscribeToPresenterEvents()
        {
            base.SubscribeToPresenterEvents();
            _presenter.OnNewWarmupStep.Subscribe(step => ConfigureView(step)).AddTo(CompositeDisposable);
            _presenter.TimerText.SubscribeToTMPText(_timerText).AddTo(CompositeDisposable);
            _presenter.IsTimerEnabled.Subscribe(value => SetTimerState(value)).AddTo(CompositeDisposable);
            _presenter.TimerSliderValue.Subscribe(value => _timerSlider.value = value).AddTo(CompositeDisposable);
        }


        private void ConfigureView(WarmupStep step)
        {
            // Show step info (description, picture)
            _stepDescriptionText.text = step.StepDescription;
        }
        private void SetTimerState(bool isEnabled)
        {
            _timerObject.gameObject.SetActive(isEnabled);
        }
    }
}