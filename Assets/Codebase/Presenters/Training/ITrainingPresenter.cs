using Assets.Codebase.Data.Tutorial;
using Assets.Codebase.Presenter.Base;
using UniRx;

namespace Assets.Codebase.Presenters.Training
{
    public interface ITrainingPresenter : IPresenter
    {
        // Events
        public Subject<Unit> OnShowRestingWidget { get; }
        public Subject<Unit> OnHideRestingWidget { get; }

        // Main
        public ReactiveProperty<string> CurrentPushupCountText { get; }
        public ReactiveProperty<string> TrainingLiveResults { get; }
        public ReactiveProperty<string> TrainingNameString { get; }
        public ReactiveProperty<string> TotalPushupsString { get; }
        public ReactiveProperty<string> CurrentApproachString { get; }

        // Resting widget
        public ReactiveProperty<string> StathamPhrase { get; }
        public ReactiveProperty<string> TimerText { get; }
        public ReactiveProperty<float> TimerFillValue { get; }

        // Tutorial
        public ReactiveProperty<TutorialStep> CurrentTutorialStep { get; }
        public ReactiveProperty<bool> TutorialActiveState { get; }
        public ReactiveProperty<string> TutorialStepNumberString { get; }
        public ReactiveProperty<string> TutorialDescriptionString { get; }
        public ReactiveProperty<string> TutorialButtonString { get; }

        public void BackToMenu();
        public void CompleteStep();
        public void CancelResting();
        public void ShowResults();
        public void ReduceStepValue();
        public void IncreaseStepValue();
        public void IncreaseRestingTime();
        public void DecreaseRestingTime();
        public void GoToNextTutorialStage();
    }
}
