using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Codebase.Views.Common
{
    public class UISwitch : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private Image _displayedImage;
        [Header("Sprites to switch between")]
        [SerializeField] private Sprite _onSprite;
        [SerializeField] private Sprite _offSprite;

        public Subject<bool> OnSwitched = new Subject<bool>();

        private bool _isOn;

        private void OnEnable()
        {
            _button.onClick.AddListener(SwitchState);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveAllListeners();
        }

        private void SwitchState()
        {
            if (_isOn)
            {
                _isOn = false;
                _displayedImage.sprite = _offSprite;
            }
            else
            {
                _isOn = true;
                _displayedImage.sprite = _onSprite;
            }

            OnSwitched.OnNext(_isOn);
        }

        public void SetInitialState(bool isOn)
        {
            _isOn = isOn;

            if (_isOn)
            {
                _displayedImage.sprite = _onSprite;
            }
            else
            {
                _displayedImage.sprite = _offSprite;
            }
        }

        public void MakeInteractable(bool isInteractable)
        {
            _button.interactable = isInteractable;
        }
    }
}