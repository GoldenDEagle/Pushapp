using Assets.Codebase.Presenter.Base;

namespace Assets.Codebase.Presenters.Warmup
{
    public interface IWarmUpPresenter : IPresenter
    {
        public void StartWarmup();
        public void SkipWarmup();
        public void GoToNextExcercise();
        public void BackToMenu();
    }
}
