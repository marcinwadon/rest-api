using RestAPI.Components.CQRS;

namespace RestAPI.Domain.Task.Command.Handler
{
    public class AddTaskHandler : IHandler<AddTask>
    {
        public void handle(AddTask command)
        {
            System.Console.WriteLine("Handle AddTask command");
            System.Console.WriteLine("message: {0}", command.message);
        }
    }
}
