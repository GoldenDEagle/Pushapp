using Assets.Codebase.Data.Trainings;
using Assets.Codebase.Infrastructure.ServicesManagment;
using Assets.Codebase.Infrastructure.ServicesManagment.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Codebase.Views.Statistics.Graph
{
    public class WindowGraph : MonoBehaviour
    {
        [SerializeField] private RectTransform _graphContainer;

        private IUiFactory _uiFactory;

        private void Awake()
        {
            _uiFactory = ServiceLocator.Container.Single<IUiFactory>();
        }

        private void Start()
        {
            DateTime today = DateTime.Now;
            DateTime tomorrow = DateTime.Now.AddDays(1);
            DateTime dayAfterTomorrow = DateTime.Now.AddDays(2);

            List<TrainingResult> testResults = new List<TrainingResult>()
            {
                new TrainingResult(new List<int>() {2, 3, 2 }, 7, today),
                new TrainingResult(new List<int>() { 5, 7, 4 }, 16, tomorrow),
                new TrainingResult(new List<int>() { 10, 20, 10 }, 40, dayAfterTomorrow)
            };

            ShowGraph(testResults);
        }

        private void CreateCircle(Vector2 anchoredPosition)
        {
            var circle = _uiFactory.CreateCircleOnGraph();
            RectTransform circleRect = circle.GetComponent<RectTransform>();
            circleRect.SetParent(_graphContainer, false);
            circleRect.anchoredPosition = anchoredPosition;
            circleRect.sizeDelta = new Vector2(11, 11);
            circleRect.anchorMin = new Vector2(0, 0);
            circleRect.anchorMax = new Vector2(0, 0);
        }

        public void ShowGraph(List<TrainingResult> results)
        {
            float graphHeight = _graphContainer.sizeDelta.y;
            float yMaximum = results.Max(x => x.TotalPushups);
            float xStep = 50f;
            for (int i = 0; i < results.Count; i++)
            {
                float xPosition = xStep + i * xStep;
                float yPosition = (results[i].TotalPushups / yMaximum) * graphHeight;
                CreateCircle(new Vector2(xPosition, yPosition));
            }
        }
    }
}
