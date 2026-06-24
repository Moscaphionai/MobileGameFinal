using System;
using Compose.UI;
using Messages;
using Messages.Commands.Compose;
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
        private Action onSceneLoaded;

        protected override void Awake()
        {
            base.Awake();

            if (Instance == this)
                DontDestroyOnLoad(gameObject);
        }

        private void OnEnable()
        {
            CommandQueueManager.Instance.AddListener<LoadSceneCommand>(LoadScene);
        }

        private void OnDisable()
        {
            CommandQueueManager.Instance.RemoveListener<LoadSceneCommand>(LoadScene);
        }

        private void LoadScene(LoadSceneCommand command)
        {
            LoadSceneWithTransition(command.sceneName, command.onLoaded);
        }

        public void LoadSceneWithTransition(string sceneName, Action onLoaded = null)
        {
            onSceneLoaded = onLoaded;

            UIManager.Instance.FadeOutTransition(() =>
            {
                SceneManager.sceneLoaded += OnSceneLoaded;
                SceneManager.LoadScene(sceneName);
            });
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
            UIManager.Instance.FadeInTransition(() =>
            {
                onSceneLoaded?.Invoke();
                onSceneLoaded = null;
            });
        }
    }
}
