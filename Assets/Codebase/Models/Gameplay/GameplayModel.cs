using Assets.Codebase.Data.Trainings;
using Assets.Codebase.Data.WarmUp;
using Assets.Codebase.Infrastructure.ServicesManagment;
using Assets.Codebase.Infrastructure.ServicesManagment.Assets;
using Assets.Codebase.Models.Base;
using Assets.Codebase.Models.Gameplay.Data;
using Assets.Codebase.Models.Progress.Data.TrainingPlans;
using Assets.Codebase.Views.Base;
using System.Linq;
using UniRx;
using UnityEngine;

namespace Assets.Codebase.Models.Gameplay
{
    public class GameplayModel : BaseModel, IGameplayModel
    {
        private const string TrainingPlansPath = "Content/TrainingPlans";
        private const string WarmupDescriptionPath = "Content/Warmups/WarmUp";
        private const string StretchinDescriptionPath = "Content/Warmups/Stretching";

        // Internal
        private ReactiveProperty<GameState> _state;
        private ReactiveProperty<ViewId> _activeView;
        private ReactiveProperty<TrainingPlan> _previewedPlan;
        private ReactiveProperty<bool> _stretchingEnabled;
        private ReactiveProperty<WarmupMode> _warmupMode;


        // Public properties
        public ReactiveProperty<GameState> State => _state;
        public ReactiveProperty<ViewId> ActiveView => _activeView;
        public ReactiveProperty<TrainingPlan> PreviewedPlan => _previewedPlan;
        public ReactiveProperty<bool> StretchingEnabled => _stretchingEnabled;
        public ReactiveProperty<WarmupMode> CurrentWarmupMode => _warmupMode;


        // From asset base
        private TrainingPlansDescriptions _trainingPlansDescriptions;
        private WarmupDescription _warmupDescription;
        private WarmupDescription _stretchingDescription;
        public TrainingPlansDescriptions TrainingPlansDescriptions => _trainingPlansDescriptions;


        public GameplayModel()
        {
            _state = new ReactiveProperty<GameState>(GameState.None);
            _activeView = new ReactiveProperty<ViewId>(ViewId.None);
            _previewedPlan = new ReactiveProperty<TrainingPlan>();
            _stretchingEnabled = new ReactiveProperty<bool>(true);
            _warmupMode = new ReactiveProperty<WarmupMode>(WarmupMode.Warmup);
        }

        /// <summary>
        /// Load everything needed from resources
        /// </summary>
        public void InitModel()
        {
            var assetProvider = ServiceLocator.Container.Single<IAssetProvider>();
            _trainingPlansDescriptions =  assetProvider.LoadResource<TrainingPlansDescriptions>(TrainingPlansPath);
            _warmupDescription = assetProvider.LoadResource<WarmupDescription>(WarmupDescriptionPath);
            _stretchingDescription = assetProvider.LoadResource<WarmupDescription>(StretchinDescriptionPath);
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

        public WarmupDescription GetWarmupDescription()
        {
            switch (_warmupMode.Value)
            {
                case WarmupMode.Warmup:
                    return _warmupDescription;
                case WarmupMode.Stretching:
                    return _stretchingDescription;
                default:
                    return null;
            }
        }

        public TrainingPlan GetNextLevelTrainingPlan(int currentLevelId)
        {
            if (currentLevelId >= _trainingPlansDescriptions.TrainingPlans.Count)
            {
                Debug.Log("You reached max level!");
                return _trainingPlansDescriptions.TrainingPlans.FirstOrDefault(x => x.Level == currentLevelId);
            }

            return _trainingPlansDescriptions.TrainingPlans.FirstOrDefault(x => x.Level == currentLevelId + 1);
        }
    }
}
