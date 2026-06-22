using System;
using UnityEngine;

namespace Compose.Map
{
    public abstract class NodeBaseModel
    {
        public string name;
        public Sprite icon;

        protected NodeBaseModel(string name, Sprite icon)
        {
            this.name = name;
            this.icon = icon;
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
