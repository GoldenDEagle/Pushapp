using Assets.Codebase.Infrastructure.ServicesManagment;
using Assets.Codebase.Infrastructure.ServicesManagment.Progress;
using Assets.Codebase.Presenter.Base;
using Assets.Codebase.Presenters.Settings;
using Assets.Codebase.Utils.Helpers;
using Assets.Codebase.Views.Base;
using Assets.Codebase.Views.Common;
using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Codebase.Views.Settings
{
    public class SettingsView : BaseView
    {
        [SerializeField] private BottomPanel _bottomPanel;
        [Header("Volume")]
        [SerializeField] private Slider _volumeSlider;
        [Header("Warmup and stretching")]
        [SerializeField] private UISwitch _warmupToggle;
        [SerializeField] private UISwitch _autoWarmupSwitchToggle;
        [SerializeField] private InputField _warmupTimeInput;
        [SerializeField] private UISwitch _stretchingToggle;
        [SerializeField] private UISwitch _autoStretchingSwitchToggle;
        [SerializeField] private InputField _stretchingTimeInput;
        [Header("Delete")]
        [SerializeField] private Button _deleteDataButton;

        private ISettingsPresenter _presenter;

        public override void Init(IPresenter presenter)
        {
            _presenter = presenter as ISettingsPresenter;

            base.Init(presenter);

            ConfigureView();
        }

        protected override void SubscribeToUserInput()
        {
            _bottomPanel.MainMenuButton.OnClickAsObservable().Subscribe(_ => _presenter.GoToMain()).AddTo(CompositeDisposable);
            _bottomPanel.StatisticsButton.OnClickAsObservable().Subscribe(_ => _presenter.GoToStatistics()).AddTo(CompositeDisposable);

            _volumeSlider.OnValueChangedAsObservable().Subscribe(value => _presenter.SetSoundVolume(value)).AddTo(CompositeDisposable);
            _warmupToggle.OnSwitched.Subscribe(value => _presenter.SetWarmupStatus(value)).AddTo(CompositeDisposable);
            _stretchingToggle.OnSwitched.Subscribe(value => _presenter.SetStretchingStatus(value)).AddTo(CompositeDisposable);
            _autoWarmupSwitchToggle.OnSwitched.Subscribe(value => _presenter.SetAutoWarmupSwitch(value)).AddTo(CompositeDisposable);
            _autoStretchingSwitchToggle.OnSwitched.Subscribe(value => _presenter.SetAutoStretchingSwitch(value)).AddTo(CompositeDisposable);
            _deleteDataButton.OnClickAsObservable().Subscribe(_ => _presenter.DeleteAllTrainingData()).AddTo(CompositeDisposable);
            _warmupTimeInput.OnEndEditAsObservable().Subscribe(value => { _presenter.ValidateTimeInput(value, _warmupTimeInput); _presenter.SetWarmupExerciseTime(_warmupTimeInput.text); }).AddTo(CompositeDisposable);
            _stretchingTimeInput.OnEndEditAsObservable().Subscribe(value => { _presenter.ValidateTimeInput(value, _stretchingTimeInput); _presenter.SetStretchingExerciseTime(_stretchingTimeInput.text); }).AddTo(CompositeDisposable);
        }


        /// <summary>
        /// Fills default parameters from progress
        /// </summary>
        private void ConfigureView()
        {
            var progress = ServiceLocator.Container.Single<IProgressService>().ProgressModel.SessionProgress;

            _volumeSlider.value = progress.SoundVolume.Value;
            _warmupToggle.SetInitialState(progress.IsWarmupEnabled.Value);
            _stretchingToggle.SetInitialState(progress.IsStretchingEnabled.Value);
            _autoWarmupSwitchToggle.SetInitialState(progress.AutoWarmupSwitchEnabled.Value);
            _autoStretchingSwitchToggle.SetInitialState(progress.AutoStretchingSwitchEnabled.Value);
            _warmupTimeInput.text = TimeConverter.TimeInMinutes(progress.WarmupExerciseTime.Value);
            _stretchingTimeInput.text = TimeConverter.TimeInMinutes(progress.StretchingExerciseTime.Value);
        }
    }
}
