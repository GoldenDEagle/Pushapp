using Assets.Codebase.Presenter.Base;
using UniRx;

namespace Assets.Codebase.Presenters.MainMenu
{
    public interface IMainMenuPresenter : IPresenter
    {
        public ReactiveProperty<string> TotalTrainingsText { get; }
        public ReactiveProperty<string> CurrentLevelText { get; }
        public ReactiveProperty<string> NextTrainingDate { get; }

        public void StartTraining();
        public void ChangePlan();
        public void ShowAchievements();

        public void GoToStatistics();
        public void GoToSettings();
    }
}
