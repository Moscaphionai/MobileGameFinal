using System.Collections.Generic;
using Compose.Map;
using ScriptableObjects.Map;
using UnityEngine;
using Utilities;

namespace Compose
{
    public sealed class MapManager : MonoSingleton<MapManager>
    {
        private const int ChildCount = 3;

        [SerializeField] private int layerCount = 6;
        [SerializeField] private List<NodeSO> nodePool = new();
        [SerializeField] private RewardNodeSO rewardNode;
        [SerializeField] private BreakNodeSO breakNode;
        [SerializeField] private BossNodeSO bossNode;

        public MapNodeData root;

        protected override void Awake()
        {
            base.Awake();
            GenerateMap();
        }

        public void GenerateMap()
        {
            var rewardDepth = layerCount / 2;

            root = CreateNodeTree(0, 0, rewardDepth);
        }

        private MapNodeData CreateNodeTree(int depth, int index, int rewardDepth)
        {
            var node = CreateNode(GetNodeSO(depth, rewardDepth), depth, index);

            if (depth == layerCount - 1)
                return node;

            for (var childIndex = 0; childIndex < ChildCount; childIndex++)
                node.children.Add(CreateNodeTree(depth + 1, index * ChildCount + childIndex, rewardDepth));

            return node;
        }

        private NodeSO GetNodeSO(int depth, int rewardDepth)
        {
            if (depth == layerCount - 1)
                return bossNode;

            if (depth == layerCount - 2)
                return breakNode;

            if (depth == rewardDepth)
                return rewardNode;

            return nodePool[UnityEngine.Random.Range(0, nodePool.Count)];
        }

        private MapNodeData CreateNode(NodeSO nodeSO, int depth, int index)
        {
            var node = new MapNodeData
            {
                depth = depth,
                index = index,
                type = nodeSO.GetNodeType(),
                nodeSO = nodeSO
            };

            if (nodeSO is BattleNodeSO battleNode)
                node.enemy = battleNode.enemies[UnityEngine.Random.Range(0, battleNode.enemies.Count)];

            return node;
        }

    }
}
