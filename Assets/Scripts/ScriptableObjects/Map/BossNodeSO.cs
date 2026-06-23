using UnityEngine;

namespace ScriptableObjects.Map
{
    [CreateAssetMenu(fileName = "NewBossNode", menuName = "ScriptableObjects/Map/Boss")]
    public sealed class BossNodeSO : BattleNodeSO
    {
        public override NodeType GetNodeType()
        {
            return NodeType.Boss;
        }
    }
}
