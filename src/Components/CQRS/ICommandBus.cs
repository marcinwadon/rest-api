namespace RestAPI.Components.CQRS
{
    public interface ICommandBus
    {
        void RegisterHandler<TCommand>(IHandler<TCommand> handler) where TCommand : class, ICommandMessage;
        void Handle(ICommandMessage command);
    }
}
