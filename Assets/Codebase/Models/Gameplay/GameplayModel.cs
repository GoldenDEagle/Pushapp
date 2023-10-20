using Assets.Codebase.Models.Base;
using Assets.Codebase.Models.Gameplay.Data;
using UniRx;

namespace Assets.Codebase.Models.Gameplay
{
    public class GameplayModel : BaseModel, IGameplayModel
    {
        public ReactiveProperty<GameState> State { get; private set; }

        protected override void InitModel()
        {
            State = new ReactiveProperty<GameState>(GameState.None);
        }

        public void ChangeGameState(GameState state)
        {
            State.Value = state;
        }
    }
}
