using Assets.Codebase.Presenter.Base;

namespace Assets.Codebase.Presenters.Training
{
    public interface ITrainingPresenter : IPresenter
    {
        public void BackToMenu();
        public void CompleteStep();
        public void CancelResting();
        public void ShowResults();
        public void ReduceStepValue();
        public void IncreaseStepValue();
    }
}
