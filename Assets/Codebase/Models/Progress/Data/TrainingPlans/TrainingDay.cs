using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Codebase.Models.Progress.Data.TrainingPlans
{
    [Serializable]
    public class TrainingDay
    {
        [Tooltip("Pushups count per session")]
        public List<int> Pushups;
        [Tooltip("Hours to rest before next training")]
        public int RestingTime;
    }
}
