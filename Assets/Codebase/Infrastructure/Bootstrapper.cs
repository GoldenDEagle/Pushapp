using Assets.Codebase.Infrastructure.Initialization;
using Assets.Codebase.Infrastructure.ServicesManagment;
using Assets.Codebase.Infrastructure.ServicesManagment.Gameplay;
using Assets.Codebase.Infrastructure.ServicesManagment.Progress;
using Assets.Codebase.Infrastructure.ServicesManagment.ViewCreation;
using Assets.Codebase.Utils.GOComponents;
using Assets.Codebase.Views.Base;
using UnityEngine;

namespace Assets.Codebase.Infrastructure
{
    [RequireComponent(typeof(DontDestroyOnLoadComponent))]
    public class Bootstrapper : MonoBehaviour
    {
        [SerializeField] private RectTransform _uiRoot;
        [SerializeField] private AudioSource _sfxSource;
        [SerializeField] private GameLaunchParams _launchParams;

        private void Awake()
        {
            // Create game structure
            GameStructure structure = new GameStructure(_uiRoot, _sfxSource, _launchParams);

            // Start the game
            //ServiceLocator.Container.Single<IViewCreatorService>().CreateView(ViewId.ExampleView);

            // Need views to test
            StartGame();
        }

        private void StartGame()
        {
            var progressModel = ServiceLocator.Container.Single<IProgressService>().ProgressModel.SessionProgress;
            var gameplayModel = ServiceLocator.Container.Single<IGameplayService>().GameplayModel;

            if (progressModel.IsTrainingPlanSelected.Value)
            {
                gameplayModel.ActivateView(ViewId.MainView);
            }
            else
            {
                gameplayModel.ActivateView(ViewId.PlanSelectionView);
            }
        }
    }
}

