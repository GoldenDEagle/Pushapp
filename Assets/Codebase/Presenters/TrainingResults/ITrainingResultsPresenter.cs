using Assets.Codebase.Presenter.Base;
using UniRx;

namespace Assets.Codebase.Presenters.TrainingResults
{
    public interface ITrainingResultsPresenter : IPresenter
    {
        public ReactiveProperty<string> ResultsString { get; }

        public void GoNextClicked();
        public bool IsStretchingEnabled();
        public void StretchingToggleClicked(bool toggleState);
    }
}
