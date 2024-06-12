using ToDoAppAPI.Models;

namespace ToDoAppAPI.Repositories.Boards
{
    public interface IBoardRepository : IDisposable
    {
        IEnumerable<Board> GetBoards();
        Board? GetBoardById(Guid boardId);
        IEnumerable<Board> GetBoardsByUserId(Guid userId);
        void InsertBoard(Board board);
        void UpdateBoard(Board board);
        void DeleteBoard(Guid boardId);
        void Save();
    }
}
