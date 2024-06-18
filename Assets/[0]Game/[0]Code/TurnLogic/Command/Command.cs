using UnityEngine.Events;

namespace Game
{
    public abstract class Command
    {
        protected CommandData _commandData;

        public Command(CommandData data)
        {
            _commandData = data;
        }

        public abstract void Execute(UnityAction onCompleted);
    }
}