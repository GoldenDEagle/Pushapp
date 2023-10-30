using Assets.Codebase.Infrastructure.ServicesManagment.Assets;
using Assets.Codebase.Models.Progress.Data.TrainingPlans;
using Assets.Codebase.Views.PlanPreview;
using Assets.Codebase.Views.PlanSelection;
using Assets.Codebase.Views.Statistics.Graph;

namespace Assets.Codebase.Infrastructure.ServicesManagment.UI
{
    public class UiFactory : IUiFactory
    {
        private const string TrainingPlanButtonPath = "UIelements/TrainingPlanButton";
        private const string TrainingDayWidgetPath = "UIelements/TrainingDayWidget";
        private const string GraphCirclePath = "UIelements/CircleOnGraph";

        private IAssetProvider _assetProvider;

        public UiFactory(IAssetProvider assetProvider)
        {
            _assetProvider = assetProvider;
        }

        public GraphNode CreateNodeOnGraph()
        {
            var element = _assetProvider.Instantiate(GraphCirclePath).GetComponent<GraphNode>();
            return element;
        }

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
