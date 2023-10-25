using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Codebase.Models.Progress.Data.TrainingPlans
{
    [Serializable]
    public class TrainingPlan
    {
        [SerializeField] private int _level;
        [SerializeField] private List<TrainingDay> _trainingDays;
        [SerializeField] private int _testTreshold;

        public int Level => _level;
        public List<TrainingDay> TrainingDays => _trainingDays;
        public int TestThreshold => _testTreshold;
    }
}
