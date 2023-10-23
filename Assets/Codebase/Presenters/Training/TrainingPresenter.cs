using Assets.Codebase.Presenters.Base;
using Assets.Codebase.Views.Base;
using System;

namespace Assets.Codebase.Presenters.Training
{
    public class TrainingPresenter : BasePresenter, ITrainingPresenter
    {
        public TrainingPresenter()
        {
            ViewId = ViewId.TrainingView;
        }

        public void BackToMenu()
        {
            // Show warning

            GameplayModel.ActivateView(ViewId.MainView);
        }

        public void CompleteStage()
        {
            throw new NotImplementedException();
        }

        public void ShowResults()
        {
            GameplayModel.ActivateView(ViewId.TrainingResultView);
        }

        public void SkipResting()
        {
            throw new NotImplementedException();
        }
    }
}
