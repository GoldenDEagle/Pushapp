using Assets.Codebase.Models.Progress.Data.TrainingPlans;
using Assets.Codebase.Views.Common;
using Assets.Codebase.Views.PlanPreview;
using Assets.Codebase.Views.PlanSelection;
using Assets.Codebase.Views.Statistics.Graph;

namespace Assets.Codebase.Infrastructure.ServicesManagment.UI
{
    public interface IUiFactory : IService
    {
        public TrainingPlanButton CreateTrainingPlanButton(TrainingPlan plan);
        public TrainingDayWidget CreateTrainingDayWidget(TrainingDay trainingDay);
        public GraphNode CreateNodeOnGraph();
        public WarningWindow CreateWarningWindow();
        public GraphTextLabel CreateGraphTextLabel();
    }
}
