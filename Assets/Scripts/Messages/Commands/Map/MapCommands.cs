using Compose;
using Compose.Map;

namespace Messages.Commands.Map
{
    public sealed class ConfirmNodeCommand : ICommand
    {
        public MapNodeData node;
    }

    public sealed class EnterMapNodeCommand : ICommand
    {
        public MapNodeData node;
    }
}
