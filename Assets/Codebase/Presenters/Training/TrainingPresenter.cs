using Assets.Codebase.Data.WarmUp;
using Assets.Codebase.Models.Progress.Data.TrainingPlans;
using Assets.Codebase.Presenters.Base;
using Assets.Codebase.Utils.Helpers;
using Assets.Codebase.Views.Base;
using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using UniRx;

namespace Assets.Codebase.Presenters.Training
{
    public class TrainingPresenter : BasePresenter, ITrainingPresenter
    {
        public Subject<Unit> OnShowRestingWidget { get; private set; }
        public Subject<Unit> OnHideRestingWidget { get; private set; }

        // General screen
        public ReactiveProperty<string> CurrentPushupCountText { get; private set; }
        public ReactiveProperty<string> TrainingLiveResults { get; private set; }

        // Resting widget
        public ReactiveProperty<string> TimerText { get; private set; }
        public ReactiveProperty<float> TimerSliderValue { get; private set; }


        private TrainingDay _description;
        private int _stepNumber;
        private int _currentStepValue;
        private float _secondsToRest = 60f;
        private List<int> _currentTrainingResults;
        private StringBuilder _resultsString;
        private CancellationTokenSource _restingCancellationToken;

        public TrainingPresenter()
        {
            ViewId = ViewId.TrainingView;

            _currentTrainingResults = new List<int>();
            _resultsString = new StringBuilder();
        }

        public override void CreateView()
        {
            _stepNumber = 0;
            _description = ProgressModel.ReactiveProgress.CurrentTrainingDay.Value;
            _currentStepValue = _description.Pushups[0];
            _currentTrainingResults.Clear();

            base.CreateView();

            ShowStepInfo();
        }

        public void BackToMenu()
        {
            // Show warning

            GameplayModel.ActivateView(ViewId.MainView);
        }

        public void CompleteStep()
        {
            _currentTrainingResults.Add(_currentStepValue);
            _stepNumber++;

            if (_stepNumber > _description.Pushups.Count)
            {
                // Save training result
                _stepNumber = 0;
                GameplayModel.ActivateView(ViewId.TrainingResultView);
                return;
            }

            OnShowRestingWidget?.OnNext(Unit.Default);
            StartResting().Forget();
        }

        public void ShowResults()
        {
            GameplayModel.ActivateView(ViewId.TrainingResultView);
        }

        public void CancelResting()
        {
            if (_restingCancellationToken != null)
            {
                _restingCancellationToken.Cancel();
                _restingCancellationToken.Dispose();
                _restingCancellationToken = null;
            }
        }

        public void ReduceStepValue()
        {
            _currentStepValue--;
            if (_currentStepValue < 0) { _currentStepValue = 0; }
            CurrentPushupCountText.Value = _currentStepValue.ToString();
        }

        public void IncreaseStepValue()
        {
            _currentStepValue++;
            if (_currentStepValue > 999) { _currentStepValue = 999; }
            CurrentPushupCountText.Value = _currentStepValue.ToString();
        }


        private void ShowStepInfo()
        {
            CurrentPushupCountText.Value = _currentStepValue.ToString();
            TrainingLiveResults.Value = CreateResultsString();
        }

        private string CreateResultsString()
        {
            _resultsString.Clear();
            int resultIndex = 0;

            // results
            while (resultIndex < _currentTrainingResults.Count)
            {
                _resultsString.Append(_currentTrainingResults[resultIndex]).Append(" ");
                resultIndex++;
            }
            // plan
            while (resultIndex < _description.Pushups.Count)
            {
                _resultsString.Append(_currentTrainingResults[resultIndex]).Append(" ");
                resultIndex++;
            }

            return _resultsString.ToString();
        }

        private async UniTaskVoid StartResting()
        {
            if (_restingCancellationToken != null)
            {
                _restingCancellationToken.Cancel();
                _restingCancellationToken.Dispose();
            }

            _restingCancellationToken = new CancellationTokenSource();

            try
            {
                await RestingTask(_restingCancellationToken.Token);
            }
            catch (OperationCanceledException)
            {
                // Handle cancellation, if needed
                OnHideRestingWidget?.OnNext(Unit.Default);
                ShowStepInfo();
            }
        }

        private async UniTask RestingTask(CancellationToken cancellationToken)
        {
            TimerSliderValue.Value = 1f;

            var timer = _secondsToRest;
            while (timer >= 0)
            {
                TimerText.Value = TimeConverter.TimeInMinutes(timer);
                TimerSliderValue.Value = timer / _secondsToRest;
                timer--;
                await UniTask.Delay(TimeSpan.FromSeconds(1), false, PlayerLoopTiming.Update, cancellationToken);
            }

            OnHideRestingWidget?.OnNext(Unit.Default);
            ShowStepInfo();
        }
    }
}
