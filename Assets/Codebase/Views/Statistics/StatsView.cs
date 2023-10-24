using Assets.Codebase.Presenter.Base;
using Assets.Codebase.Presenters.Statistics;
using Assets.Codebase.Views.Base;
using System;

namespace Assets.Codebase.Views.Statistics
{
    public class StatsView : BaseView
    {
        private IStatsPresenter _presenter;

        public override void Init(IPresenter presenter)
        {
            _presenter = presenter as IStatsPresenter;

            base.Init(presenter);
        }

        protected override void SubscribeToUserInput()
        {
            throw new NotImplementedException();
        }
    }
}
