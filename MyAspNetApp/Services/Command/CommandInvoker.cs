
namespace MyAspNetApp.Services.Command
{
    public class CommandInvoker
    {
        private readonly Stack<ICommand> _commandHistory = new Stack<ICommand>();

        public async Task HandleCommand(ICommand command)
        {
            await command.Execute();
            _commandHistory.Push(command);
        }

        public async Task UndoLastCommand()
        {
            if (_commandHistory.Count > 0)
            {
                var lastCommand = _commandHistory.Pop();
                await lastCommand.Undo();
            }
            else
            {
                throw new InvalidOperationException("No commands to undo.");
            }
        }
    }
}
