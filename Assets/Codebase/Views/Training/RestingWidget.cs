using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Codebase.Views.Training
{
    public class RestingWidget : MonoBehaviour
    {
        [SerializeField] private Button _cancelRestingButton;
        [SerializeField] private TMP_Text _restingTimerText;

        public Button CancelButton => _cancelRestingButton;
        public TMP_Text RestingTimerText => _restingTimerText;
    }
}
