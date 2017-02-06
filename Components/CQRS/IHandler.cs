namespace RestAPI.Components.CQRS
{
    public interface IHandler<TCommand> where TCommand : class, ICommandMessage
    {
        void handle(TCommand command);
    }
}
