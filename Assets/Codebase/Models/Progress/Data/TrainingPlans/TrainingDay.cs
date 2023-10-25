using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Codebase.Models.Progress.Data.TrainingPlans
{
    [Serializable]
    public class TrainingDay
    {
        [Tooltip("Pushups count per session")]
        [SerializeField] private List<int> _pushups;
        [Tooltip("Hours to rest before next training")]
        [SerializeField] private int _restingTime;

        
        public List<int> Pushups => _pushups;
        
        public int RestingTime => _restingTime;
    }
}
