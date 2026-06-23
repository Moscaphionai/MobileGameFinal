using System;
using ScriptableObjects.Map;
using UnityEngine;

namespace Compose.Map
{
    public abstract class NodeBaseModel
    {
        public MapNodeData data;
        public NodeSO nodeSO;
        public string name;
        public Sprite icon;

        protected NodeBaseModel(MapNodeData data)
        {
            this.data = data;
            nodeSO = data.nodeSO;
            name = nodeSO.nodeName;
            icon = nodeSO.icon;
        }
    }

    public abstract class NodeBase : MonoDul
    {
        [SerializeField] private NodeBaseView view;

        public NodeBaseModel model;
        public Action<NodeBase> onSelected;

        protected void Init(NodeBaseModel model)
        {
            this.model = model;
            view.Init(this);
            Refresh();
        }

        public void Select()
        {
            onSelected(this);
        }

        public void Refresh()
        {
            view.Render(model);
        }
    }
}
