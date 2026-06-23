using Compose.Actors;
using ScriptableObjects.Map;

namespace Compose.Map
{
    public class BattleNodeModel : NodeBaseModel
    {
        public BattleNodeSO battleNodeSO;
        public EnemyData enemy;

        public BattleNodeModel(MapNodeData data) : base(data)
        {
            battleNodeSO = (BattleNodeSO)data.nodeSO;
            enemy = new EnemyData(data.enemy.info);
        }
    }

    public sealed class BattleNode : NodeBase
    {
        public void Init(MapNodeData data)
        {
            Init(new BattleNodeModel(data));
        }
    }
}
