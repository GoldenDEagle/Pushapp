using Assets.Codebase.Infrastructure.ServicesManagment.Assets;
using Assets.Codebase.Models.Progress.Data.TrainingPlans;
using Assets.Codebase.Views.PlanSelection;

namespace Assets.Codebase.Infrastructure.ServicesManagment.UI
{
    public class UiFactory : IUiFactory
    {
        private const string TrainingPlanButtonPath = "UIelements/TrainingPlanButton";

        private IAssetProvider _assetProvider;

        public TrainingPlanButton CreateTrainingPlanButton(TrainingPlan plan)
        {
            var element = _assetProvider.Instantiate(TrainingPlanButtonPath).GetComponent<TrainingPlanButton>();
            element.AttachPlan(plan);
            return element;
        }
    }
}
