using Task = ToDoAppAPI.Models.Task;

namespace ToDoAppAPI.Repositories.Tasks
{
    public interface ITaskRepository : IDisposable
    {
        IEnumerable<Task> GetTasks();
        Task? GetTaskById(Guid id);
        IEnumerable<Task> GetTasksByBoardId(Guid boardId);
        void InsertTask(Task task);
        void UpdateTask(Task task);
        void DeleteTask(Guid taskId);
        void Save();
    }
}
