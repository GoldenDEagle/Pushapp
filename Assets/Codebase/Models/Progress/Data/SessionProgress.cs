using Assets.Codebase.Data.Trainings;
using Assets.Codebase.Models.Progress.Data.TrainingPlans;
using System.Collections.Generic;
using UniRx;

namespace Assets.Codebase.Models.Progress.Data
{
    /// <summary>
    /// Used at runtime.
    /// </summary>
    public class SessionProgress
    {
        // All the properties that need to be saved...

        public ReactiveProperty<int> SampleValue;
        public ReactiveProperty<float> SoundVolume;
        public ReactiveProperty<bool> IsTrainingPlanSelected;
        public ReactiveProperty<TrainingPlan> CurrentTrainingPlan;
        public ReactiveProperty<TrainingDay> CurrentTrainingDay;
        public ReactiveProperty<int> TotalPushups;

        public List<TrainingResult> AllResults;

        // ...

        /// <summary>
        /// Creates new progress with default values.
        /// </summary>
        public SessionProgress()
        {
            SampleValue = new ReactiveProperty<int>(0);
            SoundVolume = new ReactiveProperty<float>(0.5f);
            IsTrainingPlanSelected = new ReactiveProperty<bool>(false);
            CurrentTrainingPlan = new ReactiveProperty<TrainingPlan>();
            CurrentTrainingDay = new ReactiveProperty<TrainingDay>();
            TotalPushups = new ReactiveProperty<int>(0);

            AllResults = new List<TrainingResult>();
        }

        /// <summary>
        /// Creates new progress from persistant data.
        /// </summary>
        /// <param name="progress"></param> Progress to initialize from
        public SessionProgress(PersistantProgress progress)
        {
            SampleValue = new ReactiveProperty<int>(progress.SampleValue);
            SoundVolume = new ReactiveProperty<float>(progress.SoundVolume);
            IsTrainingPlanSelected = new ReactiveProperty<bool>(progress.IsTrainingPlanSelected);
            CurrentTrainingPlan = new ReactiveProperty<TrainingPlan>(progress.CurrentTrainingPlan);
            CurrentTrainingDay = new ReactiveProperty<TrainingDay>(progress.CurrentTrainingDay);
            TotalPushups = new ReactiveProperty<int>(progress.TotalPushups);

            AllResults = progress.AllResults;
        }


        // Sensible values manipulation
        public void AddPushups(int pushups)
        {
            if (pushups < 0) return;

            TotalPushups.Value += pushups;
        }

        public void AddTrainingResult(TrainingResult result)
        {
            AllResults.Add(result);
        }
    }
}
