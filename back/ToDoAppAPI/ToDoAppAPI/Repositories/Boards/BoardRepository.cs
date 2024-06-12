using ToDoAppAPI.Database;
using ToDoAppAPI.Models;

namespace ToDoAppAPI.Repositories.Boards
{
    public class BoardRepository : IBoardRepository
    {
        private DatabaseContext _dbCtx;
        private bool disposed = false;

        public BoardRepository(DatabaseContext dbCtx)
        {
            _dbCtx = dbCtx;
        }

        public void DeleteBoard(Guid boardId)
        {
            Board? board = _dbCtx.Boards.Find(boardId);

            if (board != null)
            {
                _dbCtx.Boards.Remove(board);
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

        public Board? GetBoardById(Guid boardId)
        {
            return _dbCtx.Boards.Find(boardId);
        }

        public IEnumerable<Board> GetBoardsByUserId(Guid userId)
        {
            return _dbCtx.Boards.Where(x => x.UserId == userId).ToList();
        }

        public IEnumerable<Board> GetBoards()
        {
            return _dbCtx.Boards.ToList();
        }

        public void InsertBoard(Board board)
        {
            _dbCtx.Boards.Add(board);
        }

        public void Save()
        {
            _dbCtx.SaveChanges();
        }

        public void UpdateBoard(Board board)
        {
            _dbCtx.Entry(board).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        }
    }
}
