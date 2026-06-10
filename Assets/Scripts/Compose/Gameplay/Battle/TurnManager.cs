using System.Collections;
using MessageQueue;
using MessageQueue.Messages.Gameplay.Battle;
using UnityEngine;
using Utilities;
using Utilities.Enum.Gameplay.Battle;

namespace Compose.Gameplay.Battle
{
    public class TurnManager : MonoSingleton<TurnManager>
    {
        public TurnState CurrentTurnState { get; private set; }
        
    }
}
