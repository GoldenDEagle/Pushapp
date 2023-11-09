using Assets.Codebase.Data.Trainings;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Codebase.Models.Progress.Data.TrainingPlans
{
    [Serializable]
    public class TrainingPlan
    {
        [SerializeField] private int _level;
        [SerializeField] private string _levelRequirement;
        [SerializeField] private DifficultyLevel _difficultyLevel;
        [SerializeField] private List<TrainingDay> _trainingDays;
        [SerializeField] private TrainingDay _testDay;

        public int Level => _level;
        public List<TrainingDay> TrainingDays => _trainingDays;
        public TrainingDay TestDay => _testDay;
        public string LevelRequirement => _levelRequirement;
        public DifficultyLevel DifficultyLevel => _difficultyLevel;

        public TrainingPlan(int level)
        {
            _level = level;
            _trainingDays = new List<TrainingDay>();
            _testDay = new TrainingDay();
            _difficultyLevel = DifficultyLevel.Beginner;
        }
    }
}
