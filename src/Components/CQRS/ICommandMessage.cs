namespace RestAPI.Components.CQRS
{
    public interface ICommandMessage
    {
         string message { get; }
         System.Type GetType();
    }
}
