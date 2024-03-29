﻿using Assets.Codebase.Data.Phrases;
using Assets.Codebase.Data.Trainings;
using Assets.Codebase.Data.WarmUp;
using Assets.Codebase.Infrastructure.ServicesManagment;
using Assets.Codebase.Infrastructure.ServicesManagment.Assets;
using Assets.Codebase.Infrastructure.ServicesManagment.Localization;
using Assets.Codebase.Models.Base;
using Assets.Codebase.Models.Gameplay.Data;
using Assets.Codebase.Models.Progress.Data.TrainingPlans;
using Assets.Codebase.Utils.Helpers;
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
        private const string StathamPhrasesPath = "Content/StathamPhrases";
        

        // Internal
        private ReactiveProperty<GameState> _state;
        private ReactiveProperty<ViewId> _activeViewId;
        private ReactiveProperty<TrainingPlan> _previewedPlan;
        private ReactiveProperty<bool> _stretchingEnabled;
        private ReactiveProperty<WarmupMode> _warmupMode;
        private Subject<ViewId> _onViewClosed;
        private Stopwatch _trainingStopwatch;


        // Public properties
        public ReactiveProperty<GameState> State => _state;
        public ReactiveProperty<ViewId> ActiveViewId => _activeViewId;
        public ReactiveProperty<TrainingPlan> PreviewedPlan => _previewedPlan;
        public ReactiveProperty<bool> StretchingEnabled => _stretchingEnabled;
        public ReactiveProperty<WarmupMode> CurrentWarmupMode => _warmupMode;
        public Subject<ViewId> OnViewClosed => _onViewClosed;


        // From asset base
        private TrainingPlansDescriptions _trainingPlansDescriptions;
        private WarmupDescription _warmupDescription;
        private WarmupDescription _stretchingDescription;
        private StathamContainer _stathamContainer;
        public TrainingPlansDescriptions TrainingPlansDescriptions => _trainingPlansDescriptions;


        public GameplayModel()
        {
            _state = new ReactiveProperty<GameState>(GameState.None);
            _activeViewId = new ReactiveProperty<ViewId>(ViewId.None);
            _previewedPlan = new ReactiveProperty<TrainingPlan>();
            _stretchingEnabled = new ReactiveProperty<bool>(true);
            _warmupMode = new ReactiveProperty<WarmupMode>(WarmupMode.Warmup);
            _onViewClosed = new Subject<ViewId>();
            _trainingStopwatch = new Stopwatch();
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
            _stathamContainer = assetProvider.LoadResource<StathamContainer>(StathamPhrasesPath);
        }

        public void ChangeGameState(GameState state)
        {
            State.Value = state;
        }

        public void ActivateView(ViewId viewId)
        {
            if (ActiveViewId.Value == viewId) { return; }

            _onViewClosed.OnNext(ActiveViewId.Value);

            ActiveViewId.Value = viewId;
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

        public void StartTrainingTimer()
        {
            _trainingStopwatch.Start();
        }

        public float GetTrainingTime()
        {
            return _trainingStopwatch.GetElapsedTime(true);
        }

        public string GetRandomStathamPhrase()
        {
            var localizationService = ServiceLocator.Container.Single<ILocalizationService>();
            int randomNumber = Random.Range(0, _stathamContainer.Phrases.Count);
            string phrase = localizationService.LocalizeTextByKey(_stathamContainer.Phrases[randomNumber]);
            return phrase;
        }
    }
}
