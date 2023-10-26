using Assets.Codebase.Presenter.Base;

namespace Assets.Codebase.Presenters.Statistics
{
    public interface IStatsPresenter : IPresenter
    {
        public void GoToMain();
        public void GoToSettings();
    }
}
