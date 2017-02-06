using Microsoft.AspNetCore.Mvc;

using RestAPI.Components.CQRS;
using RestAPI.Domain.Task.Command;

namespace RestAPI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICommandBus commandBus;

        public HomeController(ICommandBus commandBus)
        {
            this.commandBus = commandBus;
        }

        public IActionResult Index()
        {
            this.commandBus.handle(new AddTask("A", "B"));

            return Ok();
        }
    }
}
