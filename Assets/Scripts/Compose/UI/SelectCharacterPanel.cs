using System;
using Messages;
using Messages.Commands.Compose;
using ScriptableObjects.Actors;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Compose.UI
{
    public class SelectCharacterPanel : BasePanel
    {
        [SerializeField] private Toggle laina;
        [SerializeField] private PlayerSO lainaSO;
        [SerializeField] private Button go;
        [SerializeField] private Button back;

        private void Awake()
        {
            laina.isOn = true;
            laina.interactable = false;
        }

        private void OnEnable()
        {
            go.onClick.AddListener(OnGo);
            back.onClick.AddListener(OnBack);
        }

        private void OnDisable()
        {
            go.onClick.RemoveListener(OnGo);
            back.onClick.RemoveListener(OnBack);
        }

        private void OnGo()
        {
            CommandQueueManager.Instance.Send(new RunStartCommand()
            {
                player = lainaSO
            });
            UIManager.Instance.Hide<SelectCharacterPanel>();
            UIManager.Instance.Show<MapPanel>();
            UIManager.Instance.Show<StatePanel>();
        }

        private void OnBack()
        {
            UIManager.Instance.Hide<SelectCharacterPanel>();
            UIManager.Instance.Show<MenuPanel>();
        }
    }
}