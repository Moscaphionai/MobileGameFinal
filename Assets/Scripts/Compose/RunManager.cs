using Compose.Actors;
using Messages;
using Messages.Commands.Compose;
using ScriptableObjects.Actors;
using Utilities;

namespace Compose
{
    public class RunManager : MonoSingleton<RunManager>
    {
        private PlayerSO PlayerSO;
        public PlayerData playerData;
        
        private void OnEnable()
        {
            CommandQueueManager.Instance.AddListener<RunStartCommand>(RunStart);    
        }

        private void OnDisable()
        {
            CommandQueueManager.Instance.RemoveListener<RunStartCommand>(RunStart);
        }
        
        private void RunStart(RunStartCommand command)
        {
            PlayerSO = command.player;
            playerData = new PlayerData(PlayerSO);
            MapManager.Instance.GenerateMap();
        }
        
    }
}
