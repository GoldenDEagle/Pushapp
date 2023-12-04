using Assets.Codebase.Data.Audio;
using Assets.Codebase.Data.Trainings;
using Assets.Codebase.Infrastructure.ServicesManagment;
using Assets.Codebase.Infrastructure.ServicesManagment.Audio;
using Assets.Codebase.Infrastructure.ServicesManagment.Localization;
using Assets.Codebase.Models.Progress.Data.TrainingPlans;
using Assets.Codebase.Presenters.Base;
using Assets.Codebase.Utils.Helpers;
using Assets.Codebase.Utils.Values;
using Assets.Codebase.Views.Base;
using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public ReactiveProperty<string> TrainingNameString { get; private set; }
        public ReactiveProperty<string> TotalPushupsString { get; private set; }

        // Resting widget
        public ReactiveProperty<string> StathamPhrase { get; private set; }
        public ReactiveProperty<string> TimerText { get; private set; }
        public ReactiveProperty<float> TimerFillValue { get; private set; }

        // General
        private TrainingDay _trainingDescription;
        private int _stepNumber;
        private int _currentStepValue;
        private List<int> _currentTrainingResults;
        private StringBuilder _resultsString;

        // Resting
        private float _defaultTimeToRest = 90f;
        private float _currentTimeToRest;
        private float _timeRegulatingStep = 30f;
        private float _restingTimer;
        private CancellationTokenSource _restingCancellationToken;

        public TrainingPresenter()
        {
            ViewId = ViewId.TrainingView;

            _currentTrainingResults = new List<int>();
            _resultsString = new StringBuilder();
            OnShowRestingWidget = new Subject<Unit>();
            OnHideRestingWidget = new Subject<Unit>();
            CurrentPushupCountText = new ReactiveProperty<string>(string.Empty);
            TrainingLiveResults = new ReactiveProperty<string>(string.Empty);
            TimerText = new ReactiveProperty<string>(string.Empty);
            TimerFillValue = new ReactiveProperty<float>(0f);
            StathamPhrase = new ReactiveProperty<string>(string.Empty);
            TrainingNameString = new ReactiveProperty<string>(string.Empty);
            TotalPushupsString = new ReactiveProperty<string>(string.Empty);
        }

        public override void CreateView()
        {
            _stepNumber = 0;
            _currentTrainingResults.Clear();

            var localizationService = ServiceLocator.Container.Single<ILocalizationService>();
            TrainingNameString.Value = localizationService.LocalizeTextByKey(Constants.LevelWordKey) + " " + ProgressModel.SessionProgress.CurrentTrainingPlan.Value.Level.ToString();

            // If test
            if (ProgressModel.SessionProgress.IsOnTestingStage.Value)
            {
                _trainingDescription = ProgressModel.SessionProgress.CurrentTrainingPlan.Value.TestDay;
                TrainingNameString.Value += " " + localizationService.LocalizeTextByKey(Constants.TestTrainingNameKey);
            }
            // If regular day
            else
            {
                _trainingDescription = ProgressModel.SessionProgress.CurrentTrainingPlan.Value.TrainingDays[ProgressModel.SessionProgress.CurrentTrainingDayId.Value - 1];
                TrainingNameString.Value += " " + localizationService.LocalizeTextByKey(Constants.DayTrainingNameKey) + " " + ProgressModel.SessionProgress.CurrentTrainingDayId.ToString();
            }
            _currentStepValue = _trainingDescription.Pushups[0];

            base.CreateView();

            ShowStepInfo();
        }

        public void BackToMenu()
        {
            // Show warning
            GameplayModel.GetTrainingTime();
            GameplayModel.ActivateView(ViewId.MainView);
        }

        public void CompleteStep()
        {
            _currentTrainingResults.Add(_currentStepValue);
            _stepNumber++;

            if (_stepNumber >= _trainingDescription.Pushups.Count)
            {
                // Save training result if last step
                SaveResults();
                ShowResults();
                return;
            }

            _currentStepValue = _trainingDescription.Pushups[_stepNumber];

            OnShowRestingWidget?.OnNext(Unit.Default);
            _currentTimeToRest = _defaultTimeToRest;
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


        // Internal ....................................................
        private void ShowStepInfo()
        {
            CurrentPushupCountText.Value = _currentStepValue.ToString();
            TrainingLiveResults.Value = CreateResultsString();

            if (_currentTrainingResults.Any())
            {
                TotalPushupsString.Value = NumberConverter.Convert(_currentTrainingResults.Sum());
            }
            else
            {
                TotalPushupsString.Value = 0.ToString();    
            }
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
            
            // Current
            _resultsString.Append("<size=130%> <color=#FFA902>").Append(_trainingDescription.Pushups[resultIndex]).Append("</color> </size>").Append(" ");
            resultIndex++;

            // plan
            while (resultIndex < _trainingDescription.Pushups.Count)
            {
                _resultsString.Append(_trainingDescription.Pushups[resultIndex]).Append(" ");
                resultIndex++;
            }

            return _resultsString.ToString();
        }

        private async UniTaskVoid StartResting()
        {
            CancelResting();
            StathamPhrase.Value = GameplayModel.GetRandomStathamPhrase();

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
            TimerFillValue.Value = 1f;
            var audioService = ServiceLocator.Container.Single<IAudioService>();

            _restingTimer = _currentTimeToRest;
            while (_restingTimer >= 0)
            {
                // final countdown
                if (_restingTimer == 3)
                {
                    audioService.PlaySfxSound(SoundId.FinalSeconds);
                }
                // reminder every 30 sec
                if (_restingTimer % 30 == 0)
                {
                    if (_restingTimer != 0)
                    {
                        audioService.PlaySfxSound(SoundId.TimerPop);
                    }
                }

                TimerText.Value = TimeConverter.TimeInMinutes(_restingTimer);
                TimerFillValue.Value = (_currentTimeToRest - _restingTimer) / _currentTimeToRest;
                _restingTimer--;
                await UniTask.Delay(TimeSpan.FromSeconds(1), false, PlayerLoopTiming.Update, cancellationToken);
            }

            OnHideRestingWidget?.OnNext(Unit.Default);
            ShowStepInfo();
        }

        private void SaveResults()
        {
            var totalPushups = _currentTrainingResults.Sum();
            ProgressModel.SessionProgress.AddPushups(totalPushups);

            TrainingResult trainingResult = new TrainingResult(_currentTrainingResults, totalPushups, TimeProvider.GetServerTime(), GameplayModel.GetTrainingTime());
            ProgressModel.SessionProgress.AddTrainingResult(trainingResult);
        }

        public void IncreaseRestingTime()
        {
            _currentTimeToRest += _timeRegulatingStep;
            _restingTimer += _timeRegulatingStep;
            TimerText.Value = TimeConverter.TimeInMinutes(_restingTimer);
            TimerFillValue.Value = (_currentTimeToRest - _restingTimer) / _currentTimeToRest;
        }

        public void DecreaseRestingTime()
        {
            _restingTimer -= _timeRegulatingStep;
            if (_restingTimer < 0)
            {
                CancelResting();
                return;
            }
            _currentTimeToRest -= _timeRegulatingStep;
            TimerText.Value = TimeConverter.TimeInMinutes(_restingTimer);
            TimerFillValue.Value = (_currentTimeToRest - _restingTimer) / _currentTimeToRest;
        }
    }
}
