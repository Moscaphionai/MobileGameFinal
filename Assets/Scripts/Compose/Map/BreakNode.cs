using ScriptableObjects.Map;

namespace Compose.Map
{
    public sealed class BreakNodeModel : NodeBaseModel
    {
        public BreakNodeSO breakNodeSO;

        public BreakNodeModel(MapNodeData data) : base(data)
        {
            breakNodeSO = (BreakNodeSO)data.nodeSO;
        }
    }

    public sealed class BreakNode : NodeBase
    {
        public void Init(MapNodeData data)
        {
            Init(new BreakNodeModel(data));
        }
    }
}
