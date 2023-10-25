using Assets.Codebase.Presenter.Base;
using Assets.Codebase.Presenters.TrainingResults;
using Assets.Codebase.Views.Base;
using System;

namespace Assets.Codebase.Views.TrainingResults
{
    public class TrainingResultsView : BaseView
    {
        private ITrainingResultsPresenter _presenter;

        public override void Init(IPresenter presenter)
        {
            _presenter = presenter as ITrainingResultsPresenter;

            base.Init(presenter);
        }

        protected override void SubscribeToUserInput()
        {
            throw new NotImplementedException();
        }
    }
}
