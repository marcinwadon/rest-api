using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

using RestAPI.Components.CQRS;
using RestAPI.Domain.Task.Command;

namespace RestAPI.Modules.TaskModule.Controllers
{
    [Route("task")]
    public class TaskController : Controller
    {
        private readonly ICommandBus commandBus;

        public TaskController(ICommandBus commandBus)
        {
            this.commandBus = commandBus;
        }

        [HttpGet()]
        public IActionResult Index([FromQuery] string abc)
        {
            var jsonObject = new JObject();
            jsonObject["lorem"] = "ipsum";
            jsonObject["abc"] = abc;

            commandBus.Handle(new AddTask("A", "B"));

            return Ok(jsonObject.ToString());
        }
    }
}
