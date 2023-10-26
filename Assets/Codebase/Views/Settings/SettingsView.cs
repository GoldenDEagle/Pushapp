using Assets.Codebase.Presenter.Base;
using Assets.Codebase.Presenters.Settings;
using Assets.Codebase.Views.Base;
using Assets.Codebase.Views.Common;
using UniRx;
using UnityEngine;

namespace Assets.Codebase.Views.Settings
{
    public class SettingsView : BaseView
    {
        [SerializeField] private BottomPanel _bottomPanel;

        private ISettingsPresenter _presenter;

        public override void Init(IPresenter presenter)
        {
            _presenter = presenter as ISettingsPresenter;

            base.Init(presenter);
        }

        protected override void SubscribeToUserInput()
        {
            _bottomPanel.MainMenuButton.OnClickAsObservable().Subscribe(_ => _presenter.GoToMain()).AddTo(CompositeDisposable);
            _bottomPanel.StatisticsButton.OnClickAsObservable().Subscribe(_ => _presenter.GoToStatistics()).AddTo(CompositeDisposable);
        }
    }
}
