using System;
using ScriptableObjects.Actors;

namespace Messages.Commands.Compose
{
    public class RunStartCommand : ICommand
    {
        public PlayerSO player;
    }

    public sealed class LoadSceneCommand : ICommand
    {
        public string sceneName;
        public Action onLoaded;
    }
}
