using Assets.Codebase.Infrastructure.ServicesManagment;
using Assets.Codebase.Infrastructure.ServicesManagment.Localization;
using Assets.Codebase.Infrastructure.ServicesManagment.UI;
using Assets.Codebase.Presenters.Base;
using Assets.Codebase.Utils.Helpers;
using Assets.Codebase.Utils.Values;
using Assets.Codebase.Views.Base;
using Assets.Codebase.Views.Common;
using Cysharp.Threading.Tasks;
using TMPro;
using UniRx;
using UnityEngine.UI;

namespace Assets.Codebase.Presenters.Settings
{
    public class SettingsPresenter : BasePresenter, ISettingsPresenter
    {
        public ReactiveProperty<string> WarmupTimeString {  get; private set; }
        public ReactiveProperty<string> StretchingTimeString { get; private set; }
        public ReactiveProperty<bool> IsAutoWarmupSwitchInteractable { get; private set; }
        public ReactiveProperty<bool> IsAutoStretchingSwitchInteractable { get; private set; }

        public SettingsPresenter()
        {
            ViewId = ViewId.SettingsView;

            WarmupTimeString = new ReactiveProperty<string>();
            StretchingTimeString = new ReactiveProperty<string>();
            IsAutoWarmupSwitchInteractable = new ReactiveProperty<bool>();
            IsAutoStretchingSwitchInteractable = new ReactiveProperty<bool>();
        }

        protected override void SubscribeToModelChanges()
        {
            base.SubscribeToModelChanges();
            ProgressModel.SessionProgress.WarmupExerciseTime.Subscribe(value => SetDisplayedWarmupTime(value)).AddTo(CompositeDisposable);
            ProgressModel.SessionProgress.StretchingExerciseTime.Subscribe(value => SetDisplayedStretchingTime(value)).AddTo(CompositeDisposable);
            ProgressModel.SessionProgress.IsWarmupEnabled.Subscribe(value => IsAutoWarmupSwitchInteractable.Value = value).AddTo(CompositeDisposable);
            ProgressModel.SessionProgress.IsStretchingEnabled.Subscribe(value => IsAutoStretchingSwitchInteractable.Value = value).AddTo(CompositeDisposable);
        }

        public override void CreateView()
        {
            base.CreateView();
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

        // From slider
        public void SetWarmupExerciseTime(float timeInSeconds)
        {
            ProgressModel.SessionProgress.WarmupExerciseTime.Value = timeInSeconds;
        }
        public void SetStretchingExerciseTime(float timeInSeconds)
        {
            ProgressModel.SessionProgress.StretchingExerciseTime.Value = timeInSeconds;
        }


        public void DeleteTrainingDataClicked()
        {
            // Show warning

            var localizationService = ServiceLocator.Container.Single<ILocalizationService>();
            var warningWindow = ServiceLocator.Container.Single<IUiFactory>().CreateWarningWindow();
            warningWindow.SetWarningText(localizationService.LocalizeTextByKey(Constants.DeleteProgressWarningKey));
            warningWindow.OnWindowClosed.Subscribe(value => OnDeleteProgressWarningClosed(value)).AddTo(CompositeDisposable);

            // If accepted -> delete
        }

        private void OnDeleteProgressWarningClosed(bool wasAccepted)
        {
            if (wasAccepted)
            {
                ProgressModel.SessionProgress.ClearResults();
                ProgressModel.SaveProgress();
            }
        }

        private void SetDisplayedWarmupTime(float timeInSeconds)
        {
            WarmupTimeString.Value = TimeConverter.TimeInMinutes(timeInSeconds);
        }
        private void SetDisplayedStretchingTime(float timeInSeconds)
        {
            StretchingTimeString.Value = TimeConverter.TimeInMinutes(timeInSeconds);
        }
    }
}
