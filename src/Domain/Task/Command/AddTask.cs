using RestAPI.Components.CQRS;

namespace RestAPI.Domain.Task.Command
{
    public class AddTask : ICommandMessage
    {
        public string name { get; }
        public string description { get; }
        public string message {
            get { return "AddTask Command"; }
        }

        public AddTask(string name, string description)
        {
            this.name = name;
            this.description = description;
        }
    }
}
