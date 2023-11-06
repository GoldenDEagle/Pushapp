using Assets.Codebase.Infrastructure.ServicesManagment.Assets;
using Assets.Codebase.Models.Progress.Data.TrainingPlans;
using Assets.Codebase.Views.Common;
using Assets.Codebase.Views.PlanPreview;
using Assets.Codebase.Views.PlanSelection;
using Assets.Codebase.Views.Statistics.Graph;
using Cysharp.Threading.Tasks.Triggers;
using UnityEngine;

namespace Assets.Codebase.Infrastructure.ServicesManagment.UI
{
    public class UiFactory : IUiFactory
    {
        private const string TrainingPlanButtonPath = "UIelements/TrainingLevelButton";
        private const string TrainingDayWidgetPath = "UIelements/TrainingDayWidget";
        private const string GraphCirclePath = "UIelements/CircleOnGraph";
        private const string WarningWindowPath = "UIelements/WarningWindow";
        private const string GraphTextLabelPath = "UIelements/GraphTextLabel";

        private IAssetProvider _assetProvider;

        private RectTransform _uiRoot;

        public UiFactory(RectTransform uiRoot, IAssetProvider assetProvider)
        {
            _assetProvider = assetProvider;
            _uiRoot = uiRoot;
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

        public WarningWindow CreateWarningWindow()
        {
            var element = _assetProvider.Instantiate(WarningWindowPath).GetComponent<WarningWindow>();
            element.transform.SetParent(_uiRoot, false);
            return element;
        }

        public GraphTextLabel CreateGraphTextLabel()
        {
            var element = _assetProvider.Instantiate(GraphTextLabelPath).GetComponent<GraphTextLabel>();
            return element;
        }
    }
}
