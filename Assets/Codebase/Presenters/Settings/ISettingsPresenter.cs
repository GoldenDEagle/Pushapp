using Assets.Codebase.Presenter.Base;

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
    }
}