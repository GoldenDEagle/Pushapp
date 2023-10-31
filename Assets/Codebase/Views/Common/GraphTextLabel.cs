using System.Collections;
using TMPro;
using UnityEngine;

namespace Assets.Codebase.Views.Common
{
    [RequireComponent(typeof(RectTransform))]
    public class GraphTextLabel : MonoBehaviour
    {
        [SerializeField] private TMP_Text _text;

        private RectTransform _rectTransform;

        public RectTransform RectTransform => _rectTransform;

        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
        }

        public void SetText(string labelText)
        {
            _text.text = labelText;
        }
    }
}