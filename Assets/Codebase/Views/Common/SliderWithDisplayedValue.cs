using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Codebase.Views.Common
{
    public class SliderWithDisplayedValue : MonoBehaviour
    {
        [SerializeField] private Slider _slider;
        [SerializeField] private TMP_Text _valueText;

        public Slider Slider => _slider;
        public TMP_Text ValueText => _valueText;
    }
}