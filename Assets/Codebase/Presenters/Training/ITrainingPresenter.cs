using Assets.Codebase.Presenter.Base;

namespace Assets.Codebase.Presenters.Training
{
    public interface ITrainingPresenter : IPresenter
    {
        public void BackToMenu();
        public void CompleteStage();
        public void SkipResting();
        public void ShowResults();
    }
}
