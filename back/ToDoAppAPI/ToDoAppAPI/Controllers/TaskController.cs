using Microsoft.AspNetCore.Mvc;
using ToDoAppAPI.Database;
using ToDoAppAPI.Repositories.Tasks;
using Task = ToDoAppAPI.Models.Task;

namespace ToDoAppAPI.Controllers
{
    [Route("api/tasks")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private ITaskRepository _taskRepository;
        public TaskController()
        {
            this._taskRepository = new TaskRepository(new DatabaseContext());
        }

        [HttpGet("board/{boardId}")]
        public IEnumerable<Task> Index(Guid boardId)
        {
            return this._taskRepository.GetTasksByBoardId(boardId);
        }

        [HttpGet("{id}")]
        public Task? GetTask(Guid id)
        {
            return this._taskRepository.GetTaskById(id);
        }

        [HttpPost]
        public void RegisterTask([FromBody] Task task)
        {
            this._taskRepository.InsertTask(task);
            this._taskRepository.Save();
        }

        [HttpPut("{id}")]
        public void UpdateTask([FromBody] Task task, [FromRoute] Guid id) 
        {
            this._taskRepository.UpdateTask(task);
            this._taskRepository.Save();
        }

        [HttpDelete("{id}")]
        public void DeleteTask(Guid id)
        {
            this._taskRepository.DeleteTask(id);
            this._taskRepository.Save();
        }
    }
}
