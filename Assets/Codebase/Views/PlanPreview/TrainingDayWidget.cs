using Assets.Codebase.Models.Progress.Data.TrainingPlans;
using UnityEngine;

namespace Assets.Codebase.Views.PlanPreview
{
    /// <summary>
    /// Training day description in plan preview
    /// </summary>
    public class TrainingDayWidget : MonoBehaviour
    {
        private TrainingDay _trainingDay;

        public void Init(TrainingDay trainingDay)
        {
            _trainingDay = trainingDay;
        }
    }
}
