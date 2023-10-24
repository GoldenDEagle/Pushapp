using Assets.Codebase.Data.Trainings;
using Assets.Codebase.Data.WarmUp;
using Assets.Codebase.Infrastructure.ServicesManagment;
using Assets.Codebase.Infrastructure.ServicesManagment.Assets;
using Assets.Codebase.Models.Base;
using Assets.Codebase.Models.Gameplay.Data;
using Assets.Codebase.Models.Progress.Data.TrainingPlans;
using Assets.Codebase.Views.Base;
using UniRx;

namespace Assets.Codebase.Models.Gameplay
{
    public class GameplayModel : BaseModel, IGameplayModel
    {
        private const string TrainingPlansPath = "Content/TrainingPlans";

        // Internal fields
        private ReactiveProperty<GameState> _state;
        private ReactiveProperty<ViewId> _activeView;
        private ReactiveProperty<TrainingPlan> _previewedPlan;
        private ReactiveProperty<bool> _stretchingEnabled;
        private ReactiveProperty<WarmupMode> _warmupMode;
        private TrainingPlansDescriptions _trainingPlansDescriptions;

        // Public properties
        public ReactiveProperty<GameState> State => _state;
        public ReactiveProperty<ViewId> ActiveView => _activeView;
        public ReactiveProperty<TrainingPlan> PreviewedPlan => _previewedPlan;
        public ReactiveProperty<bool> StretchingEnabled => _stretchingEnabled;
        public ReactiveProperty<WarmupMode> CurrentWarmupMode => _warmupMode;
        public TrainingPlansDescriptions TrainingPlansDescriptions => _trainingPlansDescriptions;

        public GameplayModel()
        {
            _state = new ReactiveProperty<GameState>(GameState.None);
            _activeView = new ReactiveProperty<ViewId>(ViewId.None);
            _previewedPlan = new ReactiveProperty<TrainingPlan>();
            _stretchingEnabled = new ReactiveProperty<bool>(true);
        }

        /// <summary>
        /// Load everything needed from resources
        /// </summary>
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
