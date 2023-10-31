using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Codebase.Views.Common
{
    public class WarningWindow : MonoBehaviour
    {
        [SerializeField] private TMP_Text _warningText;
        [SerializeField] private Button _acceptButton;
        [SerializeField] private Button _declineButton;

        public Subject<bool> OnWindowClosed = new Subject<bool>();

        private void OnEnable()
        {
            _acceptButton.onClick.AddListener(AcceptButtonClicked);
            _declineButton.onClick.AddListener(DeclineButtonClicked);
        }

        private void OnDisable()
        {
            _acceptButton.onClick.RemoveAllListeners();
            _declineButton.onClick.RemoveAllListeners();
        }

        public void SetWarningText(string warningText)
        {
            _warningText.text = warningText;
        }

        private void AcceptButtonClicked()
        {
            _acceptButton.interactable = false;
            _declineButton.interactable = false;
            OnWindowClosed?.OnNext(true);
            Destroy(gameObject, 1f);
        }

        private void DeclineButtonClicked()
        {
            _acceptButton.interactable = false;
            _declineButton.interactable = false;
            OnWindowClosed?.OnNext(false);
            Destroy(gameObject, 1f);
        }
    }
}