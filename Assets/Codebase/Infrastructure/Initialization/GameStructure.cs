using Assets.Codebase.Data.Achievements;
using Assets.Codebase.Infrastructure.ServicesManagment;
using Assets.Codebase.Infrastructure.ServicesManagment.Achievements;
using Assets.Codebase.Infrastructure.ServicesManagment.Ads;
using Assets.Codebase.Infrastructure.ServicesManagment.Assets;
using Assets.Codebase.Infrastructure.ServicesManagment.Audio;
using Assets.Codebase.Infrastructure.ServicesManagment.Gameplay;
using Assets.Codebase.Infrastructure.ServicesManagment.Localization;
using Assets.Codebase.Infrastructure.ServicesManagment.PresenterManagement;
using Assets.Codebase.Infrastructure.ServicesManagment.Progress;
using Assets.Codebase.Infrastructure.ServicesManagment.UI;
using Assets.Codebase.Infrastructure.ServicesManagment.ViewCreation;
using Assets.Codebase.Models.Gameplay;
using Assets.Codebase.Models.Progress;
using Assets.Codebase.Presenters.Base;
using Assets.Codebase.Presenters.Example;
using Assets.Codebase.Presenters.MainMenu;
using Assets.Codebase.Presenters.PlanPreview;
using Assets.Codebase.Presenters.PlanSelection;
using Assets.Codebase.Presenters.Settings;
using Assets.Codebase.Presenters.Statistics;
using Assets.Codebase.Presenters.Training;
using Assets.Codebase.Presenters.TrainingResults;
using Assets.Codebase.Presenters.Warmup;
using GamePush;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Codebase.Infrastructure.Initialization
{
    /// <summary>
    /// Responsible for creation of game structure.
    /// </summary>
    public class GameStructure
    {
        public static bool IsGameInitialized = false;
        public static GameLaunchParams GameLaunchParameters { get; private set; }

        // Needed from outside
        private RectTransform _uiRoot;
        private AudioSource _sfxSource;

        // Created inside
        private IProgressModel _progressModel;
        private IGameplayModel _gameplayModel;
        private List<BasePresenter> _presenters;

        public GameStructure(RectTransform uiRoot, AudioSource sfxSource = null, GameLaunchParams launchParams = null)
        {
            if (IsGameInitialized) { return; }
            IsGameInitialized = true;

            _uiRoot = uiRoot;
            _sfxSource = sfxSource;

            GameLaunchParameters = launchParams ?? new GameLaunchParams();

            ApplyPreloadParams();

            CreateMVPStructure();
            RegisterServices();
            // Fills data using asset provider
            _gameplayModel.InitModel();

            ApplyAfterLoadParams();
        }

        // MVP structure
        private void CreateMVPStructure()
        {
            CreateModels();
            CreatePresenters();
        }

        private void CreateModels()
        {
            _progressModel = new ServerProgressModel();
#if UNITY_EDITOR
            _progressModel = new LocalProgressModel();
#endif
            _gameplayModel = new GameplayModel();
        }

        private void CreatePresenters()
        {
            // Create presenter for each view
            _presenters = new List<BasePresenter>
            {
                new ExamplePresenter(),
                new MainMenuPresenter(),
                new PlanSelectionPresenter(),
                new PlanPreviewPresenter(),
                new WarmUpPresenter(),
                new TrainingPresenter(),
                new TrainingResultsPresenter(),
                new StatsPresenter(),
                new SettingsPresenter()
            };

            foreach (var presenter in _presenters)
            {
                presenter.SetupModels(_progressModel, _gameplayModel);
            }
        }


        /// <summary>
        /// Registering all game services.
        /// </summary>
        private void RegisterServices()
        {
            var services = ServiceLocator.Container;

            services.RegisterSingle<IAssetProvider>(new AssetProvider());
            services.RegisterSingle<IViewCreatorService>(new ViewCreatorService(services.Single<IAssetProvider>(), _presenters, _uiRoot));
            services.RegisterSingle<IAudioService>(new AudioService(services.Single<IAssetProvider>(), _progressModel, _sfxSource));
            services.RegisterSingle<IAdsService>(new GamePushAdService(services.Single<IAudioService>()));
            services.RegisterSingle<IGameplayService>(new GameplayService(_gameplayModel));
            services.RegisterSingle<ILocalizationService>(new GoogleSheetLocalizationService());
            services.RegisterSingle<IPresentersService>(new PresentersService(_presenters));
            services.RegisterSingle<IProgressService>(new ProgressService(_progressModel));
            services.RegisterSingle<IUiFactory>(new UiFactory(_uiRoot, services.Single<IAssetProvider>()));
            services.RegisterSingle<IAchievementsService>(new GPAchievementsService());
        }


        // Launch params handling.
        /// <summary>
        /// Before model and service initialization.
        /// </summary>
        private void ApplyPreloadParams()
        {
            if (GameLaunchParameters.ManualParamSet)
            {
                if (GameLaunchParameters.ClearPlayerPrefs)
                {
                    PlayerPrefs.DeleteAll();
                }
            }
        }

        /// <summary>
        /// After model and service initialization.
        /// </summary>
        private void ApplyAfterLoadParams()
        {
            var services = ServiceLocator.Container;

            if (GameLaunchParameters.ManualParamSet)
            {
                services.Single<ILocalizationService>().SetLanguage(GameLaunchParameters.Language);
            }
            else
            {
                services.Single<ILocalizationService>().SetLanguage(GP_Language.Current());
            }
        }
    }
}
