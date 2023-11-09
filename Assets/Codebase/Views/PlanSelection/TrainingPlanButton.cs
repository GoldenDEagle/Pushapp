using Assets.Codebase.Infrastructure.ServicesManagment;
using Assets.Codebase.Infrastructure.ServicesManagment.Localization;
using Assets.Codebase.Models.Progress.Data.TrainingPlans;
using Assets.Codebase.Utils.Values;
using TMPro;
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
        [SerializeField] private TMP_Text _levelId;
        [SerializeField] private TMP_Text _levelRequirement;
        [SerializeField] private Image _backgroundImage;

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
            var localizationService = ServiceLocator.Container.Single<ILocalizationService>();

            _plan = plan;
            _levelId.text = localizationService.LocalizeTextByKey(Constants.LevelWordKey) + " " + plan.Level.ToString();
            _levelRequirement.text = localizationService.LocalizeTextByKey(plan.LevelRequirement);
            _backgroundImage.color = Constants.LevelColors[plan.DifficultyLevel];
        }

        private void SelectPlan()
        {
            OnPlanSelected?.OnNext(_plan);
        }
    }
}
