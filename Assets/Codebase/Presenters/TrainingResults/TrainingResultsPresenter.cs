using Assets.Codebase.Data.Trainings;
using Assets.Codebase.Presenters.Base;
using Assets.Codebase.Utils.Extensions;
using Assets.Codebase.Views.Base;
using System;
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

            ResultsString.Value = _lastTrainingResult.PushupAttempts.ToResultsString();
        }

        public void GoNextClicked()
        {
            ProgressModel.SessionProgress.PassTrainingDay();

            // Go to stretching if needed

            GameplayModel.ActivateView(ViewId.MainView);
        }
    }
}
