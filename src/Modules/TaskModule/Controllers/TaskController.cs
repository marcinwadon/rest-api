using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

using RestAPI.Components.CQRS;

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

            return Ok(jsonObject.ToString());
        }
    }
}
