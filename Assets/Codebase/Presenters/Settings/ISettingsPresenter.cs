using Assets.Codebase.Presenter.Base;
using UniRx;

namespace Assets.Codebase.Presenters.Settings
{
    public interface ISettingsPresenter : IPresenter
    {
        public ReactiveProperty<string> WarmupTimeString { get; }
        public ReactiveProperty<string> StretchingTimeString { get; }
        public ReactiveProperty<bool> IsAutoWarmupSwitchInteractable { get; }
        public ReactiveProperty<bool> IsAutoStretchingSwitchInteractable { get; }

        public void GoToMain();
        public void GoToStatistics();

        public void SetSoundVolume(float value);
        public void SetWarmupStatus(bool isEnabled);
        public void SetAutoWarmupSwitch(bool isEnabled);
        public void SetStretchingStatus(bool isEnabled);
        public void SetAutoStretchingSwitch(bool isEnabled);
        public void DeleteTrainingDataClicked();
        //public void ValidateTimeInput(string inputText, TMP_InputField inputField);
        //public void SetWarmupExerciseTime(string formattedTime);
        //public void SetStretchingExerciseTime(string formattedTime);
        public void SetWarmupExerciseTime(float timeInSeconds);
        public void SetStretchingExerciseTime(float timeInSeconds);
    }
}