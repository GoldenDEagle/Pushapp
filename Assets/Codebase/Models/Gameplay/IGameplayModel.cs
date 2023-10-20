using Assets.Codebase.Data.Trainings;
using Assets.Codebase.Models.Base;
using Assets.Codebase.Models.Gameplay.Data;
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
        public ReactiveProperty<ViewId> ActiveView { get; }
        public TrainingPlansDescriptions TrainingPlansDescriptions { get; }

        public void ChangeGameState(GameState state);
        public void ActivateView(ViewId viewId);
    }
}
