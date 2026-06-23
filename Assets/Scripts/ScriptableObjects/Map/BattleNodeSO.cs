using System.Collections.Generic;
using ScriptableObjects.Actors;
using UnityEngine;

namespace ScriptableObjects.Map
{
    [CreateAssetMenu(fileName = "NewBattleNode", menuName = "ScriptableObjects/Map/Battle")]
    public class BattleNodeSO : NodeSO
    {
        public List<EnemySO> enemies = new();

        public override NodeType GetNodeType()
        {
            return NodeType.Battle;
        }
    }
}
