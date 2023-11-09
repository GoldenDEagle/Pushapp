using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Codebase.Views.Training
{
    public class RestingWidget : MonoBehaviour
    {
        [SerializeField] private TMP_Text _stathamPhraseText;
        [SerializeField] private Button _cancelRestingButton;
        [SerializeField] private Button _increaseRestingTimeButton;
        [SerializeField] private Button _decreaseRestingTimeButton;
        [SerializeField] private TMP_Text _restingTimerText;
        [SerializeField] private Image _timerFill;

        public Button CancelButton => _cancelRestingButton;
        public TMP_Text RestingTimerText => _restingTimerText;
        public Button IncreaseRestingTimeButton => _increaseRestingTimeButton;
        public Button DecreaseRestingTimeButton => _decreaseRestingTimeButton;
        public Image TimerFill => _timerFill;
        public TMP_Text StathamPhraseText => _stathamPhraseText;
    }
}
