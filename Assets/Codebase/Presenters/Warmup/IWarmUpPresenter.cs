using Assets.Codebase.Data.WarmUp;
using Assets.Codebase.Presenter.Base;
using UniRx;

namespace Assets.Codebase.Presenters.Warmup
{
    public interface IWarmUpPresenter : IPresenter
    {
        public ReactiveProperty<bool> IsBackButtonActive { get; }
        /// <summary>
        /// Description of exercise
        /// </summary>
        public ReactiveProperty<string> StepDescriptionString { get; }
        /// <summary>
        /// Step number in format "1/10"
        /// </summary>
        public ReactiveProperty<string> StepNumberString { get; }
        /// <summary>
        /// Fired when step is changed
        /// </summary>
        public Subject<WarmupStep> OnNewWarmupStep { get; }
        /// <summary>
        /// Timer text 00:00
        /// </summary>
        public ReactiveProperty<string> TimerText { get; }
        /// <summary>
        /// Fill value of timer slider
        /// </summary>
        public ReactiveProperty<float> TimerSliderValue { get; }
        /// <summary>
        /// Is timer enabled?
        /// </summary>
        public ReactiveProperty<bool> IsTimerEnabled { get; }
        /// <summary>
        /// Current animation clip
        /// </summary>
        public ReactiveProperty<SimpleAnimationClip> WarmupAnimationClip { get; }

        public void StartWarmup();
        public void SkipWarmup();
        public void GoToNextExcercise();
        public void BackToMenu();
    }
}
