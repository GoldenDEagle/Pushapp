using Assets.Codebase.Presenter.Base;
using Assets.Codebase.Presenters.MainMenu;
using Assets.Codebase.Utils.Extensions;
using Assets.Codebase.Views.Base;
using Assets.Codebase.Views.Common;
using Cysharp.Threading.Tasks;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Codebase.Views.MainMenu
{
    public class MainMenuView : BaseView
    {
        [SerializeField] private BottomPanel _bottomPanel;
        [Header("Buttons")]
        [SerializeField] private Button _startTrainingButton;
        [SerializeField] private Button _showAchievementsButton;
        [SerializeField] private Button _changePlanButton;
        [Header("Info elements")]
        [SerializeField] private TMP_Text _totalTrainingsText;
        [SerializeField] private TMP_Text _currentLevelText;
        [SerializeField] private TMP_Text _totalPushupsText;
        [SerializeField] private TMP_Text _nextPushupsTargetText;
        [SerializeField] private Slider _pushupsProgressSlider;
        [Header("Next training info")]
        [SerializeField] private TMP_Text _nextTrainingDate;
        [SerializeField] private TMP_Text _nextTrainingLevel;
        [SerializeField] private TMP_Text _nextTrainingName;
        [SerializeField] private TMP_Text _nextTrainingPushupsList;


        private IMainMenuPresenter _presenter;

        public override void Init(IPresenter presenter)
        {
            _presenter = presenter as IMainMenuPresenter;

            base.Init(presenter);
        }

        protected override void SubscribeToUserInput()
        {
            _bottomPanel.SettingsButton.OnClickAsObservable().Subscribe(_ => _presenter.GoToSettings()).AddTo(CompositeDisposable);
            _bottomPanel.StatisticsButton.OnClickAsObservable().Subscribe(_ => _presenter.GoToStatistics()).AddTo(CompositeDisposable);

            _startTrainingButton.OnClickAsObservable().Subscribe(_ => _presenter.StartTraining()).AddTo(CompositeDisposable);
            _showAchievementsButton.OnClickAsObservable().Subscribe(_ => _presenter.ShowAchievements()).AddTo(CompositeDisposable);
            _changePlanButton.OnClickAsObservable().Subscribe(_ => _presenter.ChangePlan()).AddTo(CompositeDisposable);
        }

        protected override void SubscribeToPresenterEvents()
        {
            base.SubscribeToPresenterEvents();

            // Window configuration ...

            _presenter.TotalTrainingsText.SubscribeToTMPText(_totalTrainingsText).AddTo(CompositeDisposable);
            _presenter.CurrentLevelText.SubscribeToTMPText(_currentLevelText).AddTo(CompositeDisposable);
            _presenter.TotalPushupsText.SubscribeToTMPText(_totalPushupsText).AddTo(CompositeDisposable);
            _presenter.NextPushupsTargetText.SubscribeToTMPText(_nextPushupsTargetText).AddTo(CompositeDisposable);
            _presenter.PushupsSliderValue.Subscribe(value => _pushupsProgressSlider.value = value).AddTo(CompositeDisposable);
            _presenter.NextTrainingDateText.SubscribeToTMPText(_nextTrainingDate).AddTo(CompositeDisposable);
            _presenter.NextTrainingLevelText.SubscribeToTMPText(_nextTrainingLevel).AddTo(CompositeDisposable);
            _presenter.NextTrainingNameText.SubscribeToTMPText(_nextTrainingName).AddTo(CompositeDisposable);
            _presenter.NextTrainingPushupListText.SubscribeToTMPText(_nextTrainingPushupsList).AddTo(CompositeDisposable);
            
            // ... till here
        }
    }
}
