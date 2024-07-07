using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToDoAppAPI.Database;
using ToDoAppAPI.Models;
using ToDoAppAPI.Repositories.Boards;

namespace ToDoAppAPI.Controllers
{
    [Route("api/boards")]
    [ApiController]
    public class BoardController : ControllerBase
    {
        private IBoardRepository _boardRepository;
        public BoardController() 
        {
            this._boardRepository = new BoardRepository(new DatabaseContext());
        }

        [HttpGet]
        public IEnumerable<Board> Index()
        {
            return this._boardRepository.GetBoards();
        }

        [HttpGet("{id}")]
        public Board? GetBoard(Guid id)
        {
            return this._boardRepository.GetBoardById(id);
        }

        [HttpPost]
        [Authorize]
        public void RegisterBoard([FromBody] Board board)
        {
            this._boardRepository.InsertBoard(board);
            this._boardRepository.Save();
        }

        [HttpPut("{id}")]
        [Authorize]
        public void UpdateBoard([FromBody] Board board, [FromRoute] Guid id)
        {
            this._boardRepository.UpdateBoard(board);
            this._boardRepository.Save();
        }

        [HttpDelete("{id}")]
        [Authorize]
        public void DeleteBoard(Guid id)
        {
            this._boardRepository.DeleteBoard(id);
            this._boardRepository.Save();
        }
    }
}
