using Assets.Codebase.Presenters.Base;
using Assets.Codebase.Views.Base;

namespace Assets.Codebase.Presenters.PlanSelection
{
    public class PlanSelectionPresenter : BasePresenter, IPlanSelectionPresenter
    {
        public PlanSelectionPresenter()
        {
            ViewId = ViewId.PlanSelectionView;
        }

        protected override void SubscribeToModelChanges()
        {
            base.SubscribeToModelChanges();
        }
    }
}
