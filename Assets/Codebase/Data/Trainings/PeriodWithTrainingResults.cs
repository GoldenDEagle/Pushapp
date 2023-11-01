using System;
using System.Collections.Generic;

namespace Assets.Codebase.Data.Trainings
{
    [Serializable]
    public class PeriodWithTrainingResults
    {
        private List<TrainingResult> _list;

        public List<TrainingResult> List => _list;

        public PeriodWithTrainingResults(List<TrainingResult> results)
        {
            _list = results;
        }
    }
}
