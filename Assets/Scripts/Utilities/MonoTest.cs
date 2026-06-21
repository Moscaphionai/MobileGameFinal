using Messages;
using Messages.Commands.Battle;
using Messages.Commands.Compose;
using Messages.Events.Battle;
using ScriptableObjects.Actors;
using UnityEngine;

public class MonoTest : MonoBehaviour
{
    public PlayerSO so;

    public void RunStart()
    {
        CommandQueueManager.Instance.Send(new RunStartCommand()
        {
            player = so
        });
    }

    public void BattleStart()
    {
        CommandQueueManager.Instance.Send((new StartBattleCommand()
        {
            
        }));
    }
}