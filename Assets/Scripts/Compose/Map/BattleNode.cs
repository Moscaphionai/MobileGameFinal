using Compose.Actors;
using ScriptableObjects.Actors;
using UnityEngine;

namespace Compose.Map
{
    public sealed class BattleNodeModel : NodeBaseModel
    {
        public EnemyData enemy;

        public BattleNodeModel(string name, Sprite icon, EnemySO enemySO) : base(name, icon)
        {
            enemy = new EnemyData(enemySO.info);
        }
    }

    public sealed class BattleNode : NodeBase
    {
        public void Init(string name, Sprite icon, EnemySO enemySO)
        {
            Init(new BattleNodeModel(name, icon, enemySO));
        }
    }
}
