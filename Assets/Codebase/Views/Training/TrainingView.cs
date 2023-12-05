using Assets.Codebase.Presenter.Base;
using Assets.Codebase.Presenters.Training;
using Assets.Codebase.Utils.Extensions;
using Assets.Codebase.Views.Base;
using Assets.Codebase.Views.Common;
using Cysharp.Threading.Tasks;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Codebase.Views.Training
{
    public class TrainingView : BaseView
    {
        [SerializeField] private TMP_Text _trainingNameText;
        [SerializeField] private Button _backButton;
        [SerializeField] private Button _nextStepButton;
        [SerializeField] private Button _addPushupButton;
        [SerializeField] private Button _subtractPushupButton;
        [SerializeField] private TMP_Text _resultsLineText;
        [SerializeField] private TMP_Text _totalPushupsCountText;
        [SerializeField] private TMP_Text _approachNumberText;
        [SerializeField] private TMP_Text _currentStepValue;
        [SerializeField] private RestingWidget _restingWidget;
        [SerializeField] private TutorialWindow _tutorialWindow;

        private ITrainingPresenter _presenter;
        private CanvasWithHoles _canvasWithHoles;

        public override void Init(IPresenter presenter)
        {
            _presenter = presenter as ITrainingPresenter;
            _canvasWithHoles = FindObjectOfType<CanvasWithHoles>();

            base.Init(presenter);
        }

        protected override void SubscribeToUserInput()
        {
            _backButton.OnClickAsObservable().Subscribe(_ => _presenter.BackToMenu()).AddTo(CompositeDisposable);
            _nextStepButton.OnClickAsObservable().Subscribe(_ => _presenter.CompleteStep()).AddTo(CompositeDisposable);
            _addPushupButton.OnClickAsObservable().Subscribe(_ => _presenter.IncreaseStepValue()).AddTo(CompositeDisposable);
            _subtractPushupButton.OnClickAsObservable().Subscribe(_ => _presenter.ReduceStepValue()).AddTo(CompositeDisposable);

            // Resting
            _restingWidget.CancelButton.OnClickAsObservable().Subscribe(_ => _presenter.CancelResting()).AddTo(CompositeDisposable);
            _restingWidget.IncreaseRestingTimeButton.OnClickAsObservable().Subscribe(_ => _presenter.IncreaseRestingTime()).AddTo(CompositeDisposable);
            _restingWidget.DecreaseRestingTimeButton.OnClickAsObservable().Subscribe(_ => _presenter.DecreaseRestingTime()).AddTo(CompositeDisposable);

            // Tutorial
            _tutorialWindow.NextButton.OnClickAsObservable().Subscribe(_ => _presenter.GoToNextTutorialStage()).AddTo(CompositeDisposable);
        }

        protected override void SubscribeToPresenterEvents()
        {
            base.SubscribeToPresenterEvents();
            _presenter.OnShowRestingWidget.Subscribe(_ => ShowRestingWidget()).AddTo(CompositeDisposable);
            _presenter.OnHideRestingWidget.Subscribe(_ => HideRestingWidget()).AddTo(CompositeDisposable);

            _presenter.CurrentApproachString.SubscribeToTMPText(_approachNumberText).AddTo(CompositeDisposable);
            _presenter.CurrentPushupCountText.SubscribeToTMPText(_currentStepValue).AddTo(CompositeDisposable);
            _presenter.TrainingLiveResults.SubscribeToTMPText(_resultsLineText).AddTo(CompositeDisposable);
            _presenter.TrainingNameString.SubscribeToTMPText(_trainingNameText).AddTo(CompositeDisposable);
            _presenter.TotalPushupsString.SubscribeToTMPText(_totalPushupsCountText).AddTo(CompositeDisposable);

            _presenter.StathamPhrase.SubscribeToTMPText(_restingWidget.StathamPhraseText).AddTo(CompositeDisposable);
            _presenter.TimerText.SubscribeToTMPText(_restingWidget.RestingTimerText).AddTo(CompositeDisposable);
            _presenter.TimerFillValue.Subscribe(value => _restingWidget.TimerFill.fillAmount = value).AddTo(CompositeDisposable);

            // Tutorial
            _presenter.TutorialActiveState.Subscribe(value => ShowHideTutorial(value)).AddTo(CompositeDisposable);
            _presenter.CurrentTutorialStep.Subscribe(_ => ConfigureStepVisual()).AddTo(CompositeDisposable);
            _presenter.TutorialStepNumberString.SubscribeToTMPText(_tutorialWindow.StepNumberText).AddTo(CompositeDisposable);
            _presenter.TutorialDescriptionString.SubscribeToTMPText(_tutorialWindow.StepDescription).AddTo(CompositeDisposable);
            _presenter.TutorialButtonString.SubscribeToTMPText(_tutorialWindow.ButtonText).AddTo(CompositeDisposable);
        }


        // Add animation later
        private void ShowRestingWidget()
        {
            _restingWidget.gameObject.SetActive(true);
        }
        private void HideRestingWidget()
        {
            _restingWidget.gameObject.SetActive(false);
        }

        // Tutorial
        private void ShowHideTutorial(bool isShown)
        {
            _tutorialWindow.gameObject.SetActive(isShown);
        }
        private void ConfigureStepVisual()
        {
            _canvasWithHoles.ShowStep(_presenter.CurrentTutorialStep.Value);
        }
    }
}
