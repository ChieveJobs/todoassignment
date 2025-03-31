using Microsoft.AspNetCore.Mvc;
using TodoAssignment.Data;
using TodoAssignment.Models;
using TodoAssignment.Repositories;
using TodoAssignmentAPI.DTO;

namespace TodoAssignment.Controllers;

[ApiController]
[Route("[controller]")]
public class ToDoController : ControllerBase
{
    private readonly IToDoRepository _repository;
    private readonly ILogger<ToDoController> _logger;

    public ToDoController(IToDoRepository repository, ILogger<ToDoController> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    [HttpPost]
    public async Task<ActionResult<IEnumerable<ToDo>>> Create([FromBody] CreateToDoDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.Title))
        {
            return BadRequest("Title is required.");
        }

        await _repository.AddToDoAsync(new ToDo { Title = dto.Title, IsCompleted = false, CreatedAt = DateTime.Now });
        
        return CreatedAtAction(nameof(Get), new { id = dto.Title }, dto);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ToDo>>> GetAll()
    {
        var toDos = await _repository.GetAllToDosAsync();

        return Ok(toDos);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<IEnumerable<ToDo>>> Get(int id)
    {
        var toDos = await _repository.GetToDo(id);

        if (toDos == null)
        {
            return NotFound($"ToDo with ID {id} not found.");
        }

        return Ok(toDos);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<IEnumerable<ToDo>>> Delete(int id)
    {
        var deleteSuccesful = await _repository.DeleteToDo(id);

        if (!deleteSuccesful)
        {
            return NotFound($"ToDo with ID {id} not found.");
        }

        return Ok();
    }
}
