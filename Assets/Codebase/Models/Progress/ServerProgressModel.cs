using Assets.Codebase.Data.Trainings;
using Assets.Codebase.Models.Base;
using Assets.Codebase.Models.Progress.Data;
using Assets.Codebase.Utils.Extensions;
using Assets.Codebase.Utils.Helpers;
using Assets.Codebase.Views.Statistics;
using GamePush;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Codebase.Models.Progress
{
    public class ServerProgressModel : BaseModel, IProgressModel
    {
        private const string ProgressKey = "Progress";

        public SessionProgress SessionProgress { get; private set; }

        private PersistantProgress _persistantProgress;


        public ServerProgressModel()
        {
            LoadProgress();
        }

        public void InitModel()
        {
        }

        protected bool CanFindSave()
        {
            if (GP_Player.Has(ProgressKey))
            {
                if (GP_Player.GetString(ProgressKey) == string.Empty)
                {
                    return false;
                }
                return true;
            }

            return false;
        }

        protected void CreateNewProgress()
        {
            SessionProgress = new SessionProgress();
        }

        public void SaveProgress()
        {
            // New PersistantProgress is created on first save
            if (_persistantProgress == null)
            {
                _persistantProgress = new PersistantProgress();
            }

            _persistantProgress.SetValues(SessionProgress);
            GP_Player.Set(ProgressKey, _persistantProgress.ToJson());
        }

        public void LoadProgress()
        {
            if (CanFindSave())
            {
                GetProgressFromServer();
            }
            else
            {
                CreateNewProgress();
            }
        }

        private void GetProgressFromServer()
        {
            var progress = GP_Player.GetString(ProgressKey).ToDeserealized<PersistantProgress>();
            SessionProgress = new SessionProgress(progress);
        }


        public List<TrainingResult> GetTrainingResultsForPeriod(StatsPeriod period)
        {
            var currentTime = TimeProvider.GetServerTime();
            DateTime startingTime = DateTime.MinValue;

            switch (period)
            {
                case StatsPeriod.Total:
                    startingTime = DateTime.MinValue;
                    break;
                case StatsPeriod.Week:
                    startingTime = currentTime.Subtract(TimeSpan.FromDays(7));
                    break;
                case StatsPeriod.Month:
                    startingTime = currentTime.Subtract(TimeSpan.FromDays(30));
                    break;
                default:
                    break;
            }

            List<TrainingResult> results = new List<TrainingResult>();

            if (!SessionProgress.AllResults.Any()) return results;

            foreach (var training in SessionProgress.AllResults)
            {
                if (training.Date.DateTime > startingTime)
                {
                    results.Add(training);
                }
            }

            return results;
        }
    }
}