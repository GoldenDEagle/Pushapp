using System;
using System.Collections.Generic;
using System.Linq;
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


        /// <summary>
        /// Gets the sum of pushups
        /// </summary>
        /// <returns></returns>
        public int GetTotalPushupCount()
        {
            return _pushups.Sum();
        }
    }
}
