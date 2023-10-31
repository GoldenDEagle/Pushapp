using Assets.Codebase.Data.Trainings;
using Assets.Codebase.Infrastructure.ServicesManagment;
using Assets.Codebase.Infrastructure.ServicesManagment.UI;
using Assets.Codebase.Utils.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

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
            DateTime tomorrow = DateTime.Now.AddDays(2);
            DateTime dayAfterTomorrow = DateTime.Now.AddDays(5);

            List<TrainingResult> testResults = new List<TrainingResult>()
            {
                new TrainingResult(new List<int>() {2, 3, 2 }, 7, today),
                new TrainingResult(new List<int>() { 5, 7, 4 }, 16, tomorrow),
                new TrainingResult(new List<int>() { 10, 20, 10 }, 40, dayAfterTomorrow)
            };

            ShowGraph(testResults);
        }

        private GraphNode CreateNode(Vector2 anchoredPosition)
        {
            var node = _uiFactory.CreateNodeOnGraph();
            RectTransform nodeRect = node.RectTransform;
            nodeRect.SetParent(_graphContainer, false);
            nodeRect.anchoredPosition = anchoredPosition;
            nodeRect.sizeDelta = new Vector2(11, 11);
            nodeRect.anchorMin = new Vector2(0, 0);
            nodeRect.anchorMax = new Vector2(0, 0);

            return node;
        }

        public void ShowGraph(List<TrainingResult> results)
        {
            float graphHeight = _graphContainer.sizeDelta.y;
            float yMaximum = results.Max(x => x.TotalPushups);
            float xStep = 50f;

            GraphNode previousNode = null;
            for (int i = 0; i < results.Count; i++)
            {
                float xPosition = xStep + i * xStep;
                float yPosition = (results[i].TotalPushups / yMaximum) * graphHeight;
                var newNode = CreateNode(new Vector2(xPosition, yPosition));

                if (previousNode != null)
                {
                    CreateNodeConnection(previousNode.RectTransform.anchoredPosition, newNode.RectTransform.anchoredPosition);
                }

                previousNode = newNode;

                var label = _uiFactory.CreateGraphTextLabel();
                label.RectTransform.SetParent(_graphContainer);
                label.RectTransform.anchoredPosition = new Vector2(xPosition, -30f);
                label.SetText(results[i].Date.DateTime.ToShortDateString());
            }
        }

        private void CreateNodeConnection(Vector2 nodePositionA, Vector2 nodePositionB)
        {
            GameObject dotConnection = new GameObject("dotConnection", typeof(Image));
            dotConnection.transform.SetParent(_graphContainer, false);
            dotConnection.GetComponent<Image>().color = new Color(1, 1, 1, 0.5f);
            RectTransform connectionRect = dotConnection.GetComponent<RectTransform>();
            Vector2 dir = (nodePositionB - nodePositionA).normalized;
            float distance = Vector2.Distance(nodePositionA, nodePositionB);
            connectionRect.anchorMin = new Vector2(0, 0);
            connectionRect.anchorMax = new Vector2(0, 0);
            connectionRect.sizeDelta = new Vector2(distance, 3f);
            connectionRect.anchoredPosition = nodePositionA + dir * distance * 0.5f;
            connectionRect.localEulerAngles = new Vector3(0, 0, GeometryHelpers.GetAngleFromVectorFloat(dir));
        }
    }
}
