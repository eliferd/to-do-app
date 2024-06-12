using ToDoAppAPI.Models;

namespace ToDoAppAPI.Repositories.Statuses
{
    public interface IStatusRepository
    {
        IEnumerable<Status> GetStatuses();
        Status? GetStatus(uint statusId);
    }
}
