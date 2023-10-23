using Assets.Codebase.Presenters.Base;
using Assets.Codebase.Views.Base;
using System;

namespace Assets.Codebase.Presenters.Warmup
{
    public class WarmUpPresenter : BasePresenter, IWarmUpPresenter
    {
        public WarmUpPresenter()
        {
            ViewId = ViewId.WarmupView;
        }

        public void BackToMenu()
        {
            GameplayModel.ActivateView(ViewId.MainView);
        }

        public void GoToNextExcercise()
        {
            throw new NotImplementedException();
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
