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
        public ReactiveProperty<ViewId> ActiveViewId { get; }
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
        /// <summary>
        /// Called when target view is closed
        /// </summary>
        public Subject<ViewId> OnViewClosed { get; }


        public void ChangeGameState(GameState state);

        /// <summary>
        /// Use to switch between views (deactivates all others)
        /// </summary>
        /// <param name="viewId"></param>
        public void ActivateView(ViewId viewId);
        /// <summary>
        /// Get warmup/stretching description (based on mode)
        /// </summary>
        /// <returns></returns>
        public WarmupDescription GetWarmupDescription();
        /// <summary>
        /// Gets training plan for next level
        /// </summary>
        /// <param name="currentLevelId"></param>
        /// <returns></returns>
        public TrainingPlan GetNextLevelTrainingPlan(int currentLevelId);
        /// <summary>
        /// Start counting training time
        /// </summary>
        public void StartTrainingTimer();
        /// <summary>
        /// Stop counting training time and return value
        /// </summary>
        /// <returns></returns>
        public float GetTrainingTime();
        /// <summary>
        /// Gets random Statham phrase from container
        /// </summary>
        /// <returns></returns>
        public string GetRandomStathamPhrase();
    }
}
