using UnityEngine;
using UnityEngine.UI;

namespace Assets.Codebase.Views.Common
{
    public class BottomPanel : MonoBehaviour
    {
        [SerializeField] private Button _mainMenuButton;
        [SerializeField] private Button _statisticsButton;
        [SerializeField] private Button _settingsButton;

        public Button MainMenuButton => _mainMenuButton;
        public Button StatisticsButton => _statisticsButton;
        public Button SettingsButton => _settingsButton;
    }
}
