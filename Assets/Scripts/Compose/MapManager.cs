using System;
using System.Collections.Generic;
using UnityEngine;
using Utilities;

namespace Compose
{
    public enum NodeType
    {
        Battle,
        Elite,
        Incident,
        Shop,
        Reward,
        Break,
        Boss
    }

    [Serializable]
    public class NodeConfig
    {
        public string name;
        public NodeType type;
        public Sprite icon;
    }

    [Serializable]
    public class MapLayer
    {
        public int index;
        public List<NodeConfig> nodes = new();
    }

    public sealed class MapManager : MonoSingleton<MapManager>
    {
        [SerializeField] private int layerCount = 4;
        [SerializeField] private int nodesPerLayer = 3;
        [SerializeField] private List<NodeConfig> nodePool = new();
        [SerializeField] private NodeConfig eliteNode;
        [SerializeField] private NodeConfig shopNode;
        [SerializeField] private NodeConfig rewardNode;
        [SerializeField] private NodeConfig breakNode;
        [SerializeField] private NodeConfig bossNode;

        public List<MapLayer> layers = new();

        protected override void Awake()
        {
            base.Awake();
            GenerateMap();
        }

        public void GenerateMap()
        {
            layers.Clear();

            var nodeCount = layerCount * nodesPerLayer;
            var configs = GenerateNodeConfigs(nodeCount);
            var configIndex = 0;

            for (var layerIndex = 0; layerIndex < layerCount; layerIndex++)
            {
                var layer = new MapLayer
                {
                    index = layerIndex
                };

                for (var nodeIndex = 0; nodeIndex < nodesPerLayer; nodeIndex++)
                {
                    layer.nodes.Add(configs[configIndex]);
                    configIndex++;
                }

                layers.Add(layer);
            }

            layers.Add(new MapLayer
            {
                index = layerCount,
                nodes = new List<NodeConfig>
                {
                    CopyNode(breakNode)
                }
            });

            layers.Add(new MapLayer
            {
                index = layerCount + 1,
                nodes = new List<NodeConfig>
                {
                    CopyNode(bossNode)
                }
            });
        }

        private List<NodeConfig> GenerateNodeConfigs(int count)
        {
            var configs = new List<NodeConfig>
            {
                CopyNode(eliteNode),
                CopyNode(shopNode)
            };

            while (configs.Count < count - 1)
            {
                var config = nodePool[UnityEngine.Random.Range(0, nodePool.Count)];
                configs.Add(CopyNode(config));
            }

            Shuffle(configs);

            var rewardIndex = UnityEngine.Random.Range(count / 2, count);
            configs.Insert(rewardIndex, CopyNode(rewardNode));

            return configs;
        }

        private void Shuffle(List<NodeConfig> configs)
        {
            for (var i = configs.Count - 1; i > 0; i--)
            {
                var index = UnityEngine.Random.Range(0, i + 1);
                (configs[i], configs[index]) = (configs[index], configs[i]);
            }
        }

        private NodeConfig CopyNode(NodeConfig config)
        {
            return new NodeConfig
            {
                name = config.name,
                type = config.type,
                icon = config.icon
            };
        }
    }
}
