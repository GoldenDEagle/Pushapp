using Assets.Codebase.Data.Trainings;
using Assets.Codebase.Infrastructure.ServicesManagment;
using Assets.Codebase.Infrastructure.ServicesManagment.Achievements;
using Assets.Codebase.Infrastructure.ServicesManagment.Gameplay;
using Assets.Codebase.Models.Progress.Data.TrainingPlans;
using Assets.Codebase.Utils.CustomTypes;
using Assets.Codebase.Utils.Helpers;
using Assets.Codebase.Utils.Values;
using System.Collections.Generic;
using UniRx;

namespace Assets.Codebase.Models.Progress.Data
{
    /// <summary>
    /// Used at runtime.
    /// </summary>
    public class SessionProgress
    {
        // All the properties that need to be saved.................

        // to be deleted
        public ReactiveProperty<int> SampleValue;

        // Trainings
        public ReactiveProperty<bool> IsTrainingPlanSelected;
        public ReactiveProperty<TrainingPlan> CurrentTrainingPlan;
        public ReactiveProperty<int> CurrentTrainingDayId;
        public ReactiveProperty<int> TotalPushups;
        public ReactiveProperty<int> NextPushupTargetId;
        public ReactiveProperty<int> NextCaloriesTargetId;
        public ReactiveProperty<int> NextTrainingCountTargetId;
        public ReactiveProperty<bool> IsOnTestingStage;
        public ReactiveProperty<SerializableDateTime> NextTrainingDate;
        public List<TrainingResult> AllResults;

        // Settings
        public ReactiveProperty<float> SoundVolume;
        public ReactiveProperty<bool> IsStretchingEnabled;
        public ReactiveProperty<bool> IsWarmupEnabled;
        public ReactiveProperty<bool> AutoWarmupSwitchEnabled;
        public ReactiveProperty<bool> AutoStretchingSwitchEnabled;
        public ReactiveProperty<float> WarmupExerciseTime;
        public ReactiveProperty<float> StretchingExerciseTime;

        // .........................................................

        /// <summary>
        /// Creates new progress with default values.
        /// </summary>
        public SessionProgress()
        {
            SampleValue = new ReactiveProperty<int>(0);
            SoundVolume = new ReactiveProperty<float>(0.5f);
            IsTrainingPlanSelected = new ReactiveProperty<bool>(false);
            CurrentTrainingPlan = new ReactiveProperty<TrainingPlan>(new TrainingPlan(0));
            CurrentTrainingDayId = new ReactiveProperty<int>();
            TotalPushups = new ReactiveProperty<int>(0);
            IsOnTestingStage = new ReactiveProperty<bool>(false);
            IsStretchingEnabled = new ReactiveProperty<bool>(true);
            IsWarmupEnabled = new ReactiveProperty<bool>(true);
            NextTrainingDate = new ReactiveProperty<SerializableDateTime>(new SerializableDateTime(TimeProvider.GetServerTime()));
            AutoWarmupSwitchEnabled = new ReactiveProperty<bool>(true);
            AutoStretchingSwitchEnabled = new ReactiveProperty<bool>(true);
            NextPushupTargetId = new ReactiveProperty<int>(0);
            NextCaloriesTargetId = new ReactiveProperty<int>(0);
            NextTrainingCountTargetId = new ReactiveProperty<int>(0);
            WarmupExerciseTime = new ReactiveProperty<float>(30f);
            StretchingExerciseTime = new ReactiveProperty<float>(30f);

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
            IsStretchingEnabled = new ReactiveProperty<bool>(progress.IsStretchingEnabled);
            IsWarmupEnabled = new ReactiveProperty<bool>(progress.IsWarmupEnabled);
            NextTrainingDate = new ReactiveProperty<SerializableDateTime>(progress.NextTrainingDate);
            AutoWarmupSwitchEnabled = new ReactiveProperty<bool>(progress.AutoWarmupSwitchEnabled);
            AutoStretchingSwitchEnabled = new ReactiveProperty<bool>(progress.AutoStretchingSwitchEnabled);
            WarmupExerciseTime = new ReactiveProperty<float>(progress.WarmupExerciseTime);
            StretchingExerciseTime = new ReactiveProperty<float>(progress.StretchingExerciseTime);
            NextPushupTargetId = new ReactiveProperty<int>(progress.NextPushupTargetId);
            NextCaloriesTargetId = new ReactiveProperty<int>(progress.NextCaloriesTargetId);
            NextTrainingCountTargetId = new ReactiveProperty<int>(progress.NextTrainingCountTargetId);

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

            CheckPushupTargets();
            CheckCaloriesTargets();
        }

        /// <summary>
        /// Add new training result
        /// </summary>
        /// <param name="result"></param>
        public void AddTrainingResult(TrainingResult result)
        {
            AllResults.Add(result);
            CheckTrainingCountTargets();
        }

        /// <summary>
        /// Cleares all results
        /// </summary>
        public void ClearResults()
        {
            AllResults.Clear();
            TotalPushups.Value = 0;
            CurrentTrainingDayId.Value = 1;
            IsOnTestingStage.Value = false;
            NextPushupTargetId.Value = 0;
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
                CurrentTrainingDayId.Value = 1;
                SetNextTrainingDate();
                return;
            }

            CurrentTrainingDayId.Value++;

            // Rises test flag if all days cleared
            if (CurrentTrainingDayId.Value > CurrentTrainingPlan.Value.TrainingDays.Count)
            {
                IsOnTestingStage.Value = true;
                CurrentTrainingDayId.Value = 1;
            }

            // Set next training time based on day info
            SetNextTrainingDate();
        }


        // Internal ....................................................
        private void SetNextTrainingDate()
        {
            var currentTime = TimeProvider.GetServerTime();
            NextTrainingDate.Value = new SerializableDateTime(currentTime.AddHours(CurrentTrainingPlan.Value.TrainingDays[CurrentTrainingDayId.Value - 1].RestingTime));
        }
        private void CheckPushupTargets()
        {
            if (NextPushupTargetId.Value >= Constants.PushupTargets.Count) { return; }

            if (TotalPushups.Value >= Constants.PushupTargets[NextPushupTargetId.Value])
            {
                ServiceLocator.Container.Single<IAchievementsService>().UnlockPushupsAchievement(NextPushupTargetId.Value);

                NextPushupTargetId.Value += 1;
            }
        }
        private void CheckCaloriesTargets()
        {
            if (NextCaloriesTargetId.Value >= Constants.CaloriesTargets.Count) { return; }

            var calories = TotalPushups.Value * Constants.CaloriesPerPushup;

            if (calories >= Constants.CaloriesTargets[NextCaloriesTargetId.Value])
            {
                ServiceLocator.Container.Single<IAchievementsService>().UnlockCaloriesAchievement(NextCaloriesTargetId.Value);

                NextCaloriesTargetId.Value += 1;
            }
        }
        private void CheckTrainingCountTargets()
        {
            if (NextTrainingCountTargetId.Value >= Constants.TrainingCountTargets.Count) { return; }

            if (AllResults.Count >= Constants.TrainingCountTargets[NextTrainingCountTargetId.Value])
            {
                ServiceLocator.Container.Single<IAchievementsService>().UnlockTrainingCountAchievement(NextTrainingCountTargetId.Value);

                NextTrainingCountTargetId.Value += 1;
            }
        }
    }
}
