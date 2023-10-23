using Assets.Codebase.Views.PlanSelection;

namespace Assets.Codebase.Infrastructure.ServicesManagment.UI
{
    public interface IUiFactory : IService
    {
        public TrainingPlanButton CreateTrainingPlanButton();
    }
}
