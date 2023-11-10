using Assets.Codebase.Data.Trainings;
using Assets.Codebase.Presenter.Base;
using Assets.Codebase.Presenters.Statistics;
using Assets.Codebase.Utils.Extensions;
using Assets.Codebase.Views.Base;
using Assets.Codebase.Views.Common;
using Assets.Codebase.Views.Statistics.Graph;
using System.Collections.Generic;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Codebase.Views.Statistics
{
    public class StatsView : BaseView
    {
        [SerializeField] private BottomPanel _bottomPanel;
        [SerializeField] private TMP_Text _currentLevelText;
        [SerializeField] private TMP_Text _totalPushupsText;
        [SerializeField] private WindowGraph _statsGraph;
        [SerializeField] private TMP_Text _graphPeriodText;
        [SerializeField] private Button _nextGraphButton;
        [SerializeField] private Button _previousGraphButton;
        [SerializeField] private List<StatsWidget> _statsWidgets;

        private IStatsPresenter _presenter;

        public override void Init(IPresenter presenter)
        {
            _presenter = presenter as IStatsPresenter;

            base.Init(presenter);

            FillWidgets();
        }

        protected override void SubscribeToUserInput()
        {
            _bottomPanel.MainMenuButton.OnClickAsObservable().Subscribe(_ => _presenter.GoToMain()).AddTo(CompositeDisposable);
            _bottomPanel.SettingsButton.OnClickAsObservable().Subscribe(_ => _presenter.GoToSettings()).AddTo(CompositeDisposable);
            _nextGraphButton.OnClickAsObservable().Subscribe(_ => _presenter.GoToNextResultsSegment()).AddTo(CompositeDisposable);
            _previousGraphButton.OnClickAsObservable().Subscribe(_ => _presenter.GoToPreviousResultsSegment()).AddTo(CompositeDisposable);
        }

        protected override void SubscribeToPresenterEvents()
        {
            base.SubscribeToPresenterEvents();
            _presenter.GraphPeriodString.SubscribeToTMPText(_graphPeriodText).AddTo(CompositeDisposable);
            _presenter.CurrentLevelString.SubscribeToTMPText(_currentLevelText).AddTo(CompositeDisposable);
            _presenter.TotalPushupsString.SubscribeToTMPText(_totalPushupsText).AddTo(CompositeDisposable);
            _presenter.OnShowGraph.Subscribe(value => ShowStatsGraph(value)).AddTo(CompositeDisposable);
        }


        /// <summary>
        /// Fills all stats widgets
        /// </summary>
        private void FillWidgets()
        {
            foreach (var widget in _statsWidgets)
            {
                widget.SetData(_presenter.GetStatsForPeriod(widget.Period));
            }
        }
        /// <summary>
        /// Show stats graph
        /// </summary>
        /// <param name="resultsPeriod"></param>
        private void ShowStatsGraph(PeriodWithTrainingResults resultsPeriod)
        {
            if (_statsGraph == null) return;

            _statsGraph.ShowGraph(resultsPeriod).Forget();
        }
    }
}
