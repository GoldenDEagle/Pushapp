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
        //public ReactiveProperty<float> WarmupSliderValue { get; private set; }
        //public ReactiveProperty<float> StretchingSliderValue { get; private set; }


        public SettingsPresenter()
        {
            ViewId = ViewId.SettingsView;

            WarmupTimeString = new ReactiveProperty<string>();
            StretchingTimeString = new ReactiveProperty<string>();
            //WarmupSliderValue = new ReactiveProperty<float>();
            //StretchingSliderValue = new ReactiveProperty<float>();
        }

        protected override void SubscribeToModelChanges()
        {
            base.SubscribeToModelChanges();
            ProgressModel.SessionProgress.WarmupExerciseTime.Subscribe(value => SetDisplayedWarmupTime(value)).AddTo(CompositeDisposable);
            ProgressModel.SessionProgress.StretchingExerciseTime.Subscribe(value => SetDisplayedStretchingTime(value)).AddTo(CompositeDisposable);
        }

        public override void CreateView()
        {
            base.CreateView();
            //WarmupSliderValue.Value = ProgressModel.SessionProgress.WarmupExerciseTime.Value;
            //StretchingSliderValue.Value = ProgressModel.SessionProgress.StretchingExerciseTime.Value;
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

        // From string input
        //public void SetWarmupExerciseTime(string formattedTime)
        //{
        //    ProgressModel.SessionProgress.WarmupExerciseTime.Value = TimeConverter.ParseStringToFloatTime(formattedTime);
        //}
        //public void SetStretchingExerciseTime(string formattedTime)
        //{
        //    ProgressModel.SessionProgress.StretchingExerciseTime.Value = TimeConverter.ParseStringToFloatTime(formattedTime);
        //}

        public void DeleteTrainingDataClicked()
        {
            // Show warning

            var localizationService = ServiceLocator.Container.Single<ILocalizationService>();
            var warningWindow = ServiceLocator.Container.Single<IUiFactory>().CreateWarningWindow();
            warningWindow.SetWarningText(localizationService.LocalizeTextByKey(Constants.DeleteProgressWarningKey));
            warningWindow.OnWindowClosed.Subscribe(value => OnDeleteProgressWarningClosed(value)).AddTo(CompositeDisposable);

            // If accepted -> delete
        }

        //public void ValidateTimeInput(string inputText, TMP_InputField inputField)
        //{
        //    // Remove non-numeric characters from the input.
        //    string cleanedInput = string.Join("", System.Text.RegularExpressions.Regex.Split(inputText, "[^0-9]"));

        //    // Ensure the input doesn't exceed 5 characters (mm:ss).
        //    if (cleanedInput.Length > 5)
        //    {
        //        cleanedInput = cleanedInput.Substring(0, 5);
        //    }

        //    // Format the input as mm:ss (if it's long enough).
        //    if (cleanedInput.Length >= 2)
        //    {
        //        cleanedInput = cleanedInput.Substring(0, 2) + ":" + cleanedInput.Substring(2);
        //    }

        //    // If the input is shorter than 5 characters, pad with leading zeros.
        //    while (cleanedInput.Length < 5)
        //    {
        //        cleanedInput = "0" + cleanedInput;
        //    }

        //    // Update the InputField text with the cleaned and formatted input.
        //    inputField.text = cleanedInput;
        //}


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
