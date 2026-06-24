using System;
using System.Collections.Generic;
using Messages;
using Messages.Commands.Map;
using Messages.Events.Map;
using ScriptableObjects.Actors;
using ScriptableObjects.Map;
using UnityEngine;
using Utilities;

namespace Compose
{
    public class MapNodeData
    {
        public int depth;
        public int index;
        public NodeType type;
        public NodeSO nodeSO;
        public EnemySO enemy;
        public List<MapNodeData> children = new();
    }
    
    public sealed class MapManager : MonoSingleton<MapManager>
    {
        private const int ChildCount = 3;

        [SerializeField] private int layerCount = 6;
        [SerializeField] private List<NodeSO> nodePool = new();
        [SerializeField] private RewardNodeSO rewardNode;
        [SerializeField] private BreakNodeSO breakNode;
        [SerializeField] private BossNodeSO bossNode;

        private MapNodeData root;
        private MapNodeData curNode;
        public List<MapNodeData> CurNodes => curNode.children;
        
        private void OnEnable()
        {
            CommandQueueManager.Instance.AddListener<ConfirmNodeCommand>(ConfirmNode);
        }

        private void OnDisable()
        {
            CommandQueueManager.Instance.RemoveListener<ConfirmNodeCommand>(ConfirmNode);
        }

        public void GenerateMap()
        {
            var rewardDepth = layerCount / 2;

            root = CreateNodeTree(0, 0, rewardDepth);
            curNode = root;
        }

        private void ConfirmNode(ConfirmNodeCommand command)
        {
            curNode = command.node;
            EventQueueManager.Instance.Publish(new MapDataChangedEvent
            {
                curNode = curNode
            });

            CommandQueueManager.Instance.Send(new EnterMapNodeCommand
            {
                node = curNode
            });
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
