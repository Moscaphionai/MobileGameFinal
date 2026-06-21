using System;
using Compose._3C.Actors;
using Messages;
using Messages.Commands.Compose;
using ScriptableObjects;
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
            
        }
        
        private void RunStart(RunStartCommand command)
        {
            
        }
        
    }
}