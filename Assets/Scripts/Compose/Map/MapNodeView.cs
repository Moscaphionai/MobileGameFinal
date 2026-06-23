using System;
using System.Collections.Generic;
using Messages;
using Messages.Commands.Map;
using UnityEngine;
using UnityEngine.UI;

namespace Compose.Map
{
    public class MapNodeView : MonoDul
    {
        [SerializeField] private Image icon;
        [SerializeField] private Button button;

        private MapNodeData data;

        public void Init(MapNodeData data)
        {
            this.data = data;
            Refresh();
        }

        private void Refresh()
        {
            icon.sprite = data.nodeSO.icon;
        }

        public void Select()
        {
            CommandQueueManager.Instance.Send(new ConfirmNodeCommand
            {
                node = data
            });
        }
    }
}