using Assets.Codebase.Presenter.Base;
using TMPro;
using UniRx;
using UnityEngine;

namespace Assets.Codebase.Presenters.MainMenu
{
    public interface IMainMenuPresenter : IPresenter
    {
        public ReactiveProperty<string> TotalTrainingsText { get; }
        public ReactiveProperty<string> CurrentLevelText { get; }
        public ReactiveProperty<string> TotalPushupsText { get; }
        public ReactiveProperty<string> NextPushupsTargetText { get; }
        public ReactiveProperty<float> PushupsSliderValue { get; }
        public ReactiveProperty<string> NextTrainingDateText { get; }
        public ReactiveProperty<string> NextTrainingLevelText { get; }
        public ReactiveProperty<string> NextTrainingNameText { get; }
        public ReactiveProperty<string> NextTrainingPushupListText { get; }

        public void StartTraining();
        public void ChangePlan();
        public void ShowAchievements();

        public void GoToStatistics();
        public void GoToSettings();
    }
}
