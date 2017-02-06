namespace RestAPI.Components.CQRS
{
    public interface IHandler<TCommand> where TCommand : class, ICommandMessage
    {
        void Handle(TCommand command);
    }
}
