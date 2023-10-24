using Assets.Codebase.Data.Trainings;
using Assets.Codebase.Data.WarmUp;
using Assets.Codebase.Models.Base;
using Assets.Codebase.Models.Gameplay.Data;
using Assets.Codebase.Models.Progress.Data.TrainingPlans;
using Assets.Codebase.Views.Base;
using UniRx;

namespace Assets.Codebase.Models.Gameplay
{
    /// <summary>
    /// Model responsible for game flow.
    /// </summary>
    public interface IGameplayModel : IModel
    {
        public ReactiveProperty<GameState> State { get; }
        /// <summary>
        /// Currently active view
        /// </summary>
        public ReactiveProperty<ViewId> ActiveView { get; }
        /// <summary>
        /// Plan opened in preview mode
        /// </summary>
        public ReactiveProperty<TrainingPlan> PreviewedPlan { get; }
        /// <summary>
        /// Is stretching after training enabled?
        /// </summary>
        public ReactiveProperty<bool> StretchingEnabled { get; }
        /// <summary>
        /// Used to split warmup and stretching
        /// </summary>
        public ReactiveProperty<WarmupMode> CurrentWarmupMode { get; }
        /// <summary>
        /// Descriptions of training plans (loaded from asset base)
        /// </summary>
        public TrainingPlansDescriptions TrainingPlansDescriptions { get; }


        public void ChangeGameState(GameState state);

        /// <summary>
        /// Use to switch between views (deactivates all others)
        /// </summary>
        /// <param name="viewId"></param>
        public void ActivateView(ViewId viewId);
    }
}
