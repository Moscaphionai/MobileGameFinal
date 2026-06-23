using System;
using System.Collections.Generic;
using ScriptableObjects.Actors;
using ScriptableObjects.Map;

namespace Compose.Map
{
    [Serializable]
    public class MapNodeData
    {
        public int depth;
        public int index;
        public NodeType type;
        public NodeSO nodeSO;
        public EnemySO enemy;
        public List<MapNodeData> children = new();
    }
}
