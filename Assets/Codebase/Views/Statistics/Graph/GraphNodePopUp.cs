using TMPro;
using UnityEngine;

namespace Assets.Codebase.Views.Statistics.Graph
{
    public class GraphNodePopUp : MonoBehaviour
    {
        [SerializeField] private TMP_Text _resultsText;

        public TMP_Text ResultsText => _resultsText;

        public void SetText(string text)
        {
            _resultsText.text = text;
        }
    }
}