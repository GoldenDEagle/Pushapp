using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Codebase.Views.Common
{
    public class InfoWindow : MonoBehaviour
    {
        [SerializeField] private TMP_Text _infoText;
        [SerializeField] private Button _okButton;

        private void OnEnable()
        {
            _okButton.onClick.AddListener(OkButtonClicked);
        }

        private void OnDisable()
        {
            _okButton.onClick.RemoveAllListeners();
        }

        public void SetInfoText(string infoText)
        {
            _infoText.text = infoText;
        }

        private void OkButtonClicked()
        {
            _okButton.interactable = false;

            Destroy(gameObject, 0.1f);
        }
    }
}