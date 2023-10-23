using Assets.Codebase.Models.Progress.Data.TrainingPlans;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Codebase.Views.PlanSelection
{
    /// <summary>
    /// Button on plan selection view
    /// </summary>
    public class TrainingPlanButton : MonoBehaviour
    {
        [SerializeField] private Button _selectButton;

        private TrainingPlan _plan;

        public Subject<TrainingPlan> OnPlanSelected = new Subject<TrainingPlan>();

        private void OnEnable()
        {
            _selectButton.onClick.AddListener(SelectPlan);
        }

        private void OnDisable()
        {
            _selectButton.onClick.RemoveListener(SelectPlan);
        }

        public void Init(TrainingPlan plan)
        {
            _plan = plan;
        }

        private void SelectPlan()
        {
            OnPlanSelected?.OnNext(_plan);
        }
    }
}
