using Assets.Codebase.Data.Trainings;
using Assets.Codebase.Infrastructure.ServicesManagment;
using Assets.Codebase.Infrastructure.ServicesManagment.Assets;
using Assets.Codebase.Models.Base;
using Assets.Codebase.Models.Gameplay.Data;
using Assets.Codebase.Views.Base;
using UniRx;

namespace Assets.Codebase.Models.Gameplay
{
    public class GameplayModel : BaseModel, IGameplayModel
    {
        private const string TrainingPlansPath = "Content/TrainingPlans";

        private ReactiveProperty<GameState> _state;
        private ReactiveProperty<ViewId> _activeView;
        private TrainingPlansDescriptions _trainingPlansDescriptions;

        public ReactiveProperty<GameState> State => _state;
        public ReactiveProperty<ViewId> ActiveView => _activeView;
        public TrainingPlansDescriptions TrainingPlansDescriptions => _trainingPlansDescriptions;

        public GameplayModel()
        {
            _state = new ReactiveProperty<GameState>(GameState.None);
            _activeView = new ReactiveProperty<ViewId>(ViewId.None);
        }

        public void InitModel()
        {
            var assetProvider = ServiceLocator.Container.Single<IAssetProvider>();
            _trainingPlansDescriptions =  assetProvider.LoadResource<TrainingPlansDescriptions>(TrainingPlansPath);
        }

        public void ChangeGameState(GameState state)
        {
            State.Value = state;
        }

        public void ActivateView(ViewId viewId)
        {
            if (ActiveView.Value == viewId) { return; }

            ActiveView.Value = viewId;
        }
    }
}
