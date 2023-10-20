using Assets.Codebase.Models.Progress.Data.TrainingPlans;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Codebase.Data.Trainings
{
    /// <summary>
    /// Hold all training plans.
    /// </summary>
    [CreateAssetMenu(menuName = "Create Training Plans", fileName = "TrainingPlans", order = 51)]
    [Serializable]
    public class TrainingPlansDescriptions : ScriptableObject
    {
        [SerializeField] private List<TrainingPlan> _trainingPlans;

        public List<TrainingPlan> TrainingPlans => _trainingPlans;
    }
}
