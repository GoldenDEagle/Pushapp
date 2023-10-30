using Assets.Codebase.Presenters.Base;
using Assets.Codebase.Views.Base;
using UnityEngine.UI;

namespace Assets.Codebase.Presenters.Settings
{
    public class SettingsPresenter : BasePresenter, ISettingsPresenter
    {
        public SettingsPresenter()
        {
            ViewId = ViewId.SettingsView;
        }


        public void GoToMain()
        {
            GameplayModel.ActivateView(ViewId.MainView);
        }

        public void GoToStatistics()
        {
            GameplayModel.ActivateView(ViewId.StatsView);
        }

        public void SetSoundVolume(float value)
        {
            ProgressModel.SessionProgress.SoundVolume.Value = value;
        }

        public void SetStretchingStatus(bool isEnabled)
        {
            ProgressModel.SessionProgress.IsStretchingEnabled.Value = isEnabled;
        }

        public void SetWarmupStatus(bool isEnabled)
        {
            ProgressModel.SessionProgress.IsWarmupEnabled.Value = isEnabled;
        }

        public void SetAutoStretchingSwitch(bool isEnabled)
        {
            ProgressModel.SessionProgress.AutoStretchingSwitchEnabled.Value = isEnabled;
        }

        public void SetAutoWarmupSwitch(bool isEnabled)
        {
            ProgressModel.SessionProgress.AutoWarmupSwitchEnabled.Value = isEnabled;
        }

        public void DeleteAllTrainingData()
        {
            // Show warning

            // If accepted -> delete
            ProgressModel.SessionProgress.ClearResults();
        }

        public void ValidateTimeInput(string inputText, InputField inputField)
        {
            // Remove non-numeric characters from the input.
            string cleanedInput = string.Join("", System.Text.RegularExpressions.Regex.Split(inputText, "[^0-9]"));

            // Ensure the input doesn't exceed 5 characters (mm:ss).
            if (cleanedInput.Length > 5)
            {
                cleanedInput = cleanedInput.Substring(0, 5);
            }

            // Format the input as mm:ss (if it's long enough).
            if (cleanedInput.Length >= 2)
            {
                cleanedInput = cleanedInput.Substring(0, 2) + ":" + cleanedInput.Substring(2);
            }

            // Update the InputField text with the cleaned and formatted input.
            inputField.text = cleanedInput;
        }

    }
}
