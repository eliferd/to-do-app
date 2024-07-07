using Microsoft.AspNetCore.Mvc;
using ToDoAppAPI.Database;
using ToDoAppAPI.Models;
using ToDoAppAPI.Repositories;
using ToDoAppAPI.Repositories.Statuses;

namespace ToDoAppAPI.Controllers
{
    [Route("api/statuses")]
    [ApiController]
    public class StatusController : ControllerBase
    {
        private IStatusRepository _statusRepository;

        public StatusController()
        {
            this._statusRepository = new StatusRepository(new DatabaseContext());
        }

        [HttpGet]
        public IEnumerable<Status> Index()
        {
            return this._statusRepository.GetStatuses();
        }

        [HttpGet("{id}")]
        public Status? GetStatus(uint id)
        {
            return this._statusRepository.GetStatus(id);
        }
    }
}
