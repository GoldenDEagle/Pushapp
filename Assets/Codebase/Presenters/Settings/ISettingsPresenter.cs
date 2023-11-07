using Assets.Codebase.Presenter.Base;
using TMPro;
using UnityEngine.UI;

namespace Assets.Codebase.Presenters.Settings
{
    public interface ISettingsPresenter : IPresenter
    {
        public void GoToMain();
        public void GoToStatistics();

        public void SetSoundVolume(float value);
        public void SetWarmupStatus(bool isEnabled);
        public void SetAutoWarmupSwitch(bool isEnabled);
        public void SetStretchingStatus(bool isEnabled);
        public void SetAutoStretchingSwitch(bool isEnabled);
        public void DeleteAllTrainingData();
        public void ValidateTimeInput(string inputText, TMP_InputField inputField);
        public void SetWarmupExerciseTime(string formattedTime);
        public void SetStretchingExerciseTime(string formattedTime);
    }
}