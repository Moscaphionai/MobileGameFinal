using ScriptableObjects.Map;

namespace Compose.Map
{
    public sealed class BossNodeModel : BattleNodeModel
    {
        public BossNodeSO bossNodeSO;

        public BossNodeModel(MapNodeData data) : base(data)
        {
            bossNodeSO = (BossNodeSO)data.nodeSO;
        }
    }

    public sealed class BossNode : NodeBase
    {
        public void Init(MapNodeData data)
        {
            Init(new BossNodeModel(data));
        }
    }
}
