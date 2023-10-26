using Assets.Codebase.Presenter.Base;

namespace Assets.Codebase.Presenters.Settings
{
    public interface ISettingsPresenter : IPresenter
    {
        public void GoToMain();
        public void GoToStatistics();
    }
}