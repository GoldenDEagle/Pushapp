using System;
using System.Collections.Generic;

namespace Assets.Codebase.Models.Progress.Data.TrainingPlans
{
    [Serializable]
    public class TrainingPlan
    {
        public int Level;
        public List<TrainingDay> TrainingDays;
    }
}
