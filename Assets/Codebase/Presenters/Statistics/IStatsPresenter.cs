using Assets.Codebase.Presenter.Base;
using UniRx;

namespace Assets.Codebase.Presenters.Statistics
{
    public interface IStatsPresenter : IPresenter
    {
        public ReactiveProperty<string> CurrentLevelString { get; }
        

        public void GoToMain();
        public void GoToSettings();
    }
}
