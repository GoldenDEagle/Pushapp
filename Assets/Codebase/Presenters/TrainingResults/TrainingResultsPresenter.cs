using Assets.Codebase.Data.Trainings;
using Assets.Codebase.Data.WarmUp;
using Assets.Codebase.Presenters.Base;
using Assets.Codebase.Utils.Extensions;
using Assets.Codebase.Views.Base;
using UniRx;

namespace Assets.Codebase.Presenters.TrainingResults
{
    public class TrainingResultsPresenter : BasePresenter, ITrainingResultsPresenter
    {
        public ReactiveProperty<string> ResultsString { get; private set; }

        private TrainingResult _lastTrainingResult;

        public TrainingResultsPresenter()
        {
            ViewId = ViewId.TrainingResultView;

            ResultsString = new ReactiveProperty<string>();
        }

        public override void CreateView()
        {
            _lastTrainingResult = ProgressModel.SessionProgress.AllResults[ProgressModel.SessionProgress.AllResults.Count - 1];

            base.CreateView();

            ResultsString.Value = _lastTrainingResult.PushupAttempts.ToPushupsListString();
        }

        public void GoNextClicked()
        {
            ProgressUpdate();

            GoToNextView();
        }

        public void RepeatClicked()
        {
            GameplayModel.ActivateView(ViewId.TrainingView);
        }

        public void StretchingToggleClicked(bool toggleState)
        {
            ProgressModel.SessionProgress.IsStretchingEnabled.Value = toggleState;
        }

        public bool IsStretchingEnabled()
        {
            return ProgressModel.SessionProgress.IsStretchingEnabled.Value;
        }

        // Internal ...................................................
        private void GoToNextView()
        {
            // Go to stretching if needed
            if (ProgressModel.SessionProgress.IsStretchingEnabled.Value)
            {
                GameplayModel.CurrentWarmupMode.Value = WarmupMode.Stretching;
                GameplayModel.ActivateView(ViewId.WarmupView);
            }
            // Or straight to main
            else
            {
                GameplayModel.ActivateView(ViewId.MainView);
            }
        }

        private void ProgressUpdate()
        {
            // If it was a test
            if (ProgressModel.SessionProgress.IsOnTestingStage.Value)
            {
                if (CheckTestCompletion())
                {
                    ProgressModel.SessionProgress.PassTrainingDay();
                }
                else
                {
                    // Test failed logic if needed
                }
            }
            // If it was regular day
            else
            {
                ProgressModel.SessionProgress.PassTrainingDay();
            }

            ProgressModel.SaveProgress();
        }

        private bool CheckTestCompletion()
        {
            if (_lastTrainingResult.TotalPushups >= ProgressModel.SessionProgress.CurrentTrainingPlan.Value.TestDay.GetTotalPushupCount())
            {
                return true;
            }
            return false;
        }
    }
}
