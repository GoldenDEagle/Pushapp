﻿using Assets.Codebase.Data.Audio;
using Assets.Codebase.Data.WarmUp;
using Assets.Codebase.Infrastructure.ServicesManagment;
using Assets.Codebase.Infrastructure.ServicesManagment.Audio;
using Assets.Codebase.Infrastructure.ServicesManagment.Localization;
using Assets.Codebase.Presenters.Base;
using Assets.Codebase.Utils.Helpers;
using Assets.Codebase.Views.Base;
using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using UniRx;

namespace Assets.Codebase.Presenters.Warmup
{
    public class WarmUpPresenter : BasePresenter, IWarmUpPresenter
    {
        public ReactiveProperty<bool> IsBackButtonActive { get; private set; }
        public ReactiveProperty<string> StepDescriptionString { get; private set; }
        public ReactiveProperty<string> StepNumberString { get; private set; }
        public Subject<WarmupStep> OnNewWarmupStep { get; private set; }
        public ReactiveProperty<string> TimerText { get; private set; }
        public ReactiveProperty<float> TimerSliderValue { get; private set; }
        public ReactiveProperty<bool> IsTimerEnabled { get; private set; }
        public ReactiveProperty<SimpleAnimationClip> WarmupAnimationClip { get; private set; }

        private const string GetReadyTimerKey = "timer_getReady";

        private WarmupDescription _description;
        private int _stepNumber;
        private float _secondsToPrepare = 5f;
        private float _exerciseTime;
        private CancellationTokenSource _timerCancellationToken;

        public WarmUpPresenter()
        {
            ViewId = ViewId.WarmupView;

            OnNewWarmupStep = new Subject<WarmupStep>();
            TimerText = new ReactiveProperty<string>(string.Empty);
            TimerSliderValue = new ReactiveProperty<float>(1);
            IsTimerEnabled = new ReactiveProperty<bool>();
            IsBackButtonActive = new ReactiveProperty<bool>(false);
            StepDescriptionString = new ReactiveProperty<string>(string.Empty);
            StepNumberString = new ReactiveProperty<string>(string.Empty);
            WarmupAnimationClip = new ReactiveProperty<SimpleAnimationClip>(new SimpleAnimationClip());
        }

        public override void CreateView()
        {
            _stepNumber = 0;
            _description = GameplayModel.GetWarmupDescription();
            if (GameplayModel.CurrentWarmupMode.Value == WarmupMode.Warmup)
            {
                _exerciseTime = ProgressModel.SessionProgress.WarmupExerciseTime.Value;
                IsTimerEnabled.Value = ProgressModel.SessionProgress.AutoWarmupSwitchEnabled.Value;
                IsBackButtonActive.Value = true;
            }
            else
            {
                _exerciseTime = ProgressModel.SessionProgress.StretchingExerciseTime.Value;
                IsTimerEnabled.Value = ProgressModel.SessionProgress.AutoStretchingSwitchEnabled.Value;
                IsBackButtonActive.Value = false;
            }

            base.CreateView();

            LaunchStep();
        }

        public void BackToMenu()
        {
            StopTimer();
            GameplayModel.GetTrainingTime();
            GameplayModel.ActivateView(ViewId.MainView);
        }

        public void GoToNextExcercise()
        {
            _stepNumber++;

            if (_stepNumber >= _description.Steps.Count)
            {
                _stepNumber = 0;
                SkipWarmup();
                return;
            }

            LaunchStep();
        }

        public void SkipWarmup()
        {
            StopTimer();

            switch (GameplayModel.CurrentWarmupMode.Value)
            {
                case WarmupMode.Warmup:
                    GameplayModel.ActivateView(ViewId.TrainingView);
                    break;
                case WarmupMode.Stretching:
                    GameplayModel.ActivateView(ViewId.MainView);
                    break;
                default:
                    break;
            }
        }

        public void StartWarmup()
        {
            throw new NotImplementedException();
        }


        private void LaunchStep()
        {
            SetStepInfo();

            // Launch timers if enabled in settings
            if ((GameplayModel.CurrentWarmupMode.Value == WarmupMode.Warmup && ProgressModel.SessionProgress.AutoWarmupSwitchEnabled.Value)
            || (GameplayModel.CurrentWarmupMode.Value == WarmupMode.Stretching && ProgressModel.SessionProgress.AutoStretchingSwitchEnabled.Value))
            {
                StopTimer();
                _timerCancellationToken = new CancellationTokenSource();
                ExerciseCycle(_timerCancellationToken.Token).Forget();
            }
        }

        public void StopTimer()
        {
            if (_timerCancellationToken != null)
            {
                _timerCancellationToken.Cancel();
                _timerCancellationToken.Dispose();
                _timerCancellationToken = null;
            }
        }

        private async UniTask ExerciseCycle(CancellationToken cancellationToken)
        {
            TimerText.Value = ServiceLocator.Container.Single<ILocalizationService>().LocalizeTextByKey(GetReadyTimerKey);
            TimerSliderValue.Value = 1f;
            var audioService = ServiceLocator.Container.Single<IAudioService>();

            // Preparation
            var timer = _secondsToPrepare;
            while (timer >= 0)
            {
                TimerSliderValue.Value = timer / _secondsToPrepare;
                timer--;
                await UniTask.Delay(TimeSpan.FromSeconds(1), false, PlayerLoopTiming.Update, cancellationToken);
            }

            // Exercise
            //timer = _description.Steps[_stepNumber].StepDurationSeconds;
            timer = _exerciseTime;
            while (timer >= 0)
            {
                if (timer == 3)
                {
                    audioService.PlaySfxSound(SoundId.FinalSeconds);
                }

                TimerText.Value = TimeConverter.TimeInMinutes(timer);
                //TimerSliderValue.Value = timer / _description.Steps[_stepNumber].StepDurationSeconds;
                TimerSliderValue.Value = timer / _exerciseTime;
                timer--;
                await UniTask.Delay(TimeSpan.FromSeconds(1), false, PlayerLoopTiming.Update, cancellationToken);
            }

            GoToNextExcercise();
        }

        private void SetStepInfo()
        {
            var currentStep = _description.Steps[_stepNumber];
            StepDescriptionString.Value = ServiceLocator.Container.Single<ILocalizationService>().LocalizeTextByKey(currentStep.StepDescription);
            StepNumberString.Value = (_stepNumber + 1) + " / " + _description.Steps.Count;
            WarmupAnimationClip.Value = _description.Steps[_stepNumber].StepGraphic;
        }
    }
}
