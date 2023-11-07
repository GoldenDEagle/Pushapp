﻿using Assets.Codebase.Infrastructure.ServicesManagment.Assets;
using Assets.Codebase.Presenters.Base;
using Assets.Codebase.Views.Base;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Codebase.Infrastructure.ServicesManagment.ViewCreation
{
    /// <summary>
    /// Creates new views using IAssetProvider.
    /// </summary>
    public class ViewCreatorService : IViewCreatorService
    {
        // All presenters
        private List<BasePresenter> _presenters;

        // Root for all UI
        private RectTransform _uiRoot;

        // Paths to all view prefabs
        private const string ExampleViewPath = "Views/ExampleView";
        private const string LevelSelectionViewPath = "Views/LevelSelectionView";
        private const string LevelPreviewViewPath = "Views/?????";
        private const string MainViewPath = "Views/MainView";
        private const string WarmupViewPath = "Views/WarmupView";
        private const string TrainingViewPath = "Views/TrainingView";
        private const string ResultsViewPath = "Views/ResultsView";
        private const string StatsViewPath = "Views/StatsView";
        private const string SettingsViewPath = "Views/SettingsView";

        private IAssetProvider _assets;

        public ViewCreatorService(IAssetProvider assets, List<BasePresenter> presenters, RectTransform uiRoot)
        {
            _assets = assets;
            _presenters = presenters;
            _uiRoot = uiRoot;
        }

        public BaseView CreateView(ViewId viewId)
        {
            // Find target presenter
            var presenter = _presenters.FirstOrDefault(x => x.GetCorrespondingViewId() == viewId);

            if (presenter == null)
            {
                throw new System.NotImplementedException("Couldn't find corresponding presenter");
            }

            var path = string.Empty;

            switch (viewId)
            {
                case ViewId.None:
                    new System.ArgumentException(nameof(viewId));
                    break;
                case ViewId.ExampleView:
                    path = ExampleViewPath;
                    break;
                case ViewId.PlanSelectionView:
                    path = LevelSelectionViewPath;
                    break;
                case ViewId.PlanPreviewView:
                    path = LevelPreviewViewPath;
                    break;
                case ViewId.MainView:
                    path = MainViewPath;
                    break;
                case ViewId.TrainingView:
                    path = TrainingViewPath;
                    break;
                case ViewId.StatsView:
                    path = StatsViewPath;
                    break;
                case ViewId.SettingsView:
                    path = SettingsViewPath;
                    break;
                case ViewId.WarmupView:
                    path = WarmupViewPath;
                    break;
                case ViewId.TrainingResultView:
                    path = ResultsViewPath;
                    break;
                default:
                    throw new System.ArgumentException(nameof(viewId));
            }

            // Create and init view
            var view = _assets.Instantiate(path).GetComponent<BaseView>();
            view.transform.SetParent(_uiRoot, false);
            return view;
        }
    }
}
