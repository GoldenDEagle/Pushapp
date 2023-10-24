using Assets.Codebase.Data.WarmUp;
using Assets.Codebase.Presenter.Base;
using UniRx;

namespace Assets.Codebase.Presenters.Warmup
{
    public interface IWarmUpPresenter : IPresenter
    {
        /// <summary>
        /// Fired when step is changed
        /// </summary>
        public Subject<WarmupStep> OnNewWarmupStep { get; }

        public void StartWarmup();
        public void SkipWarmup();
        public void GoToNextExcercise();
        public void BackToMenu();
    }
}
