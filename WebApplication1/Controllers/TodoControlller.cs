using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using System.Collections.Generic;
using MeuTodo.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using TODOAPI.ViewModels;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("V1")]
    public class TodoControlller: ControllerBase
    {
   
        [HttpGet]
        [Route("Todos")]
        public  async Task<IActionResult>GetAsync(
                         [FromServices]AppDbContext context)
        {
            var todosList = await context
                .Todos
                .AsNoTracking()
                .ToListAsync();

            return Ok(todosList);
        }
        [HttpGet]
        [Route("Todos/{id}")]
        public async Task<IActionResult> GetByIdAsync(
                        [FromServices] AppDbContext context, 
                        [FromRoute]int id)
        {
            var todo = await context
                .Todos
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.ID == id); 

            return todo == null
                   ? NotFound()
                   : Ok(todo);
        }
        [HttpPost("Todos")]
        public async Task<IActionResult> PostAsync(
                        [FromServices] AppDbContext context,
                        [FromBody]CreateTodoVM Model)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);

            var todo = new Todo
            {
                Date = DateTime.Now,
                IsDone = false,
                Title = Model.Title,
                Description = Model.Description
            };
           
            try
            {
                await context.Todos.AddAsync(todo);
                await context.SaveChangesAsync();
                return Created($"V1/Todos/{todo.ID}", todo);
            }
            catch (Exception ex) 
            {
                return BadRequest($"Ocorreu um erro ao criar o objeto Todo. Detalhes: {ex.InnerException.Message}");
            }
        }
        [HttpPut("Todos/{id}")]
        public async Task<IActionResult> PutAsync(
                        [FromServices] AppDbContext context,
                        [FromBody] CreateTodoVM Model,
                        [FromRoute] int id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var todo = await context.Todos
                .FirstOrDefaultAsync(x => x.ID == id);

            if (todo == null) return NotFound();

            try
            {
                todo.Title = Model.Title;
                todo.Description = Model.Description;


                context.Todos.Update(todo);
                await context.SaveChangesAsync();
                return Ok(todo);
            }
            catch (Exception ex)
            {
                return BadRequest($"Ocorreu um erro ao criar o objeto Todo. Detalhes: {ex.InnerException.Message}");
            }
        }
        [HttpDelete("Todos/{id}")]
        public async Task<IActionResult> DeleteAsync(
                        [FromServices] AppDbContext context,    
                        [FromRoute] int id)
        {
            var todo = await context.Todos
               .FirstOrDefaultAsync(x => x.ID == id);

            if (todo == null) return NotFound();

            try
            {
                context.Todos.Remove(todo);
                await context.SaveChangesAsync();
                return Ok($"O objeto Todo com o ID {id} foi excluído com sucesso.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Ocorreu um erro ao criar o objeto Todo. Detalhes: {ex.InnerException.Message}");
            }


        }

    }
}
