using UnityEngine;

namespace ScriptableObjects.Map
{
    public enum NodeType
    {
        Battle,
        Incident,
        Reward,
        Break,
        Boss
    }

    public abstract class NodeSO : ScriptableObject
    {
        public string nodeName;
        public Sprite icon;

        public abstract NodeType GetNodeType();
    }
}
