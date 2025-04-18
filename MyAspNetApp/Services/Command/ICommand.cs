namespace MyAspNetApp.Services.Command
{
    public interface ICommand
    {
        Task Execute();
        Task Undo();
    }
}
