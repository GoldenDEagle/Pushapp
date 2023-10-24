using Assets.Codebase.Data.WarmUp;
using Assets.Codebase.Presenters.Base;
using Assets.Codebase.Views.Base;
using System;
using UniRx;

namespace Assets.Codebase.Presenters.Warmup
{
    public class WarmUpPresenter : BasePresenter, IWarmUpPresenter
    {
        public Subject<WarmupStep> OnNewWarmupStep { get; private set; }

        private WarmupDescription _description;
        private int _stepNumber;

        public WarmUpPresenter()
        {
            ViewId = ViewId.WarmupView;

            OnNewWarmupStep = new Subject<WarmupStep>();
        }

        public override void CreateView()
        {
            _stepNumber = 0;
            _description = GameplayModel.GetWarmupDescription();

            base.CreateView();

            OnNewWarmupStep?.OnNext(_description.Steps[_stepNumber]);
        }

        public void BackToMenu()
        {
            GameplayModel.ActivateView(ViewId.MainView);
        }

        public void GoToNextExcercise()
        {
            _stepNumber++;

            if (_stepNumber >= _description.Steps.Count)
            {
                _stepNumber = 0;
                SkipWarmup();
                return;
            }

            OnNewWarmupStep?.OnNext(_description.Steps[_stepNumber]);
        }

        public void SkipWarmup()
        {
            GameplayModel.ActivateView(ViewId.TrainingView);
        }

        public void StartWarmup()
        {
            throw new NotImplementedException();
        }
    }
}
