using Microsoft.EntityFrameworkCore;
using TodoAssignment.Data;
using TodoAssignment.Models;

namespace TodoAssignment.Repositories
{
    public class ToDoRepository : IToDoRepository
    {
        private readonly AppDbContext _dbContext;

        public ToDoRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> AddToDoAsync(ToDo toDo)
        {
            _dbContext.ToDos.Add(toDo);
            await _dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<IEnumerable<ToDo>> GetAllToDosAsync()
        {
            return await _dbContext.ToDos.ToListAsync();
        }

        public async Task<ToDo> GetToDo(int id)
        {
            return await _dbContext.ToDos.FindAsync(id);
        }

        public async Task<bool> DeleteToDo(int id)
        {
            var todo = await _dbContext.ToDos.FindAsync(id);

            if(todo== null)
            {
                return false;
            }

            _dbContext.ToDos.Remove(todo);
            await _dbContext.SaveChangesAsync();

            return true;
        }
    }
}
