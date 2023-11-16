using Assets.Codebase.Data.Trainings;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Codebase.Views.Statistics.Graph
{
    public class GraphNode : MonoBehaviour
    {
        [SerializeField] private GraphNodePopUp _popUpBar;

        private RectTransform _rectTranform;

        public RectTransform RectTransform => _rectTranform;

        private void Awake()
        {
            _rectTranform = GetComponent<RectTransform>();
        }

        public void SetInfo(string popUpText)
        {
            _popUpBar.SetText(popUpText);
        }

        public void ShowPopUp()
        {
            _popUpBar.gameObject.SetActive(true);
        }

        public void HidePopUp()
        {
            _popUpBar.gameObject.SetActive(false);
        }
    }
}