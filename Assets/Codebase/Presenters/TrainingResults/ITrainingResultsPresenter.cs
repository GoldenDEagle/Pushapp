using Assets.Codebase.Presenter.Base;
using UniRx;

namespace Assets.Codebase.Presenters.TrainingResults
{
    public interface ITrainingResultsPresenter : IPresenter
    {
        public ReactiveProperty<string> ViewHeaderString { get; }
        public ReactiveProperty<string> LevelString { get; }
        public ReactiveProperty<string> TrainingDayString { get; }
        public ReactiveProperty<string> ResultsString { get; }
        public ReactiveProperty<string> TotalPushupsString { get; }
        public ReactiveProperty<string> TrainingDurationString { get; }
        public ReactiveProperty<string> BurntCaloriesString { get; }
        public ReactiveProperty<string> NextTrainingDateString { get; }

        public void GoNextClicked();
        public bool IsStretchingEnabled();
        public void StretchingToggleClicked(bool toggleState);
    }
}
