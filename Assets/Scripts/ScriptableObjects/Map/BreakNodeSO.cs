using UnityEngine;

namespace ScriptableObjects.Map
{
    [CreateAssetMenu(fileName = "NewBreakNode", menuName = "ScriptableObjects/Map/Break")]
    public sealed class BreakNodeSO : NodeSO
    {
        public override NodeType GetNodeType()
        {
            return NodeType.Break;
        }
    }
}
