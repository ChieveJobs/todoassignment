using TodoAssignment.Models;

namespace TodoAssignment.Repositories
{
    public interface IToDoRepository
    {
        Task<bool> AddToDoAsync(ToDo toDo);
        Task<IEnumerable<ToDo>> GetAllToDosAsync();
        Task<ToDo> GetToDo(int id);
        Task<bool> DeleteToDo(int id);
    }
}
