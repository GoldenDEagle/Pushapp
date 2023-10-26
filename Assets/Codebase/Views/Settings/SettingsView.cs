using Assets.Codebase.Infrastructure.ServicesManagment;
using Assets.Codebase.Infrastructure.ServicesManagment.Progress;
using Assets.Codebase.Presenter.Base;
using Assets.Codebase.Presenters.Settings;
using Assets.Codebase.Views.Base;
using Assets.Codebase.Views.Common;
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
        [SerializeField] private Toggle _warmupToggle;
        [SerializeField] private Toggle _stretchingToggle;

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
            _warmupToggle.OnValueChangedAsObservable().Subscribe(value => _presenter.SetWarmupStatus(value)).AddTo(CompositeDisposable);
            _stretchingToggle.OnValueChangedAsObservable().Subscribe(value => _presenter.SetStretchingStatus(value)).AddTo(CompositeDisposable);
        }


        /// <summary>
        /// Fills default parameters from progress
        /// </summary>
        private void ConfigureView()
        {
            var progress = ServiceLocator.Container.Single<IProgressService>().ProgressModel.SessionProgress;

            _volumeSlider.value = progress.SoundVolume.Value;
            _warmupToggle.isOn = progress.IsWarmupEnabled.Value;
            _stretchingToggle.isOn = progress.IsStretchingEnabled.Value;
        }
    }
}
