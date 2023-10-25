using Assets.Codebase.Data.Trainings;
using Assets.Codebase.Infrastructure.ServicesManagment;
using Assets.Codebase.Infrastructure.ServicesManagment.Gameplay;
using Assets.Codebase.Models.Gameplay;
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
        public ReactiveProperty<int> CurrentTrainingDayId;
        public ReactiveProperty<int> TotalPushups;
        public ReactiveProperty<bool> IsOnTestingStage;

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
            CurrentTrainingDayId = new ReactiveProperty<int>();
            TotalPushups = new ReactiveProperty<int>(0);
            IsOnTestingStage = new ReactiveProperty<bool>(false);

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
            CurrentTrainingDayId = new ReactiveProperty<int>(progress.CurrentTrainingDayId);
            TotalPushups = new ReactiveProperty<int>(progress.TotalPushups);
            IsOnTestingStage = new ReactiveProperty<bool>(progress.IsOnTestingStage);

            AllResults = progress.AllResults;
        }


        // Sensible values manipulation

        /// <summary>
        /// Add to total pushups
        /// </summary>
        /// <param name="pushups"></param>
        public void AddPushups(int pushups)
        {
            if (pushups < 0) return;

            TotalPushups.Value += pushups;
        }

        /// <summary>
        /// Add new training result
        /// </summary>
        /// <param name="result"></param>
        public void AddTrainingResult(TrainingResult result)
        {
            AllResults.Add(result);
        }

        /// <summary>
        /// Call when training day is passed
        /// </summary>
        public void PassTrainingDay()
        {
            // If test passed get next level plan
            if (IsOnTestingStage.Value)
            {
                IsOnTestingStage.Value = false;
                CurrentTrainingPlan.Value = ServiceLocator.Container.Single<IGameplayService>().GameplayModel.GetNextLevelTrainingPlan(CurrentTrainingPlan.Value.Level);
                CurrentTrainingDayId.Value = 0;
                return;
            }

            CurrentTrainingDayId.Value++;

            // Rises test flag if all days cleared
            if (CurrentTrainingDayId.Value >= CurrentTrainingPlan.Value.TrainingDays.Count)
            {
                IsOnTestingStage.Value = true;
                CurrentTrainingDayId.Value = 0;
            }
        }
    }
}
