using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Codebase.Views.Common
{
    public class TutorialWindow : MonoBehaviour
    {
        [SerializeField] private TMP_Text _stepNumberText;
        [SerializeField] private TMP_Text _stepDescription;
        [SerializeField] private Button _nextButton;
        [SerializeField] private TMP_Text _buttonText;

        public TMP_Text StepNumberText => _stepNumberText;
        public TMP_Text StepDescription => _stepDescription;
        public Button NextButton => _nextButton;
        public TMP_Text ButtonText => _buttonText;
    }
}
