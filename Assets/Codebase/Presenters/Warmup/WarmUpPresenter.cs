using Assets.Codebase.Data.WarmUp;
using Assets.Codebase.Presenters.Base;
using Assets.Codebase.Utils.Helpers;
using Assets.Codebase.Views.Base;
using Cysharp.Threading.Tasks;
using System;
using UniRx;

namespace Assets.Codebase.Presenters.Warmup
{
    public class WarmUpPresenter : BasePresenter, IWarmUpPresenter
    {
        public Subject<WarmupStep> OnNewWarmupStep { get; private set; }
        public ReactiveProperty<string> TimerText { get; private set; }
        public ReactiveProperty<float> TimerSliderValue { get; private set; }


        private WarmupDescription _description;
        private int _stepNumber;
        private float _secondsToPrepare = 5f;

        public WarmUpPresenter()
        {
            ViewId = ViewId.WarmupView;

            OnNewWarmupStep = new Subject<WarmupStep>();
            TimerText = new ReactiveProperty<string>(string.Empty);
            TimerSliderValue = new ReactiveProperty<float>(1);
        }

        public override void CreateView()
        {
            _stepNumber = 0;
            _description = GameplayModel.GetWarmupDescription();

            base.CreateView();

            LaunchStep();
        }

        public void BackToMenu()
        {
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
            GameplayModel.ActivateView(ViewId.TrainingView);
        }

        public void StartWarmup()
        {
            throw new NotImplementedException();
        }


        private void LaunchStep()
        {
            OnNewWarmupStep?.OnNext(_description.Steps[_stepNumber]);
            ExerciseCycle().Forget();
        }

        private async UniTaskVoid ExerciseCycle()
        {
            TimerText.Value = "Приготовьтесь!";
            TimerSliderValue.Value = 1f;

            // Preparation
            var timer = _secondsToPrepare;
            while (timer >= 0)
            {
                TimerSliderValue.Value = timer / _secondsToPrepare;
                timer--;
                await UniTask.Delay(TimeSpan.FromSeconds(1));
            }

            // Exercise
            timer = _description.Steps[_stepNumber].StepDurationSeconds;
            while (timer >= 0)
            {
                TimerText.Value = TimeConverter.TimeInMinutes(timer);
                TimerSliderValue.Value = timer / _description.Steps[_stepNumber].StepDurationSeconds;
                timer--;
                await UniTask.Delay(TimeSpan.FromSeconds(1));
            }

            GoToNextExcercise();
        }
    }
}
