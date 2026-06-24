using Compose.Actors;
using Messages;
using Messages.Commands.Battle;
using Messages.Commands.Compose;
using Messages.Commands.Map;
using ScriptableObjects.Actors;
using ScriptableObjects.Map;
using Utilities;

namespace Compose
{
    public class RunManager : MonoSingleton<RunManager>
    {
        private PlayerSO playerSO;
        public PlayerData playerData;

        private void OnEnable()
        {
            CommandQueueManager.Instance.AddListener<RunStartCommand>(RunStart);
            CommandQueueManager.Instance.AddListener<EnterMapNodeCommand>(EnterMapNode);
        }

        private void OnDisable()
        {
            CommandQueueManager.Instance.RemoveListener<RunStartCommand>(RunStart);
            CommandQueueManager.Instance.RemoveListener<EnterMapNodeCommand>(EnterMapNode);
        }

        private void RunStart(RunStartCommand command)
        {
            playerSO = command.player;
            playerData = new PlayerData(playerSO);
            MapManager.Instance.GenerateMap();
        }

        private void EnterMapNode(EnterMapNodeCommand command)
        {
            if (command.node.type != NodeType.Battle && command.node.type != NodeType.Boss)
                return;

            var enemyData = new EnemyData(command.node.enemy.info);

            CommandQueueManager.Instance.Send(new LoadSceneCommand
            {
                sceneName = "BattleRoom",
                onLoaded = () =>
                {
                    CommandQueueManager.Instance.Send(new StartBattleCommand
                    {
                        player = playerData,
                        enemy = enemyData
                    });
                }
            });
        }
    }
}
