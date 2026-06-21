using Messages;
using Messages.Commands.Compose;
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
}