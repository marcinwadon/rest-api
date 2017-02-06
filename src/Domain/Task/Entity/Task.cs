namespace RestAPI.Domain.Task.Entity
{
    public class Task
    {
        private string name;
        private string description;

        public Task(string name, string description)
        {
            this.name = name;
            this.description = description;
        }

        public string GetName()
        {
            return this.name;
        }

        public string GetDescripiton()
        {
            return this.description;
        }
    }
}
