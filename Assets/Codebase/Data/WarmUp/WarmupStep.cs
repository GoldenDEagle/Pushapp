using System;
using UnityEngine;

namespace Assets.Codebase.Data.WarmUp
{
    [Serializable]
    public class WarmupStep
    {
        [SerializeField] private int _stepId;
        [SerializeField] private string _stepDescription;
        [SerializeField] private Sprite _stepGraphic;
        [SerializeField] private float _stepDurationSeconds;

        public int StepId => _stepId;
        public string StepDescription => _stepDescription;
        public float StepDurationSeconds => _stepDurationSeconds;
        public Sprite StepGraphic => _stepGraphic;
    }
}
