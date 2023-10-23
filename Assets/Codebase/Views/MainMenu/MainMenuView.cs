using Assets.Codebase.Presenter.Base;
using Assets.Codebase.Presenters.MainMenu;
using Assets.Codebase.Utils.Extensions;
using Assets.Codebase.Views.Base;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Codebase.Views.MainMenu
{
    public class MainMenuView : BaseView
    {
        [Header("Buttons")]
        [SerializeField] private Button _startTrainingButton;
        [SerializeField] private Button _showAchievementsButton;
        [SerializeField] private Button _changePlanButton;
        [Header("Info elements")]
        [SerializeField] private TMP_Text _totalTrainingsText;
        [SerializeField] private TMP_Text _currentLevelText;


        private IMainMenuPresenter _presenter;

        public override void Init(IPresenter presenter)
        {
            _presenter = presenter as IMainMenuPresenter;

            base.Init(presenter);
        }

        protected override void SubscribeToUserInput()
        {
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

            // ... till here
        }
    }
}
