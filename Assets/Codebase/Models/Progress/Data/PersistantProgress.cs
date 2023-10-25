﻿using Assets.Codebase.Data.Trainings;
using Assets.Codebase.Models.Progress.Data.TrainingPlans;
using System;
using System.Collections.Generic;

namespace Assets.Codebase.Models.Progress.Data
{
    /// <summary>
    /// Representation of ReactiveProgress that can be serialized and saved.
    /// </summary>
    [Serializable]
    public class PersistantProgress
    {
        // All the same properties as ReactiveProgress, but Serializable
        public int SampleValue;
        public float SoundVolume;
        public bool IsTrainingPlanSelected;
        public TrainingPlan CurrentTrainingPlan;
        public TrainingDay CurrentTrainingDay;
        public int TotalPushups;

        public List<TrainingResult> AllResults;

        public void SetValues(SessionProgress progress)
        {
            // Fill all properties
            SampleValue = progress.SampleValue.Value;
            SoundVolume = progress.SoundVolume.Value;
            IsTrainingPlanSelected = progress.IsTrainingPlanSelected.Value;
            CurrentTrainingDay = progress.CurrentTrainingDay.Value;
            CurrentTrainingPlan = progress.CurrentTrainingPlan.Value;
            TotalPushups = progress.TotalPushups.Value;
            AllResults = progress.AllResults;
        }
    }
}
