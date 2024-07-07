using ToDoAppAPI.Database;
using Task = ToDoAppAPI.Models.Task;

namespace ToDoAppAPI.Repositories.Tasks
{
    public class TaskRepository : ITaskRepository
    {
        private DatabaseContext _dbCtx;
        private bool disposed = false;

        public TaskRepository(DatabaseContext dbCtx)
        {
            _dbCtx = dbCtx;
        }

        public void DeleteTask(Guid taskId)
        {
            Task? task = _dbCtx.Tasks.Find(taskId);

            if (task != null)
            {
                _dbCtx.Tasks.Remove(task);
            }
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    _dbCtx.Dispose();
                }
                disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public Task? GetTaskById(Guid id)
        {
            return _dbCtx.Tasks.Find(id);
        }

        public IEnumerable<Task> GetTasks()
        {
            return _dbCtx.Tasks.ToList();
        }

        public IEnumerable<Task> GetTasksByBoardId(Guid boardId)
        {
            return _dbCtx.Tasks.Where(x => x.BoardId == boardId).ToList();
        }

        public void InsertTask(Task task)
        {
            _dbCtx.Tasks.Add(task);
        }

        public void Save()
        {
            _dbCtx.SaveChanges();
        }

        public void UpdateTask(Task task)
        {
            _dbCtx.Entry(task).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        }
    }
}
