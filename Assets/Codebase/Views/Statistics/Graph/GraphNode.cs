using UnityEngine;

namespace Assets.Codebase.Views.Statistics.Graph
{
    public class GraphNode : MonoBehaviour
    {
        private RectTransform _rectTranform;

        public RectTransform RectTransform => _rectTranform;

        private void Awake()
        {
            _rectTranform = GetComponent<RectTransform>();
        }
    }
}