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

        // Resting widget
        public ReactiveProperty<string> StathamPhrase { get; }
        public ReactiveProperty<string> TimerText { get; }
        public ReactiveProperty<float> TimerFillValue { get; }

        public void BackToMenu();
        public void CompleteStep();
        public void CancelResting();
        public void ShowResults();
        public void ReduceStepValue();
        public void IncreaseStepValue();
        public void IncreaseRestingTime();
        public void DecreaseRestingTime();
    }
}
