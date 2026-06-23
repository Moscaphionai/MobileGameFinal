using UnityEngine;

namespace ScriptableObjects.Map
{
    [CreateAssetMenu(fileName = "NewRewardNode", menuName = "ScriptableObjects/Map/Reward")]
    public sealed class RewardNodeSO : NodeSO
    {
        public override NodeType GetNodeType()
        {
            return NodeType.Reward;
        }
    }
}
