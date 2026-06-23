using UnityEngine;

namespace ScriptableObjects.Map
{
    [CreateAssetMenu(fileName = "NewIncidentNode", menuName = "ScriptableObjects/Map/Incident")]
    public sealed class IncidentNodeSO : NodeSO
    {
        public override NodeType GetNodeType()
        {
            return NodeType.Incident;
        }
    }
}
