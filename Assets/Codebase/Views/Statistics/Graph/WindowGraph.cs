﻿using Assets.Codebase.Data.Trainings;
using Assets.Codebase.Infrastructure.ServicesManagment;
using Assets.Codebase.Infrastructure.ServicesManagment.UI;
using Assets.Codebase.Utils.Helpers;
using Assets.Codebase.Views.Common;
using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Codebase.Views.Statistics.Graph
{
    public class WindowGraph : MonoBehaviour
    {
        [SerializeField] private RectTransform _graphContainer;

        private List<GraphNode> _displayedNodes;
        private List<GraphTextLabel> _displayedLabels;
        private List<GraphConnection> _displayedConnections;

        private IUiFactory _uiFactory;

        private void Awake()
        {
            _uiFactory = ServiceLocator.Container.Single<IUiFactory>();
            _displayedNodes = new List<GraphNode>();
            _displayedConnections = new List<GraphConnection>();
            _displayedLabels = new List<GraphTextLabel>();
        }

        private GraphNode CreateNode(Vector2 anchoredPosition)
        {
            var node = _uiFactory.CreateNodeOnGraph();
            RectTransform nodeRect = node.RectTransform;
            nodeRect.SetParent(_graphContainer, false);
            nodeRect.anchoredPosition = anchoredPosition;
            _displayedNodes.Add(node);

            return node;
        }

        public void ClearGraph()
        {
            foreach (var node in _displayedNodes)
            {
                Destroy(node.gameObject);
            }
            _displayedNodes.Clear();

            foreach (var connection in _displayedConnections)
            {
                Destroy(connection.gameObject);
            }
            _displayedConnections.Clear();

            foreach(var label in _displayedLabels)
            {
                Destroy(label.gameObject);
            }
            _displayedLabels.Clear();
        }

        public async UniTaskVoid ShowGraph(PeriodWithTrainingResults resultsForPeriod)
        {
            ClearGraph();
            var initialResults = resultsForPeriod.List;
            var bestResults = GetBestResults(initialResults);

            if (!bestResults.Any()) return;

            await UniTask.DelayFrame(2);
            float graphHeight = _graphContainer.sizeDelta.y;
            float yMaximum = bestResults.Max(x => x.TotalPushups);
            float xStep = 75f;

            GraphNode previousNode = null;

            for (int i = 0; i < bestResults.Count; i++)
            {
                float xPosition = 10f + i * xStep;
                float yPosition = (bestResults[i].TotalPushups / yMaximum) * graphHeight;
                var newNode = CreateNode(new Vector2(xPosition, yPosition));
                newNode.SetInfo(NumberConverter.Convert(bestResults[i].TotalPushups));

                if (previousNode != null)
                {
                    CreateNodeConnection(previousNode.RectTransform.anchoredPosition, newNode.RectTransform.anchoredPosition);
                }

                previousNode = newNode;

                var label = _uiFactory.CreateGraphTextLabel();
                label.RectTransform.SetParent(_graphContainer);
                label.RectTransform.anchoredPosition = new Vector2(xPosition - 5f, 0f);
                //string labelText = bestResults[i].Date.DateTime.ToShortDateString().Substring(0,5);
                string labelText = bestResults[i].Date.DateTime.ToString("dd.MM");
                label.SetText(labelText);
                _displayedLabels.Add(label);
            }
        }

        private void CreateNodeConnection(Vector2 nodePositionA, Vector2 nodePositionB)
        {
            var dotConnection = _uiFactory.CreateGraphConnection();
            dotConnection.transform.SetParent(_graphContainer, false);
            _displayedConnections.Add(dotConnection);
            RectTransform connectionRect = dotConnection.GetComponent<RectTransform>();
            Vector2 dir = (nodePositionB - nodePositionA).normalized;
            float distance = Vector2.Distance(nodePositionA, nodePositionB);
            connectionRect.sizeDelta = new Vector2(distance, 3f);
            connectionRect.anchoredPosition = nodePositionA + dir * distance * 0.5f;
            connectionRect.localEulerAngles = new Vector3(0, 0, GeometryHelpers.GetAngleFromVectorFloat(dir));
        }

        private List<TrainingResult> GetBestResults(List<TrainingResult> trainingResults)
        {
            // Group by date and select the result with the maximum value for each date
            var bestResults = trainingResults
                .GroupBy(result => result.Date.DateTime.Date)
                .Select(group => group.OrderByDescending(result => result.TotalPushups).First())
                .ToList();

            return bestResults;
        }
    }
}
