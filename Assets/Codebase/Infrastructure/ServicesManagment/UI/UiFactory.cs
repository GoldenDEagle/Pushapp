using Assets.Codebase.Infrastructure.ServicesManagment.Assets;
using Assets.Codebase.Models.Progress.Data.TrainingPlans;
using Assets.Codebase.Views.PlanPreview;
using Assets.Codebase.Views.PlanSelection;

namespace Assets.Codebase.Infrastructure.ServicesManagment.UI
{
    public class UiFactory : IUiFactory
    {
        private const string TrainingPlanButtonPath = "UIelements/TrainingPlanButton";
        private const string TrainingDayWidgetPath = "UIelements/TrainingDayWidget";

        private IAssetProvider _assetProvider;

        public TrainingDayWidget CreateTrainingDayWidget(TrainingDay trainingDay)
        {
            var element = _assetProvider.Instantiate(TrainingDayWidgetPath).GetComponent<TrainingDayWidget>();
            element.Init(trainingDay);
            return element;
        }

        public TrainingPlanButton CreateTrainingPlanButton(TrainingPlan plan)
        {
            var element = _assetProvider.Instantiate(TrainingPlanButtonPath).GetComponent<TrainingPlanButton>();
            element.Init(plan);
            return element;
        }
    }
}
