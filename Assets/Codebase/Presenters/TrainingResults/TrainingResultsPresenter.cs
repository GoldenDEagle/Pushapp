using Assets.Codebase.Presenters.Base;
using Assets.Codebase.Views.Base;
using System;

namespace Assets.Codebase.Presenters.TrainingResults
{
    public class TrainingResultsPresenter : BasePresenter, ITrainingResultsPresenter
    {
        public TrainingResultsPresenter()
        {
            ViewId = ViewId.TrainingResultView;
        }

        public void GoNextClicked()
        {
            throw new NotImplementedException();
        }
    }
}
