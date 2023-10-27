using Assets.Codebase.Data.Trainings;
using Assets.Codebase.Models.Progress.Data.TrainingPlans;
using Assets.Codebase.Utils.CustomTypes;
using System;
using System.Collections.Generic;
using UniRx;

namespace Assets.Codebase.Models.Progress.Data
{
    /// <summary>
    /// Representation of ReactiveProgress that can be serialized and saved.
    /// </summary>
    [Serializable]
    public class PersistantProgress
    {
        // All the same properties as ReactiveProgress, but Serializable.........

        // to be deleted
        public int SampleValue;

        // Trainings
        public bool IsTrainingPlanSelected;
        public TrainingPlan CurrentTrainingPlan;
        public int CurrentTrainingDayId;
        public int TotalPushups;
        public int NextGlobalTarget;
        public bool IsOnTestingStage;
        public SerializableDateTime NextTrainingDate;
        public List<TrainingResult> AllResults;

        // Settings
        public float SoundVolume;
        public bool IsStretchingEnabled;
        public bool IsWarmupEnabled;
        public bool AutoWarmupSwitchEnabled;
        public bool AutoStretchingSwitchEnabled;
        public float WarmupExerciseTime;
        public float StretchingExerciseTime;

        // ........................................................................

        public void SetValues(SessionProgress progress)
        {
            // Fill all properties
            SampleValue = progress.SampleValue.Value;
            SoundVolume = progress.SoundVolume.Value;
            IsTrainingPlanSelected = progress.IsTrainingPlanSelected.Value;
            CurrentTrainingDayId = progress.CurrentTrainingDayId.Value;
            CurrentTrainingPlan = progress.CurrentTrainingPlan.Value;
            TotalPushups = progress.TotalPushups.Value;
            IsOnTestingStage = progress.IsOnTestingStage.Value;
            AllResults = progress.AllResults;
            IsStretchingEnabled = progress.IsStretchingEnabled.Value;
            IsWarmupEnabled = progress.IsWarmupEnabled.Value;
            NextTrainingDate = progress.NextTrainingDate.Value;
            AutoStretchingSwitchEnabled = progress.AutoStretchingSwitchEnabled.Value;
            AutoWarmupSwitchEnabled = progress.AutoWarmupSwitchEnabled.Value;
            NextGlobalTarget = progress.NextGlobalTarget.Value;
            WarmupExerciseTime = progress.WarmupExerciseTime.Value;
            StretchingExerciseTime = progress.StretchingExerciseTime.Value;
        }
    }
}
