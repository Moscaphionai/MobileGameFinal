using UnityEngine;
using UnityEngine.UI;

namespace Compose.UI
{
    public class MenuPanel : BasePanel
    {
        public Button startBtn;
        public Button settingBtn;
        public Button exitBtn;

        private void OnEnable()
        {
            startBtn.onClick.AddListener(OnStart);
            settingBtn.onClick.AddListener(OnSetting);
            exitBtn.onClick.AddListener(OnExit);
        }

        private void OnDisable()
        {
            startBtn.onClick.RemoveListener(OnStart);
            settingBtn.onClick.RemoveListener(OnSetting);
            exitBtn.onClick.RemoveListener(OnExit);
        }

        private void OnStart()
        {
            UIManager.Instance.Hide<MenuPanel>();
            UIManager.Instance.Show<SelectCharacterPanel>();
        }

        private void OnSetting()
        {
        }

        private void OnExit()
        {
            Application.Quit();
        }
    }
}
