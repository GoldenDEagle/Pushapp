using Assets.Codebase.Presenter.Base;
using Assets.Codebase.Presenters.TrainingResults;
using Assets.Codebase.Utils.Extensions;
using Assets.Codebase.Views.Base;
using Assets.Codebase.Views.Common;
using Cysharp.Threading.Tasks;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Codebase.Views.TrainingResults
{
    public class TrainingResultsView : BaseView
    {
        [Header("Header")]
        [SerializeField] private TMP_Text _headerText;
        [SerializeField] private TMP_Text _levelText;
        [SerializeField] private TMP_Text _dayName;
        [SerializeField] private TMP_Text _resultsLineText;
        [SerializeField] private TMP_Text _trainingDuration;
        [SerializeField] private TMP_Text _totalPushupsCount;
        [SerializeField] private TMP_Text _caloriesBurnt;
        [Header("Content")]
        [SerializeField] private Button _goNextButton;
        [SerializeField] private TMP_Text _nextTrainingDate;
        [SerializeField] private UISwitch _stretchingToogle;

        private ITrainingResultsPresenter _presenter;

        public override void Init(IPresenter presenter)
        {
            _presenter = presenter as ITrainingResultsPresenter;

            base.Init(presenter);

            // Set default values
            _stretchingToogle.SetInitialState(_presenter.IsStretchingEnabled());
        }

        protected override void SubscribeToUserInput()
        {
            _goNextButton.OnClickAsObservable().Subscribe(_ => _presenter.GoNextClicked()).AddTo(CompositeDisposable);
            _stretchingToogle.OnSwitched.Subscribe(value => _presenter.StretchingToggleClicked(value)).AddTo(CompositeDisposable);
        }

        protected override void SubscribeToPresenterEvents()
        {
            base.SubscribeToPresenterEvents();

            _presenter.ViewHeaderString.SubscribeToTMPText(_headerText).AddTo(CompositeDisposable);
            _presenter.LevelString.SubscribeToTMPText(_levelText).AddTo(CompositeDisposable);
            _presenter.TrainingDayString.SubscribeToTMPText(_dayName).AddTo(CompositeDisposable);
            _presenter.ResultsString.SubscribeToTMPText(_resultsLineText).AddTo(CompositeDisposable);
            _presenter.TrainingDurationString.SubscribeToTMPText(_trainingDuration).AddTo(CompositeDisposable);
            _presenter.TotalPushupsString.SubscribeToTMPText(_totalPushupsCount).AddTo(CompositeDisposable);
            _presenter.BurntCaloriesString.SubscribeToTMPText(_caloriesBurnt).AddTo(CompositeDisposable);
            _presenter.NextTrainingDateString.SubscribeToTMPText(_nextTrainingDate).AddTo(CompositeDisposable);
        }
    }
}
