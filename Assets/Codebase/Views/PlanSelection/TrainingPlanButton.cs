using Assets.Codebase.Models.Progress.Data.TrainingPlans;
using UniRx;
using UnityEngine;

namespace Assets.Codebase.Views.PlanSelection
{
    /// <summary>
    /// Button on plan selection view
    /// </summary>
    public class TrainingPlanButton : MonoBehaviour
    {
        private TrainingPlan _plan;

        public Subject<TrainingPlan> OnPlanSelected = new Subject<TrainingPlan>();
    }
}
