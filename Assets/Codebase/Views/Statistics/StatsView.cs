using Assets.Codebase.Presenter.Base;
using Assets.Codebase.Presenters.Statistics;
using Assets.Codebase.Views.Base;
using Assets.Codebase.Views.Common;
using UniRx;
using UnityEngine;

namespace Assets.Codebase.Views.Statistics
{
    public class StatsView : BaseView
    {
        [SerializeField] private BottomPanel _bottomPanel;

        private IStatsPresenter _presenter;

        public override void Init(IPresenter presenter)
        {
            _presenter = presenter as IStatsPresenter;

            base.Init(presenter);
        }

        protected override void SubscribeToUserInput()
        {
            _bottomPanel.MainMenuButton.OnClickAsObservable().Subscribe(_ => _presenter.GoToMain()).AddTo(CompositeDisposable);
            _bottomPanel.SettingsButton.OnClickAsObservable().Subscribe(_ => _presenter.GoToSettings()).AddTo(CompositeDisposable);
        }
    }
}
