using ToDoAppAPI.Database;
using ToDoAppAPI.Models;

namespace ToDoAppAPI.Repositories.Statuses
{
    public class StatusRepository : IStatusRepository
    {
        private DatabaseContext _dbCtx;

        public StatusRepository(DatabaseContext dbCtx)
        {
            _dbCtx = dbCtx;
        }

        public Status? GetStatus(uint statusId)
        {
            return _dbCtx.Statuses.Find(statusId);
        }

        public IEnumerable<Status> GetStatuses()
        {
            return _dbCtx.Statuses.ToList();
        }
    }
}
