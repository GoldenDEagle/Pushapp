using Assets.Codebase.Presenters.Base;
using Assets.Codebase.Views.Base;

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

    }
}
