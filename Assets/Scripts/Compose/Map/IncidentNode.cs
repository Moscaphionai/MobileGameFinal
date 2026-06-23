using ScriptableObjects.Map;

namespace Compose.Map
{
    public sealed class IncidentNodeModel : NodeBaseModel
    {
        public IncidentNodeSO incidentNodeSO;

        public IncidentNodeModel(MapNodeData data) : base(data)
        {
            incidentNodeSO = (IncidentNodeSO)data.nodeSO;
        }
    }

    public sealed class IncidentNode : NodeBase
    {
        public void Init(MapNodeData data)
        {
            Init(new IncidentNodeModel(data));
        }
    }
}
