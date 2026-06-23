using ScriptableObjects.Map;

namespace Compose.Map
{
    public sealed class RewardNodeModel : NodeBaseModel
    {
        public RewardNodeSO rewardNodeSO;

        public RewardNodeModel(MapNodeData data) : base(data)
        {
            rewardNodeSO = (RewardNodeSO)data.nodeSO;
        }
    }

    public sealed class RewardNode : NodeBase
    {
        public void Init(MapNodeData data)
        {
            Init(new RewardNodeModel(data));
        }
    }
}
