using Compose.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utilities;

namespace Compose
{
    public class Dul
    {
    }

    public class MonoDul : MonoBehaviour
    {
    }

    public class ComposeManager : MonoSingleton<ComposeManager>
    {
        protected override void Awake()
        {
            base.Awake();
            DontDestroyOnLoad(gameObject);
        }

        public void LoadSceneWithTransition(string sceneName)
        {
            UIManager.Instance.FadeOutTransition(() =>
            {
                SceneManager.sceneLoaded += OnSceneLoaded;
                SceneManager.LoadScene(sceneName);
            });
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
            UIManager.Instance.FadeInTransition();
        }
    }
}
